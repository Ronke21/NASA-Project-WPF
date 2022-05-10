using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace NASA_PL.Converters
{
    internal class DictToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value switch
            {
                null => Visibility.Collapsed,
                Dictionary<string, string> dict => dict.Count == 0 ? Visibility.Visible : Visibility.Collapsed,
                _ => Visibility.Collapsed
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
