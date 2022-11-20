using ExercisesManager.Data;
using Microsoft.EntityFrameworkCore;

namespace ExercisesManager.API.Extensions
{
    internal static class WebApplicationExtensions
    {
        public static WebApplication ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
            return app;
        }
    }
}