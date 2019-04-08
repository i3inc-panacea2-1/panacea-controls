using System.Windows;
using System.Windows.Controls;

namespace Panacea.Controls
{

        public class FixedRatioControl : ContentControl
        {
            public static readonly DependencyProperty RatioProperty = DependencyProperty.Register(nameof(Ratio),
                typeof(double), typeof(FixedRatioControl), new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsMeasure));

            public double Ratio
            {
                get { return (double)GetValue(RatioProperty); }
                set { SetValue(RatioProperty, value); }
            }


            protected override Size MeasureOverride(Size constraint)
            {
                var res = base.MeasureOverride(constraint);
                return new Size(res.Width, res.Width * Ratio);
            }
        }
    
}
