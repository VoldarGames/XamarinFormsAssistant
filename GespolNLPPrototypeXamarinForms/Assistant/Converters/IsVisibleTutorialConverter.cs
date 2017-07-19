using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinFormsAssistant.Assistant.Converters
{
    internal class IsVisibleTutorialConverter : IValueConverter
    {
        private readonly bool _inverse;
        public IsVisibleTutorialConverter(bool inverse = false)
        {
            _inverse = inverse;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(_inverse) return !string.IsNullOrEmpty((string) value);
            return string.IsNullOrEmpty((string) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}