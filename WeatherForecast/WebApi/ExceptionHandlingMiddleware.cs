using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;

namespace WeatherForecast.WebApi
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                var errorList = exception.Errors.Select(x => x.ErrorMessage).ToArray();
                var result = JsonSerializer.Serialize(new { errors = errorList });
                await response.WriteAsync(result);
            }
        }
    }
}
