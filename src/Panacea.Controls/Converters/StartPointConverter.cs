﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Panacea.Controls.Converters
{
    public class StartPointConverter : IValueConverter
    {
        [Obsolete]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double && ((double)value > 0.0))
            {
                return new Point((double)value / 2, 0);
            }

            return new Point();
        }

        [Obsolete]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

    }


    public class ArcSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double && ((double)value > 0.0))
            {
                return new Size((double)value / 2, (double)value / 2);
            }

            return new Point();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    public class ArcEndPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var actualWidth = values[0].ExtractDouble();
            var value = values[1].ExtractDouble();
            var minimum = values[2].ExtractDouble();
            var maximum = values[3].ExtractDouble();

            if (new[] { actualWidth, value, minimum, maximum }.AnyNan())
                return Binding.DoNothing;

            if (values.Length == 5)
            {
                var fullIndeterminateScaling = values[4].ExtractDouble();
                if (!double.IsNaN(fullIndeterminateScaling) && fullIndeterminateScaling > 0.0)
                {
                    value = (maximum - minimum) * fullIndeterminateScaling;
                }
            }

            var percent = maximum <= minimum ? 1.0 : (value - minimum) / (maximum - minimum);
            var degrees = 360 * percent;
            var radians = degrees * (Math.PI / 180);

            var centre = new Point(actualWidth / 2, actualWidth / 2);
            var hypotenuseRadius = (actualWidth / 2);

            var adjacent = Math.Cos(radians) * hypotenuseRadius;
            var opposite = Math.Sin(radians) * hypotenuseRadius;

            return new Point(centre.X + opposite, centre.Y - adjacent);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class LargeArcConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = values[0].ExtractDouble();
            var minimum = values[1].ExtractDouble();
            var maximum = values[2].ExtractDouble();

            if (new[] { value, minimum, maximum }.AnyNan())
                return Binding.DoNothing;

            if (values.Length == 4)
            {
                var fullIndeterminateScaling = values[3].ExtractDouble();
                if (!double.IsNaN(fullIndeterminateScaling) && fullIndeterminateScaling > 0.0)
                {
                    value = (maximum - minimum) * fullIndeterminateScaling;
                }
            }

            var percent = maximum <= minimum ? 1.0 : (value - minimum) / (maximum - minimum);

            return percent > 0.5;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RotateTransformCentreConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //value == actual width
            return (double)value / 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }


    internal static class LocalEx
    {
        public static double ExtractDouble(this object val)
        {
            var d = val as double? ?? double.NaN;
            return double.IsInfinity(d) ? double.NaN : d;
        }


        public static bool AnyNan(this IEnumerable<double> vals)
        {
            return vals.Any(double.IsNaN);
        }
    }
}
