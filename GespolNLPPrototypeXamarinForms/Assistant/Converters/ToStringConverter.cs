using System;
using System.Globalization;
using Xamarin.Forms;
using XamarinFormsAssistant.Assistant.Views.Components;

namespace XamarinFormsAssistant.Assistant.Converters
{
    public class ToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AnimatableToStringConverter : IValueConverter
    {
       
        private AnimatableViewCell _animatableViewCell;
        public AnimatableToStringConverter(AnimatableViewCell animatableViewCell)
        {
            _animatableViewCell = animatableViewCell;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _animatableViewCell.DoAnimation = false;
            //TODO: memento?
            if (!string.IsNullOrEmpty((string)value) && !_animatableViewCell.AssistantCell.OldValue.Equals((string)value))
            {
                _animatableViewCell.AssistantCell.OldValue = (string)value;
                if(!((string)value).Contains("*Sin valor*")) _animatableViewCell.DoAnimation = true;
                //var propertyHeader = ((string)value).Split(':').FirstOrDefault();
                //MessagingCenter.Send((string)value,$"{AssistantMessaging.PropertyChanged}{propertyHeader}");  
            }
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}