using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Repositories.Subscribe;
using BiddingAPI.Services.Subscribe;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // 1. Add Authentication Services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = "https://biddinglv.eu.auth0.com/";
                options.Audience = "http://localhost:61244/api/";
            });

            services.AddCors();

            // ConfigureMVC(ref services);
            // ConfigureCORS(ref services);
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            //app.UseCors(
            //    options => options.WithOrigins("http://localhost:4200", "http://bidding.lv", "http://biddinglv.azurewebsites.net/")
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //);

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            // 2. Enable authentication middleware
            app.UseAuthentication();

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

        //private void ConfigureMVC(ref IServiceCollection services)
        //{
        //    services
        //        .AddMvc(options =>
        //        {
        //            options.Filters.Add(typeof(ApiExceptionFilter));
        //            var policy = new AuthorizationPolicyBuilder()
        //                        .RequireAuthenticatedUser()
        //                        .Build();
        //            options.Filters.Add(new AuthorizeFilter(policy));
        //        })
        //        .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
        //        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        //}

        //private void ConfigureCORS(ref IServiceCollection services)
        //{
        //    services.AddCors(
        //        options => options.AddPolicy("Dev", builder =>
        //        {
        //            builder.WithOrigins("http://localhost:4200", "http://izsoles-dev.azurewebsites.net/");
        //            builder.AllowAnyHeader();
        //            builder.AllowAnyMethod();
        //            builder.AllowCredentials();
        //        }));
        //}

        private void ConfigureDbContext(ref IServiceCollection services)
        {
            services.AddDbContext<BiddingContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BiddingLV"),
                sqlOptions => sqlOptions.EnableRetryOnFailure()));
        }

        private void ConfigureAppConfigurationService(ref IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);

        }

        private void ConfigureAppServices(ref IServiceCollection services)
        {
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<ISubscribeRepository, SubscribeRepository>();
        }
    }
}
