using System;
using System.Globalization;
using System.Windows.Data;

namespace Panacea.Controls.Converters
{
    internal class TransformationConverter : IMultiValueConverter
    {
        StringToRangeConverter _stringToRangeConverter= new StringToRangeConverter();
        ZeroToOneRangeConverterConverter _zeroToOneRangeConverterConverter = new ZeroToOneRangeConverterConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var res = _stringToRangeConverter.Convert(new object[] { values[0], values[1], }, targetType, parameter, culture);
            res = _zeroToOneRangeConverterConverter.Convert(new object[] { res, values[2], values[3] }, targetType, parameter, culture);
            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new Exception("Not implemented");
        }
    }

}
