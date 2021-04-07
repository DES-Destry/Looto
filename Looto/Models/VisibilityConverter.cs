using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Looto.Models
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    class VisibilityConverter : MarkupExtension, IValueConverter
    {
        private static VisibilityConverter _converter = null;
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
            {
                _converter = new VisibilityConverter();
            }

            return _converter;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Hidden;
            }

            if (bool.TryParse(value.ToString(), out bool visible))
            {
                if (visible)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null)
                {
                    return false;
                }

                Visibility visibility = (Visibility)value;

                if (visibility == Visibility.Visible)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
