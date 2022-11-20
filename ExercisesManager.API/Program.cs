using ExercisesManager.API.Configurations;
using ExercisesManager.API.DI;
using ExercisesManager.API.Extensions;
using ExercisesManager.API.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerUI;
var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup().LoadConfigurationFromSection(builder.Configuration).GetCurrentClassLogger();

// Add services to the container.
try
{
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwagger();
    builder.Services.AddDatabase(builder.Configuration);
    builder.Services.AddCustomAuthentication(GetIdentityConfig());
    builder.Services.AddServices();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    IdentityConfig GetIdentityConfig()
    {
        var secret = builder.Configuration["JWT:Secret"];
        var issuer = builder.Configuration["JWT:ValidIssuer"];
        return new IdentityConfig(secret, issuer);
    }

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<ErrorResponseDTO>>()); // Global Error Handler

    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Exercises manager api");
        options.RoutePrefix = string.Empty;
        options.SupportedSubmitMethods(new SubmitMethod[] { SubmitMethod.Delete, SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put });
    });

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.ApplyMigrations();

    app.Run();

}
catch (Exception ex)
{
    logger.Error(ex, "Exception in program.cs file");
    throw;
}
