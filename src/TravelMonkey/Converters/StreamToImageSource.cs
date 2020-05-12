using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;


namespace TravelMonkey.Converters
{
    [ValueConversion(typeof(Stream), typeof(ImageSource))]
    public class StreamToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Stream == false)
            {
                return default(ImageSource);
            }

            var input = (Stream)value;
            var param = (object)parameter;

            // TODO: Put your value conversion logic here.


            var source = ImageSource.FromStream(() => input);
            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}