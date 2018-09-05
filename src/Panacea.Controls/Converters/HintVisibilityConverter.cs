using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Panacea.Controls.Converters
{
    internal class HintVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(values[0]?.ToString()) || !(bool)values[1])
            {
                return Visibility.Hidden;
            }
            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new Exception("Not implemented");
        }
    }
}
