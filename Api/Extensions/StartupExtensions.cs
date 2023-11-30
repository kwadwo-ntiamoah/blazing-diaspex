using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Repositories;
using Data.Repositories.IRepositories;
using Data.Repositories.RepositoryImpl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.IServices;
using Services.ServicesImpl;

namespace Api.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration config) {
            services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(config.GetConnectionString("DbConnection")));
            
            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config) {
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt => {
                var key = config.GetValue<string>("JWT:Key");

                opt.SaveToken = true;
                opt.TokenValidationParameters = new() {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
                };
            });

            return services;
        }

        public static IServiceCollection SetupPasswordPolicy(this IServiceCollection services) {
            services.Configure<IdentityOptions>(opt => {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireDigit = false;
            });

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services) {
            services.AddScoped<JWTService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IReplyService, ReplyService>();

            return services;
        }

        public static IServiceCollection AddCustomRepositories(this IServiceCollection services) {
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<INewsRepo, NewsRepo>();
            services.AddScoped<IPostRepo, PostRepo>();
            services.AddScoped<IProfileRepo, ProfileRepo>();
            services.AddScoped<IReplyRepo, ReplyRepo>();

            return services;
        }
    }
}