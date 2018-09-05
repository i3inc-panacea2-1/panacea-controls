using System;
using System.Globalization;
using System.Windows.Data;

namespace Panacea.Controls.Converters
{
    /// <summary>
    /// Converts a value between 0 and 1 to a value of a different range that has the same distance from start and end. 
    /// First value: the current value between 0 and 1.
    /// Second value: the minimum of the new range.
    /// Third value: the maximum of the new range.
    /// Example: a value of 0.5 will become 1 if the new minimum is 0 and new maximum is 2.
    /// </summary>
    public class ZeroToOneRangeConverterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var currentZeroToOneValue = (double)values[0];
            var newMinimum = (double)values[1];
            var newMaximum = (double)values[2];
            var res = currentZeroToOneValue * (newMaximum - newMinimum) / 1 + newMinimum;
            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new Exception("Not implemented");
        }
    }
}
