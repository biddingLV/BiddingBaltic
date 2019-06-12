using Bidding.Models.ViewModels.Bidding.Users.Add;
using Bidding.Models.ViewModels.Bidding.Users.Shared;
using Bidding.Repositories.Shared;
using Bidding.Repositories.Users;
using Bidding.Services.Shared.Permissions;
using Bidding.Services.Users;
using Bidding.Shared.Attributes;
using Bidding.Shared.Authorization;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.ErrorHandling.Validators;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility;
using Bidding.Models.ApplicationModels.Configuration;
using Bidding.Models.DatabaseModels;
using Bidding.Models.DatabaseModels.Bidding;
using Bidding.Repositories.Auctions;
using Bidding.Repositories.Subscribe;
using Bidding.Services.Auctions;
using Bidding.Services.Subscribe;
using FluentValidation;
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
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Bidding.Database.Contexts;
using Bidding.Database.DatabaseModels.Auctions;

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
            ConfigureDbContext(ref services);
            ConfigureHttpContext(ref services);
            ConfigureAppConfigurationService(ref services);
            ConfigureAppServices(ref services);
            ConfigureFluentApiValidators(ref services);
            ConfigureAuthentication(services);
            ConfigureAuthorization(ref services);
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
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); // TODO: KKE: Check why this is not working for post requests!
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

        private void ConfigureDbContext(ref IServiceCollection services)
        {
            services.AddDbContext<BiddingContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("BiddingLV"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
                )
            );
        }

        private void ConfigureHttpContext(ref IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
        }

        private void ConfigureAppConfigurationService(ref IServiceCollection services)
        {
            services.AddSingleton(Configuration);

        }

        private void ConfigureAppServices(ref IServiceCollection services)
        {
            // if there is a problem with Cors, check if you have added service and repo here!
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<ISubscribeRepository, SubscribeRepository>();
            services.AddScoped<PermissionService>();
            services.AddScoped<PermissionRepository>();
            services.AddScoped<AuctionsService>();
            services.AddScoped<AuctionsRepository>();
            services.AddScoped<UsersService>();
            services.AddScoped<UsersRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        private void ConfigureFluentApiValidators(ref IServiceCollection services)
        {
            //In order for ASP.NET to discover your validators, they must be registered with the services collection.
            services.AddTransient<IValidator<Auction>, AuctionValidator>();
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            var authenticationSettings = Configuration.GetSection(nameof(Authentication)).Get<Authentication>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<BiddingContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
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

                       if (payload.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.Unauthorized, UserErrorMessages.CanNotSignIn); }

                       BiddingContext m_context = context.HttpContext.RequestServices.GetRequiredService<BiddingContext>();

                       // todo: kke: fix this to not be object comparison!
                       if (payload["email_verified"].IsNotSpecified()) { throw new WebApiException(HttpStatusCode.Unauthorized, UserErrorMessages.UsersEmailNotVerified); }

                       string usersIdentityId = payload["sub"].ToString();
                       string usersEmail = payload["email"].ToString();

                       if (usersIdentityId.IsNotSpecified() == false && usersEmail.IsNotSpecified() == false)
                       {
                           // todo: kke: 1. check if this is the first sign in for the user!
                           // todo: kke: can we use auth0 for that?
                           // 2. if user doesnt exist fetch and save all the information!

                           UsersService usersServiceProvider = services.BuildServiceProvider().GetService<UsersService>();
                           UserProfileModel userDetails = new UserProfileModel();

                           if (usersServiceProvider.UserExists(usersEmail))
                           {
                               // load user details for the profile cookie
                               userDetails = services.BuildServiceProvider().GetService<UsersService>().UserDetails(usersEmail);
                           }
                           else
                           {
                               UserAddRequestModel newUser = new UserAddRequestModel()
                               {
                                   // todo: kke: what about social logins here? can we get full name?
                                   Email = payload["email"].ToString(),
                                   UniqueIdentifier = payload["sub"].ToString()
                               };

                               // todo: kke: maybe merge these two in one call so we dont call db two times?
                               usersServiceProvider.Create(newUser);
                               userDetails = services.BuildServiceProvider().GetService<UsersService>().UserDetails(usersEmail);
                           }

                           // validate user details
                           // ValidateUserDetails(userDetails, usersIdentityId, m_context);

                           // setup user claims
                           context.Principal.AddIdentity(SetupUserClaims(userDetails));

                           // setup profile cookie
                           string userProfileCookieJSON = SetupUserProfileCookie(userDetails);

                           // setup profile cookie options
                           CookieOptions userProfileCookieOptions = SetupUserProfileCookieOptions();

                           context.Response.Cookies.Append("BIDPROFILE", userProfileCookieJSON, userProfileCookieOptions);
                       }
                       else
                       {
                           throw new WebApiException(HttpStatusCode.Unauthorized, UserErrorMessages.CanNotSignIn);
                       }
                   }
               };
           });
        }

        private string SetupUserProfileCookie(UserProfileModel userDetails)
        {
            // Create the profile cookie, used for displaying user information in the client.
            return Newtonsoft.Json.JsonConvert.SerializeObject(new UserProfileCookie()
            {
                IsAuthenticated = true,
                UserId = userDetails.UserId, // todo: kke: why do we setup here user id?
                Email = userDetails.UserEmail
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

        private void ValidateUserDetails(UserProfileModel userDetails, string payloadUserUniqueIdentifier, BiddingContext m_context)
        {
            // our database and auth0 user unique identifiers need to be the same
            if (userDetails.UserUniqueIdentifier != payloadUserUniqueIdentifier) { throw new WebApiException(HttpStatusCode.Unauthorized, UserErrorMessages.CanNotSignIn); }
        }

        private ClaimsIdentity SetupUserClaims(UserProfileModel userDetails)
        {
            // setup user id and organization id claims
            List<Claim> claims = new List<Claim>
            {
                new Claim("UserId", userDetails.UserId.ToString(), ClaimValueTypes.Integer, "Bidding"),
                new Claim(ClaimTypes.Role, userDetails.UserRole, ClaimValueTypes.String, "Bidding")
            };

            // setup claims identity
            return new ClaimsIdentity(claims);
        }

        private void ConfigureAuthorization(ref IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddAuthorization(options =>
            {
                // todo: implement this!
                // Permissions Access
                options.AddPolicy("View Admin Panel",
                    policy => policy.Requirements.Add(new PermissionRequirement(1)));
            });

            // Dev policies
            if (Environment.IsDevelopment())
            {

            }
            else // Prod policies
            {

            }
        }
    }
}
