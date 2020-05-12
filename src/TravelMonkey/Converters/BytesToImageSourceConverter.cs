using System;
using System.Globalization;
using System.Threading.Tasks;
using TravelMonkey.Converters;
using TravelMonkey.Core.Helpers;
using Xamarin.Forms;


namespace TravelMonkey.Converters
{
    [ValueConversion(typeof(Byte[]), typeof(ImageSource))]
    public class BytesToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Byte[] == false)
            {
                return default(ImageSource);
            }
            var input = (Byte[])value;
            var param = (object)parameter;

            // TODO: Put your value conversion logic here.
            System.IO.Stream stream = null;
            Task.Run(async () =>
            {
                //TODO how can I get around converting here? Async == Bad
                stream = await input.ToStream();
            }).Wait();

            var image = ImageSource.FromStream(() => stream);

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}