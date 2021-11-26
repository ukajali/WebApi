using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.Core.Model
{
    public static class ExtensionsString
    {
        public static Tuple<string, string> SmartStringLocationSplit(this string str)
        {
            if(!string.IsNullOrEmpty(str) && str.Contains("/"))
            {
                var splitedStr = str.Split('/');
                if(splitedStr.Count() > 1)
                {

                    return Tuple.Create(splitedStr[0].ToString(), splitedStr[1].ToString()); 
                }
                else
                {
                    return Tuple.Create("", "");
                }
            }
            else
            {
                return Tuple.Create("", "");
            }
        }
    }
}
