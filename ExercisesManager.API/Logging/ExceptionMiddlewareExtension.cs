using Microsoft.AspNetCore.Diagnostics;

namespace ExercisesManager.API.Logging
{
    internal static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger<ErrorResponseDTO> logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var errorMessage = $"Something went wrong: {contextFeature.Error}";
                        logger.LogError(contextFeature.Error, errorMessage);

                        await context.Response.WriteAsync(new ErrorResponseDTO
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Something went wrong. See logged exceptions for more details."
                        }.ToString());
                    }
                });
            });
        }
    }
}