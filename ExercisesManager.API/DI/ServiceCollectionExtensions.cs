using System.Reflection;
using System.Text;
using ExercisesManager.API.Configurations;

using ExercisesManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ExercisesManager.API.DI
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var sectionName = "PostgreSQL";
            var host = configuration.GetSection($"{sectionName}:host").Value;
            var username = configuration.GetSection($"{sectionName}:username").Value;
            var password = configuration.GetSection($"{sectionName}:password").Value;
            var database = configuration.GetSection($"{sectionName}:database").Value;
            var port = configuration.GetSection($"{sectionName}:port").Value;

            var databaseInfo = new DatabaseConfig(host, port, username, password, database);

            var migrationsAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;

            services
                .AddDbContext<ApplicationDbContext>(
                    builder => builder.UseNpgsql(databaseInfo.ConnectionString, optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(migrationsAssembly);
                        optionsBuilder.EnableRetryOnFailure(
                            maxRetryCount: 15,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorCodesToAdd: null);
                    }));

            return services;
        }

        // public static IServiceCollection AddServices(this IServiceCollection services)
        // {
        //     services.AddScoped<IUsersService, UsersService>();
        //     services.AddScoped<IExercisesService, ExercisesService>();

        //     return services;
        // }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                string assemblyVersion = string.Empty;
                var assembly = Assembly.GetEntryAssembly();

                if (assembly != null)
                {
                    var versionAttribute = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
                    assemblyVersion = versionAttribute == null ? string.Empty : versionAttribute.Version;
                }

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Exercises Manager API", Version = assemblyVersion });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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