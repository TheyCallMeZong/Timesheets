using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Timesheets.Data.Ef;
using Timesheets.Data.Implementations;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Implementations;
using Timesheets.Domain.Interfaces;
using Timesheets.Models.Dto.Authentication;

namespace Timesheets.Infrastucture.Extensions
{
    internal static class Configuration
    {
        public static void AuthenticateConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AccessToken>(configuration.GetSection("Authentication:AccessToken"));
            services.Configure<RefreshToken>(configuration.GetSection("Authentication:RefreshToken"));
            var jwtSettings = new JwtOptions();
            configuration.Bind("Authentication:AccessToken", jwtSettings);
            
            services.AddTransient<ILoginManager, LoginManager>();
            
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = jwtSettings.GetTokenValidationParameters();
                });
        }
        public static void ManagerConfig(this IServiceCollection services)
        {
            services.AddScoped<IUserManager,UserManager>();
            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<IRefreshTokenManager, RefreshTokenManager>();
        }

        public static void RepositoriesConfig(this IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
        }

        public static void DbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PostgreSqlDbContext>(option =>
                            option.UseNpgsql(configuration.GetConnectionString("PostgreSqlDbContext")));
        }

        public static void SwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Timesheets", 
                    Version = "v1",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, 
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}