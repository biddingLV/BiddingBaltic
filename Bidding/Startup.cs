using Bidding.Shared.Authorization;
using Bidding.Shared.Exceptions;
using BiddingAPI.Models.ApplicationModels.Configuration;
using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Repositories.Auctions;
using BiddingAPI.Repositories.Subscribe;
using BiddingAPI.Services.Auctions;
using BiddingAPI.Services.Subscribe;
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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Bidding
{
    public class Startup
    {
        // TODO: Move this class.
        public class AntiforgeryCookieResultFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ResultFilterAttribute
        {
            protected IAntiforgery Antiforgery { get; set; }
            public AntiforgeryCookieResultFilterAttribute(IAntiforgery antiforgery) => this.Antiforgery = antiforgery;

            public override void OnResultExecuting(Microsoft.AspNetCore.Mvc.Filters.ResultExecutingContext context)
            {
                // kke: you can see this in the cookie!
                var tokens = this.Antiforgery.GetAndStoreTokens(context.HttpContext);
                context.HttpContext.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions() { HttpOnly = false });
            }
        }

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
                // TODO: MJU: use https: for everything!
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
                    spa.UseAngularCliServer(npmScript: "start");
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
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
            // kke: what is this magic?
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

            // TODO: MJU: Check that these CORS settings are correct!
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
            services.AddScoped<IAuctionsService, AuctionsService>();
            services.AddScoped<IAuctionsRepository, AuctionsRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
               options.Cookie.Name = "TXSESSION";
               options.SlidingExpiration = true; // TODO: MJU: Should we do this? Usability vs. Security
               options.Cookie.HttpOnly = true;
               options.Cookie.SameSite = SameSiteMode.Lax;
               options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;   // TODO: MJU: Remember for https!
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
                       var logoutUri = $"https://{Configuration["Authentication:Domain"]}/v2/logout?client_id={Configuration["Authentication:ClientId"]}";

                       var postLogoutUri = context.Properties.RedirectUri;
                       if (!string.IsNullOrEmpty(postLogoutUri))
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

                       // Check if we got token.
                       if (payload == null) { return; }

                       BiddingContext m_context = context.HttpContext.RequestServices.GetRequiredService<BiddingContext>();
                       string isEmailVerified = context.SecurityToken.Payload["email_verified"].ToString();

                       // Check if email is verified from identityprovider.
                       bool couldParseEmailIsVerified = bool.TryParse(isEmailVerified, out var isEmailVerifiedResult);

                       // if email not verified - return
                       if (!couldParseEmailIsVerified || !isEmailVerifiedResult) { return; }

                       string userIdentityId = context.SecurityToken.Payload["sub"].ToString();
                       string userLoginEmail = context.SecurityToken.Payload["email"].ToString();

                       // Check if payload has userId & login email
                       if (!((userIdentityId != null || string.IsNullOrEmpty(userIdentityId)) && (userLoginEmail != null || string.IsNullOrEmpty(userLoginEmail)))) // TODO: MJU: Check this logic!
                       {
                           // missing email or provider|userId
                           return;
                       }

                       // load user details
                       //UserProfileResponseModel userDetails = LoadUserDetails(ref services, userLoginEmail);

                       // validate user details
                       //ValidateUserDetails(userDetails, userIdentityId, m_context);

                       // setup user claims
                       //context.Principal.AddIdentity(SetupUserClaims(userDetails));

                       // setup profile cookie
                       //string userProfileCookieJSON = SetupUserProfileCookie(userDetails);

                       // setup profile cookie options
                       //CookieOptions userProfileCookieOptions = SetupUserProfileCookieOptions();

                       //context.Response.Cookies.Append("TXPROFILE", userProfileCookieJSON, userProfileCookieOptions);

                       // TODO: MJU: Create XSRF-TOKEN cookie for angular csrf protection.
                   }
               };
           });
        }

        //private UserProfileResponseModel LoadUserDetails(ref IServiceCollection services, string userLoginEmail)
        //{
        //    return services.BuildServiceProvider().GetService<IUsersService>().UserDetails(userLoginEmail);
        //}

        //private string SetupUserProfileCookie(UserProfileResponseModel user)
        //{
        //    // Create the profile cookie, used for displaying user information in the client.
        //    UserProfileCookie userProfileCookie = new UserProfileCookie()
        //    {
        //        IsAuthenticated = true,
        //        OrganizationId = (int)user.OrgId,
        //        UserId = user.Id,
        //        UserName = user.UserName,
        //        Email = user.LoginEmail
        //    };

        //    // userProfileCookieJSON 
        //    return Newtonsoft.Json.JsonConvert.SerializeObject(userProfileCookie);
        //}

        private CookieOptions SetupUserProfileCookieOptions()
        {
            return new CookieOptions
            {
                SameSite = SameSiteMode.Lax,
                HttpOnly = false
            };
        }

        //private void ValidateUserDetails(UserProfileResponseModel user, string payloadUserIdentityId, SelfServiceContext m_context)
        //{
        //    // it cant be possible that we dont have the user information in our Database
        //    // At the moment: user can only be added to the system by invite.
        //    if (user.IsEmpty()) { return; }

        //    // our database and auth0 identity provider user ids must be the same
        //    if (user.IdentityProviderUserId != payloadUserIdentityId) { return; }

        //    // if users loginEmail missing return
        //    if (user.LoginEmail.IsEmpty()) { return; }    // NOTE: MJU: Should be almost impossible.

        //    // return if user organization missing
        //    if (user.OrgId.IsEmpty()) { return; }

        //    // save the identity provider user id if it is missing for the user
        //    if (user.IdentityProviderUserId.IsEmpty() || user.IdentityProviderUserId == "0" || user.IdentityProviderUserId == "auth0|0") // TODO: KKE: REMOVE LAST CHECK AFTER TESTING!
        //    {
        //        user.IdentityProviderUserId = payloadUserIdentityId;
        //        m_context.Entry(user).Property("IdentityProviderUserId").IsModified = true; // Only updating one property instead of entity

        //        m_context.SaveChanges();
        //    }
        //}

        //private ClaimsIdentity SetupUserClaims(UserProfileResponseModel user)
        //{
        //    // setup user id and organization id claims
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim("UserId", user.Id.ToString(), ClaimValueTypes.Integer, "Bidding"),
        //        new Claim("OrganizationId", user.OrgId.ToString(), ClaimValueTypes.Integer, "Bidding")
        //    };

        //    // handle super admin claims
        //    if (user.IsSuperAdmin)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, "SuperAdmin"));
        //    }
        //    else
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, user.RoleName));
        //    }

        //    // setup claims identity
        //    ClaimsIdentity appIdentity = new ClaimsIdentity(claims);

        //    return appIdentity;
        //}

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
