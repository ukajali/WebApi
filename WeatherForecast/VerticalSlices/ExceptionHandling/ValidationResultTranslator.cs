using System.Linq;
using FluentValidation.Results;

namespace WeatherForecast.VerticalSlices.ExceptionHandling
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
