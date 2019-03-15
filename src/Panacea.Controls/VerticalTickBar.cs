using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Panacea.Controls
{
    public class VerticalTickBar : TickBar
    {
        public static readonly DependencyProperty AddProperty = DependencyProperty.Register(
            "Add", typeof(int), typeof(VerticalTickBar), new PropertyMetadata(default(int)));

        public int Add
        {
            get { return (int)GetValue(AddProperty); }
            set { SetValue(AddProperty, value); }
        }

        protected override void OnRender(DrawingContext dc)
        {
            Size size = new Size(base.ActualWidth, base.ActualHeight);

            int tickCount, range = 0;

            if (Maximum - Minimum < 4)
            {
                tickCount = (int)(Maximum - Minimum);
                range = 1;
            }
            else
            {
                range = (int)((Maximum - Minimum) / 4);
                tickCount = 4;
            }


            double tickFrequencySize = size.Height / tickCount;
            int index = (int)((Maximum - Minimum) / range);
            // Draw each tick text
            for (var i = Minimum; i <= Maximum; i += range)
            {
                int value = Convert.ToInt32(i);

                string text = Convert.ToString(value + Add, 10);
                FormattedText formattedText = new FormattedText(text, CultureInfo.GetCultureInfo("en-us"),
                                                      FlowDirection.LeftToRight, new Typeface("Segoe UI Light"), 14,
                                                      Brushes.Gray, VisualTreeHelper.GetDpi(this).PixelsPerDip);

                dc.DrawText(formattedText, new Point(10, (tickFrequencySize * index - 12)));
                index--;
            }
        }

    }
}
