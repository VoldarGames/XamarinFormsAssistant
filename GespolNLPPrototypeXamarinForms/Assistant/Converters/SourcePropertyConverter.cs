using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinFormsAssistant.Assistant.Converters
{
    public class SourcePropertyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {

                if (!((string) value).Contains("*Sin valor*")) return "checkmark.png";

            }
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}