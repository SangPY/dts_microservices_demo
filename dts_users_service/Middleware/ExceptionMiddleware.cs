using dts_users_service.Common;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace dts_users_service.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                Log.Warning(ex, "Validation Failed");

                await HandleValidationException(context, ex);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleNotFound(context, ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);

                await HandleException(context, ex);
            }
        }

        private static async Task HandleValidationException(
            HttpContext context,
            ValidationException ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            //var response = new
            //{
            //    Success = false,
            //    Message = "Validation Failed",
            //    Errors =  ex.Message
            //};

            var response = new ErrorResponse
            {
                Success = false,

                Message = "Validation Failed",

                Errors = ex.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList(),

                Timestamp = DateTime.UtcNow,

                TraceId = context.TraceIdentifier
            };


            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }

        private static async Task HandleNotFound(
            HttpContext context,
            Exception ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = StatusCodes.Status404NotFound;

            var response = new ErrorResponse
            {
                Success = false,
                Message = ex.Message,
                Timestamp = DateTime.UtcNow,
                TraceId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }

        private static async Task HandleException(
            HttpContext context,
            Exception ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new ErrorResponse
            {
                Success = false,
                Message = "Internal Server Error",
                Errors = new List<string> { ex.Message },
                Timestamp = DateTime.UtcNow,
                TraceId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
