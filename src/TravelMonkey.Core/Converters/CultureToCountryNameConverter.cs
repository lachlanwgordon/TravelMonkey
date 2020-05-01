using System;
using System.Globalization;

namespace TravelMonkey.Core.Converters
{
    public static class CultureToCountryNameConverter
    {
        public static string ToCountryName(this string value)
        {
            if (string.IsNullOrWhiteSpace(value as string))
                return "";

            var cultureInfo = new CultureInfo((string)value);

            if (cultureInfo == null)
                return "";

            return cultureInfo.EnglishName;
        }
    }
}
