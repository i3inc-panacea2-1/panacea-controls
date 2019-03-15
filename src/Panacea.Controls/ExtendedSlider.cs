using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Panacea.Controls
{
    public class ExtendedSlider : Slider
    {
        static ExtendedSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExtendedSlider), new FrameworkPropertyMetadata(typeof(ExtendedSlider)));
        }

        public static readonly DependencyProperty SliderWidthProperty = DependencyProperty.Register(
            "SliderWidth", typeof(double), typeof(ExtendedSlider), new PropertyMetadata(default(double)));

        public double SliderWidth
        {
            get { return (double)GetValue(SliderWidthProperty); }
            set { SetValue(SliderWidthProperty, value); }
        }
        public static readonly DependencyProperty AddProperty = DependencyProperty.Register(
            "Add", typeof(int), typeof(ExtendedSlider), new PropertyMetadata(default(int)));

        public int Add
        {
            get { return (int)GetValue(AddProperty); }
            set { SetValue(AddProperty, value); }
        }
    }
}
