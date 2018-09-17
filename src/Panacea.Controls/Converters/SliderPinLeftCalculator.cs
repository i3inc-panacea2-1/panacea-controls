using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Panacea.Controls.Converters
{
    internal class SliderPinLeftCalculator : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var fixedSpace = (double)values[0];
            var thumb = values[1] as FrameworkElement;
            if (thumb == null) return 0;
            var border = values[2] as FrameworkElement;
            if (border == null) return 0;
            var thumbLeft = thumb.TranslatePoint(new Point(0, 0), border).X;
            var pinHalfSize = ((double)values[3]) / 2;
            return fixedSpace + thumbLeft - pinHalfSize;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
