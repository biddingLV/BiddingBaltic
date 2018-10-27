using Bidding.Shared.Exceptions;
using BiddingAPI.Authorization;
using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Repositories.Subscribe;
using BiddingAPI.Services.Subscribe;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Bidding
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureMVC(ref services);

            // todo: kke: why is this needed?
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BiddingContext>()
                .AddDefaultTokenProviders();
            //

            // https://www.jerriepelser.com/blog/accessing-tokens-aspnet-core-2/
            // https://wildermuth.com/2017/08/19/Two-AuthorizationSchemes-in-ASP-NET-Core-2
            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:ApiIdentifier"];
            });
            //.AddJwtBearer(opt =>
            //{
            //    opt.Authority = domain;
            //    opt.Audience = Configuration["Auth0:ApiIdentifier"];

            //    //opt.TokenValidationParameters = new TokenValidationParameters()
            //    //{
            //    //    ValidIssuer = Configuration["Auth0:Issuer"],
            //    //    ValidAudience = Configuration["Auth0:Issuer"],
            //    //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth0:Key"]))
            //    //};
            //    // this - https://joonasw.net/view/adding-custom-claims-aspnet-core-2
            //    //opt.TokenValidationParameters.
            //    // https://joonasw.net/view/adding-custom-claims-aspnet-core-2

            //});

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
            });

            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            ConfigureCORS(ref services);
            ConfigureDbContext(ref services);
            ConfigureAppConfigurationService(ref services);
            ConfigureAppServices(ref services);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseCors("AllowSpecificOrigin");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private void ConfigureMVC(ref IServiceCollection services)
        {
            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(ApiExceptionFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void ConfigureCORS(ref IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
        }

        private void ConfigureDbContext(ref IServiceCollection services)
        {
            services.AddDbContext<BiddingContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BiddingLV"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()));
        }

        private void ConfigureAppConfigurationService(ref IServiceCollection services)
        {
            services.AddSingleton(Configuration);

        }

        private void ConfigureAppServices(ref IServiceCollection services)
        {
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<ISubscribeRepository, SubscribeRepository>();
        }
    }
}
