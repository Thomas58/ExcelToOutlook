using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ExcelToOutlook.Tools
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case Visibility.Visible:
                    return true;
                default:
                    return false;
            }
        }
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class InvertBoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Visibility.Hidden;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case Visibility.Visible:
                    return false;
                default:
                    return true;
            }
        }
    }
}