using KnowCloud.Contract;
using KnowCloud.Filters;
using KnowCloud.Services.Contract;
using KnowCloud.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;

namespace KnowCloud.Utility
{
    public static class ServicesExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddControllers(options =>
            {
                options.Filters.Add<CustomExceptionFilterAttribute>(); // Filtro de excepciones global
            });

            services.AddHttpContextAccessor();

            // Configuración de HttpClient con tiempo de vida óptimo
            services.AddHttpClient<IAuthServices, AuthServices>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            // Inyección de dependencias
            services.AddTransient<IDataCloudAzure, DataCloudAzure>();
            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<ITokenProvider, TokenProvider>();

            // Configuración para subir archivos grandes
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10 MB
            });

            // Configuración de autenticación con cookies
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/Denied";
                    options.SlidingExpiration = true; // Mantiene la sesión activa mientras se use
                });

            // Configuración de autorización con políticas
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireRole("RoleAdmin"));
            });
        }
    }
}
