using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherForecast.VerticalSlices.ExeptionHandling;

namespace WeatherForecast.WebApi
{
    public class ExeptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception er)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                switch (er)
                {
                    case LocationException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                  
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = er?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
