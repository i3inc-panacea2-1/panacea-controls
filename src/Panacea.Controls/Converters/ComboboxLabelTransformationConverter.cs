using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Panacea.Controls.Converters
{
    internal class ComboboxLabelTransformationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var currentZeroToOneValue = (double)values[0];
            var focused = (bool)values[3];
            var isKeyboardFocusWithin = (bool)values[4];
            var selectedValue = values[5]?.ToString();
            var selectedItem = values[6];
            if (!string.IsNullOrEmpty(selectedValue) || selectedItem != null)
            {
                currentZeroToOneValue = 0.0;
            }
            
            var newMinimum = (double)values[1];
            var newMaximum = (double)values[2];
            var res = currentZeroToOneValue * (newMaximum - newMinimum) / 1 + newMinimum;
            return res;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
