using Bidding.Models.ApplicationModels.Configuration;
using Bidding.Models.Contexts;
using Bidding.Models.DatabaseModels.Shared;
using Bidding.Models.ViewModels.Users.Shared;
using Bidding.Repositories.Auctions;
using Bidding.Repositories.Shared;
using Bidding.Repositories.Subscribe;
using Bidding.Repositories.Users;
using Bidding.Services.Auctions;
using Bidding.Services.Shared;
using Bidding.Services.Shared.Permissions;
using Bidding.Services.Subscribe;
using Bidding.Services.Users;
using Bidding.Shared.Attributes;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility.Validation.Comparers;
using FeatureAuthorize;
using FeatureAuthorize.PolicyCode;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp.Web.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bidding
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: false)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSinglePageApplication(ref services);
            ConfigureHttps(ref services);
            ConfigureAntiCSRF(ref services);
            ConfigureSession(ref services);
            ConfigureMVC(ref services);
            ConfigureCORS(ref services);
            ConfigureHttpContext(ref services);
            ConfigureAppConfigurationService(ref services);
            ConfigureAppServices(ref services);
            ConfigureAuthentication(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IAntiforgery antiforgery)
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

            app.UseCors("Dev");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            // Custom responses
            app.Use(next => context =>
            {
                // TODO: use https: for everything!
                // TODO: Not working with live code editing in development.
                if (!env.IsDevelopment())
                {
                    // Content Security Policy
                    //context.Response.Headers.Add(
                    //    "Content-Security-Policy",
                    //    "default-src 'none'; script-src 'self'; connect-src 'self' ws:; img-src 'self'; style-src 'self' 'unsafe-inline'; font-src 'self' data:; frame-ancestors 'none'"
                    //);
                }

                string path = context.Request.Path.Value;

                if (
                    string.Equals(path, "/", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(path, "/index.html", StringComparison.OrdinalIgnoreCase))
                {
                    // Set cookie with anti CSRF token in it, used by angular.
                    var tokens = antiforgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                        new CookieOptions() { HttpOnly = false });
                }

                return next(context);
            });

            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "start/{controller=Auth}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }

        private void ConfigureSinglePageApplication(ref IServiceCollection services)
        {
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        private void ConfigureHttps(ref IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                // not working - ATM
                //services.AddHttpsRedirection(options => { options.HttpsPort = 44300; });
            }
            else
            {
                // services.AddHttpsRedirection(options => { options.HttpsPort = 443; });
            }
        }

        private void ConfigureAntiCSRF(ref IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
                options.SuppressXFrameOptionsHeader = false;
            });
        }

        private void ConfigureSession(ref IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        private void ConfigureMVC(ref IServiceCollection services)
        {
            // todo: kke: whats this?
            services.AddTransient<AntiforgeryCookieResultFilterAttribute>();

            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(ApiExceptionFilter));
                    var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                    options.Filters.AddService<AntiforgeryCookieResultFilterAttribute>();
                    options.Filters.Add(new AuthorizeFilter(policy));
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private void ConfigureCORS(ref IServiceCollection services)
        {
            string[] origins = { "http://localhost:62411" }; // development

            if (Environment.IsProduction())
            {
                // origins = new[] { "http://localhost:4200", "http://localhost:3000", "http://devup.azurewebsites.net" };
            }

            // TODO: Check that these CORS settings are correct!
            services.AddCors(
                options => options.AddPolicy("Dev", builder =>
                {
                    builder.WithOrigins(origins);
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowCredentials();
                }));
        }

        private void ConfigureHttpContext(ref IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        private void ConfigureAppConfigurationService(ref IServiceCollection services)
        {
            services.AddSingleton(Configuration);
        }

        /// <summary>
        /// If there is a problem with Cors, check if you have added service and repository to services.
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureAppServices(ref IServiceCollection services)
        {
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<ISubscribeRepository, SubscribeRepository>();
            services.AddScoped<PermissionService>();
            services.AddScoped<PermissionRepository>();
            services.AddScoped<AuctionsService>();
            services.AddScoped<AuctionsRepository>();
            services.AddScoped<UsersService>();
            services.AddScoped<UsersRepository>();
            services.AddScoped<FileUploaderService>();
            services.AddScoped<FileUploaderRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Configure ImageSharp for image compression
            services.AddImageSharp();
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            var authenticationSettings = Configuration
                .GetSection(nameof(Authentication))
                .Get<Authentication>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<BiddingContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<BiddingContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("BiddingBaltic"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
                )
            );

            //Register the Permission policy handlers
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.Cookie.Name = Configuration["Cookies:Session"];
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    options.LoginPath = "/sign-in";
                    options.Cookie.IsEssential = true;
                    options.Events = new CookieAuthenticationEvents()
                    {
                        OnRedirectToLogin = (context) =>
                        {
                            if (context.Request.Path.StartsWithSegments("/api"))
                            {
                                context.Response.Clear();
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                return Task.CompletedTask;
                            }
                            context.Response.Redirect(context.RedirectUri);
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddOpenIdConnect(Configuration["Authentication:Scheme"], options =>
                {
                    // Set the authority to your Auth0 domain
                    options.Authority = Configuration["Authentication:Authority"];

                    // Configure the Auth0 Client ID and Client Secret
                    options.ClientId = Configuration["Authentication:ClientId"];
                    options.ClientSecret = Configuration["Authentication:ClientSecret"];

                    // Set response type to code
                    options.ResponseType = "code";
                    options.SaveTokens = true;

                    // Configure the scope
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");

                    options.CallbackPath = new PathString("/" + Configuration["Authentication:CallbackPath"]);

                    // Configure the Claims Issuer to be Auth0
                    options.ClaimsIssuer = Configuration["Authentication:Issuer"];

                    options.Events = new OpenIdConnectEvents
                    {
                        // handle the logout redirection
                        OnRedirectToIdentityProviderForSignOut = (context) =>
                        {
                            string logoutUri = $"https://{Configuration["Authentication:Domain"]}/v2/logout?client_id={Configuration["Authentication:ClientId"]}";
                            string postLogoutUri = context.Properties.RedirectUri;

                            if (postLogoutUri.IsNotSpecified() == false)
                            {
                                if (postLogoutUri.StartsWith("/"))
                                {
                                    // transform to absolute
                                    var request = context.Request;
                                    postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                                }
                                logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
                            }

                            context.Response.Redirect(logoutUri);
                            context.HandleResponse();

                            return Task.CompletedTask;
                        },
                        OnTokenValidated = async (context) =>
                        {
                            JwtPayload payload = context.SecurityToken.Payload;

                            if (payload.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.Unauthorized, UserErrorMessage.CanNotSignIn); }

                            ApplicationUser user = new ApplicationUser()
                            {
                                Email = payload["email"].ToString(),
                                IdentityId = payload["sub"].ToString(),
                                EmailConfirmed = Convert.ToBoolean(payload["email_verified"]),
                                UserName = payload["email"].ToString()
                            };

                            UsersService usersService = services.BuildServiceProvider().GetService<UsersService>();
                            ApplicationUser userDetails = await usersService.HandleUserLoginAsync(user).ConfigureAwait(false);

                            // setup user claims
                            context.Principal.AddIdentity(await SetupUserClaimsAsync(services, userDetails).ConfigureAwait(false));

                            // setup profile cookie
                            string userProfileCookieJSON = SetupUserProfileCookie(userDetails);

                            // setup profile cookie options
                            CookieOptions userProfileCookieOptions = SetupUserProfileCookieOptions();

                            context.Response.Cookies.Append("BIDPROFILE", userProfileCookieJSON, userProfileCookieOptions);
                        }
                    };
                });
        }

        private async Task<ClaimsIdentity> SetupUserClaimsAsync(IServiceCollection services, ApplicationUser userDetails)
        {
            BiddingContext dbContext = services.BuildServiceProvider().GetService<BiddingContext>();
            var rtoPCalcer = new CalcAllowedPermissions(dbContext);

            List<Claim> claims = new List<Claim>
            {
                new Claim(
                    type: PermissionConstants.UserIdClaimType,
                    value: userDetails.Id.ToString(),
                    valueType: ClaimValueTypes.Integer,
                    issuer: "Bidding"
                ),
                new Claim(
                    type: PermissionConstants.PackedPermissionClaimType,
                    value: await rtoPCalcer.CalcPermissionsForUserAsync(userDetails.Id).ConfigureAwait(false)
                )
            };

            return new ClaimsIdentity(claims);
        }

        /// <summary>
        /// Create the profile cookie, used for displaying user information in the client.
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        private string SetupUserProfileCookie(ApplicationUser userDetails)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new UserProfileCookieModel()
            {
                IsAuthenticated = true,
                UserId = userDetails.Id,
                Email = userDetails.Email
            });
        }

        private CookieOptions SetupUserProfileCookieOptions()
        {
            return new CookieOptions
            {
                SameSite = SameSiteMode.Lax,
                HttpOnly = false
            };
        }
    }
}
