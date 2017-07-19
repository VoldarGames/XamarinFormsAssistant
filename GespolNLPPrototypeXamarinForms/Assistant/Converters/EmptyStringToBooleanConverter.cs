using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinFormsAssistant.Assistant.Converters
{
    public class EmptyStringToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = (string)value;
            return !string.IsNullOrEmpty(result);
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}