using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using TeamHolidayPlanner.Data;
using TeamHolidayPlanner.Domain;
using TeamHolidayPlanner.Web.Controllers;
using TeamHolidayPlanner.Web.Identity;
using TeamHolidayPlanner.Web.Models;

namespace Web
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(ILogger<Startup> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityCore<User>()
              .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            })
            .AddCookie(IdentityConstants.ApplicationScheme, o =>
            {
                o.LoginPath = new PathString("/Account/Login");
                o.Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync
                };
            });

            var connectionString = Configuration["ConnectionStrings:EmployeeContext"];
            services.AddDbContext<EmployeeContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<DbContext, EmployeeContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserClaimsPrincipalFactory<User>, ApplicationUserClaimsPrincipalFactory>();
            services.AddScoped<IUserStore<User>, TeamHolidayPlanner.Web.Identity.UserStore>();
            services.AddScoped<SignInManager<User>>();
            services.AddScoped<ISecurityStampValidator, SecurityStampValidator<User>>();

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddHttpContextAccessor();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddAuthorization(options =>
            {
                // Couldnt get the DefaultAuthorizationPolicyProvider working :(
                AddEntitiesPolicy(options, "Department");
                AddEntitiesPolicy(options, "Employee");
                AddEntitiesPolicy(options, "EmploymentGrade");
                AddEntitiesPolicy(options, "HolidayPeriod");

            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            _logger.LogDebug($"({services.Count}) SERVICES!!!");
            foreach (var service in services)
            {
                _logger.LogDebug($"SERVICE:[{service.Lifetime}] {service.ServiceType.FullName}  ({service.ImplementationType})");
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Department, DepartmentModel>();
            }
         );
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            //var genericRepository = app.ApplicationServices.GetService<EmployeeContext>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void AddEntitiesPolicy(AuthorizationOptions options, string entityName)
        {
            AddEntityPolicy(options, entityName, "Index");
            AddEntityPolicy(options, entityName, "Details");
            AddEntityPolicy(options, entityName, "Create");
            AddEntityPolicy(options, entityName, "Edit");
            AddEntityPolicy(options, entityName, "Delete");
        }

        private void AddEntityPolicy(AuthorizationOptions options, string entityName, string action)
        {
            var value = $"{entityName}{action}";

            options.AddPolicy(
                 value, policy => policy
                .RequireAuthenticatedUser()
                .RequireClaim("permission", value));
        }
    }
}
