using System;
using System.Globalization;
using TravelMonkey.Core.Converters;
using Xamarin.Forms;

namespace TravelMonkey.Converters
{
    public class LanguageCodeToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).ToCountryName();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not needed
            throw new NotImplementedException();
        }
    }
}