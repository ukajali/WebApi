using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.VerticalSlices.ExeptionHandling
{
    public static class ValidationResultTranslator
    {
        public static string ValidationResultTranslate(ValidationResult validationResult)
        {
            var errorList = validationResult.Errors.Select(x => x.ErrorMessage).ToArray();
            var errorString = errorList.Aggregate((x, y) => x + "\n" + y);
            return errorString;
        }
    }
}
