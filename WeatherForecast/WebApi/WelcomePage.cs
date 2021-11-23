﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WeatherForecast.Core.Contracts;
using WeatherForecast.Core.Model;

namespace WeatherForecast.WebApi
{
    public static class WelcomePage
    {
        public static async Task GetHtmlContent(HttpContext context)
        {
            var services = context.RequestServices;
            var dbContext = (IDatabaseContext)services.GetService(typeof(IDatabaseContext));
            var locations = dbContext.LocationClimates.ToList();

            await context.Response.WriteAsync(GenerateWelcomePageHtml(locations));
        }
        private static string GenerateHtmlLocationsList(List<Climate> locations)
        {
            var locationsBilder = new StringBuilder();
            locationsBilder.Append("<ul>");
            foreach (var clim in locations)
            {
                locationsBilder.Append("<li>");
                locationsBilder.Append(clim.Location.GetFullLocation());
                locationsBilder.Append("</li>");
            }
            locationsBilder.Append("</ul>");
           
            return locationsBilder.ToString();
        }
        private static string GenerateWelcomePageHtml(List<Climate> locationsList)
        {
            var locations = GenerateHtmlLocationsList(locationsList);
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                <link rel='icon' type='image/svg+xml' href='/favicon.svg'>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1'>
                <title>Welcome - Weather Forecast Service</title>
                <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css' rel='stylesheet' integrity='sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3' crossorigin='anonymous'>
                </head>
                <body>
                <main>
                    <div class='container py-4'>
                        <header class='pb-3 mb-4 border-bottom'>
                            <a href='/' class='d-flex align-items-center text-dark text-decoration-none'><img src='/favicon.svg'></img><span class='fs-4'>Weather Forecast</span></a>
                        </header>
                            
                        <div class='p-4 mb-4 bg-light rounded-3'>
                            <div class='container-fluid py-4'>
                                <h1 class='display-5 fw-bold'>Weather Forecast</h1>
                                <p>
                                    Service is dedicated to generate weather forecast for different locations up to 14-day 
                                </p>
                            </div>
                        </div>

                        <div class='row align-items-md-stretch'>
                            <div class='col-md-6'>
                                <div class='h-100 p-5 text-white bg-dark rounded-3'>
                                    <h2>Open API</h2>
                                    <a href='/swagger' class='btn btn-outline-light' role='button'>Open Swagger UI</a>
                                    <h2 class='mt-4'>Sample forecasts</h2>
                                    <ul>
                                        <li><a href='/Forecast?days=14&country=poland&city=krakow' class='link-light'>Poland: Kraków - 14 days (format: JSON)</a></li>
                                    </ul>
                                </div>
                            </div>
                            <div class='col-md-6'>
                                <div class='h-100 p-5 bg-light border rounded-3'>
                                    <h2>Locations</h2>
                                    <p>Supported locations: {locations} </p>
                                </div>
                            </div>
                        </div>

                        <footer class='pt-3 mt-4 text-muted border-top'>
                            &copy; 2021
                        </footer>
                    </div>
                </main>    

                <script src='https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js' integrity='sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p' crossorigin='anonymous'></script>
                </body>
                </html>";
        }
       
        public static async Task GetFavicon(HttpContext context)
        {
            context.Response.ContentType = "image/svg+xml";
            await context.Response.WriteAsync(SvgIcon); 
        }

        private const string SvgIcon =@"
                <svg xmlns='http://www.w3.org/2000/svg' width='40' height='32' class='me-2' viewBox='0 0 118 94' role='img'>
                    <title>Bootstrap icon</title>
                    <path fill-rule='evenodd' clip-rule='evenodd' d='M24.509 0c-6.733 0-11.715 5.893-11.492 12.284.214 6.14-.064 14.092-2.066 20.577C8.943 39.365 5.547 43.485 0 44.014v5.972c5.547.529 8.943 4.649 10.951 11.153 2.002 6.485 2.28 14.437 2.066 20.577C12.794 88.106 17.776 94 24.51 94H93.5c6.733 0 11.714-5.893 11.491-12.284-.214-6.14.064-14.092 2.066-20.577 2.009-6.504 5.396-10.624 10.943-11.153v-5.972c-5.547-.529-8.934-4.649-10.943-11.153-2.002-6.484-2.28-14.437-2.066-20.577C105.214 5.894 100.233 0 93.5 0H24.508zM80 57.863C80 66.663 73.436 72 62.543 72H44a2 2 0 01-2-2V24a2 2 0 012-2h18.437c9.083 0 15.044 4.92 15.044 12.474 0 5.302-4.01 10.049-9.119 10.88v.277C75.317 46.394 80 51.21 80 57.863zM60.521 28.34H49.948v14.934h8.905c6.884 0 10.68-2.772 10.68-7.727 0-4.643-3.264-7.207-9.012-7.207zM49.948 49.2v16.458H60.91c7.167 0 10.964-2.876 10.964-8.281 0-5.406-3.903-8.178-11.425-8.178H49.948z' fill='lawngreen'></path>
                </svg>";

     
    }
}