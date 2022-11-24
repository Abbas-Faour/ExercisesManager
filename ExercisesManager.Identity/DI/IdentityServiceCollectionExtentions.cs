using System.Reflection;
using System.Text;
using ExercisesManager.Identity.Config;
using ExercisesManager.Identity.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ExercisesManager.Identity.DI
{
    public static class IdentityServiceCollectionExtentions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IdentityConfig identityConfiguration)
        {

            var migrationsAssembly = typeof(IdentityContext).GetTypeInfo().Assembly.GetName().Name;

            services
                .AddDbContext<IdentityContext>(
                    builder => builder.UseNpgsql(identityConfiguration.ConnectionString, optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(migrationsAssembly);
                        optionsBuilder.EnableRetryOnFailure(
                            maxRetryCount: 15,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorCodesToAdd: null);
                    }));

            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.AddSingleton(identityConfiguration);

            // Add authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = identityConfiguration.IdentityIssuer,
                    ValidAudience = identityConfiguration.IdentityIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identityConfiguration.IdentitySecret))
                };
            });

            return services;
        }
    }
}