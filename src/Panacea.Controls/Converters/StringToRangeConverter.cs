using System;
using System.Globalization;
using System.Windows.Data;

namespace Panacea.Controls.Converters
{
    /// <summary>
    /// Returns 0 for a range of 0 to 1 if the given string is not empty, and the second parameter if the string is empty.
    /// </summary>
    internal class StringToRangeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(values[0]?.ToString()))
            {
                return 0.0;
            }
            return (double)values[1];

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new Exception("Not implemented");
        }
    }
}
