using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Panacea.Controls
{
    /// <summary>
    /// Interaction logic for WhitePicker.xaml
    /// </summary>
    public partial class WhitePicker : UserControl
    {
        public WhitePicker()
        {
            InitializeComponent();

            temperatureSlider.Value = 6600;
            selectedColor = Color.FromRgb(255, 255, 255);
        }
        private Color selectedColor;
        public Color SelectedColor
        {
            get { return selectedColor; }
            private set
            {
                if (selectedColor != value)
                {
                    this.selectedColor = value;
                    SelectedColorBrush = new SolidColorBrush(value);
                    ColorChanged(value);
                }
            }
        }

        public Brush SelectedColorBrush
        {
            get { return (Brush)GetValue(SelectedColorBrushProperty); }
            set { SetValue(SelectedColorBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedColorBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedColorBrushProperty =
            DependencyProperty.Register("SelectedColorBrush", typeof(Brush), typeof(WhitePicker), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 255, 255))));

        private void UpdateColor()
        {
            //algorithm from http://www.tannerhelland.com/4435/convert-temperature-rgb-algorithm-code/
            int r, g, b;
            r = g = b = 255;
            //kelvin from 1500 to 15000
            var t = temperatureSlider.Value / 100;
            if (t <= 66)
            {
                r = 255;
                int temp = Convert.ToInt32(t);
                g = Convert.ToInt32(99.4708025861 * Math.Log(temp) - 161.1195681661);
                if (g < 0)
                {
                    g = 0;
                }
                if (g > 255)
                {
                    g = 255;
                }
                if (t<=19)
                {
                    b = 0;
                }
                else
                {
                    temp = Convert.ToInt32(t - 10);
                    b = Convert.ToInt32(138.5177312231 * Math.Log(temp) - 305.0447927307);
                    if (b < 0)
                    {
                        b = 0;
                    }
                    if (b > 255)
                    {
                        b = 255;
                    }
                }
            }
            else
            {
                int temp = Convert.ToInt32(t - 60);
                r = Convert.ToInt32(329.698727446 * Math.Pow(temp, -0.1332047592));
                if (r < 0)
                {
                    r = 0;
                }
                if (r>255)
                {
                    r = 255;
                }
                g = Convert.ToInt32(288.1221695283 * Math.Pow(temp, -0.0755148492));
                if (g < 0)
                {
                    g = 0;
                }
                if (g > 255)
                {
                    g = 255;
                }
                b = 255;
            }
            SelectedColor = Color.FromRgb(byte.Parse(r.ToString()), byte.Parse(g.ToString()), byte.Parse(b.ToString()));
        }

        public delegate void ColorChangedHandler(object sender, ColorEventArgs e);
        public event ColorChangedHandler OnColorChanged;

        private void ColorChanged(Color color)
        {
            // Make sure someone is listening to event
            if (OnColorChanged == null) return;

            ColorEventArgs args = new ColorEventArgs(color);
            OnColorChanged(this, args);
        }

        public Color White
        {
            get { return (Color)GetValue(WhiteProperty); }
            set { SetValue(WhiteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for White.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WhiteProperty =
            DependencyProperty.Register("White", typeof(Color), typeof(WhitePicker), new PropertyMetadata(Color.FromRgb(255, 255, 255)));



        public Color ColdWhite
        {
            get { return (Color)GetValue(ColdWhiteProperty); }
            set { SetValue(ColdWhiteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColdWhite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColdWhiteProperty =
            DependencyProperty.Register("ColdWhite", typeof(Color), typeof(WhitePicker), new PropertyMetadata(Color.FromRgb(255, 108, 0)));



        public Color HotWhite
        {
            get { return (Color)GetValue(HotWhiteProperty); }
            set { SetValue(HotWhiteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HotWhite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HotWhiteProperty =
            DependencyProperty.Register("HotWhite", typeof(Color), typeof(WhitePicker), new PropertyMetadata(Color.FromRgb(181, 205, 255)));

        

        private void TemperatureSlider_LostMouseCapture(object sender, MouseEventArgs e)
        {
            UpdateColor();
        }
    }
}
