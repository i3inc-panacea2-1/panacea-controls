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
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            InitializeComponent();
            selectedColor = Color.FromRgb(255, 0, 0);
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
            DependencyProperty.Register("SelectedColorBrush", typeof(Brush), typeof(ColorPicker), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 0, 0))));


        private Boolean IsMouseDown = false;
        private void UpdateColor()
        {
            // Test to ensure we do not get bad mouse positions along the edges
            int x = (int)Mouse.GetPosition(main).X;
            int y = (int)Mouse.GetPosition(main).Y;
            if ((x < 0) || (y < 0)) return;
            int r, g, b;
            r = g = b = 255;
            var x1 = x/mainCanvas.ActualWidth;

            if (x1<=0.165)
            {
                //red to yellow
                r = 255;
                g = Convert.ToInt32(x1*255/0.165);
                b = 0;
            }
            else if (x1<=0.33)
            {
                //yellow to green
                r =255 - Convert.ToInt32((x1 - 0.165) * 255 / 0.165);
                g = 255;
                b = 0;
            }
            else if (x1 <= 0.495)
            {
                //green to cyan
                r = 0;
                g = 255;
                b = Convert.ToInt32((x1 - 0.33) * 255 / 0.165);
            }
            else if (x1 <= 0.66)
            {
                //cyan to blue
                r = 0;
                g = 255 - Convert.ToInt32((x1 - 0.495) * 255 / 0.165);
                b = 255;
            }
            else if (x1 <= 0.825)
            {
                //blue to violet
                r = Convert.ToInt32((x1 - 0.66) * 255 / 0.165);
                g = 0;
                b = 255;
            }
            else
            {
                //violet to red
                r = 255;
                g = 0;
                b = 255 - Convert.ToInt32((x1 - 0.825) * 255 / 0.165);
            }

            SelectedColor = Color.FromRgb(byte.Parse(r.ToString()), byte.Parse(g.ToString()), byte.Parse(b.ToString()));//ColorImports.GetPixelColor(main.PointToScreen(new Point(x, y)));
        }
        private void CanvasImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                //Update the mouse cursor position and the Selected Color
                ellipsePixel.SetValue(Canvas.LeftProperty, (double)(Mouse.GetPosition(main).X - (ellipsePixel.Width / 2.0)));
                ellipsePixel.SetValue(Canvas.TopProperty, (double)(Mouse.GetPosition(main).Y - (ellipsePixel.Width / 2.0)));
                main.InvalidateVisual();
            }
        }

        /// <summary>
        /// Handle MouseDown event.
        /// </summary>
        private void CanvasImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = true;
            //Update the mouse cursor position and the Selected Color
            ellipsePixel.SetValue(Canvas.LeftProperty, (double)(Mouse.GetPosition(main).X - (ellipsePixel.Width / 2.0)));
            ellipsePixel.SetValue(Canvas.TopProperty, (double)(Mouse.GetPosition(main).Y - (ellipsePixel.Width / 2.0)));
            main.InvalidateVisual();
        }

        /// <summary>
        /// Handle MouseUp event.
        /// </summary>
        private void CanvasImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = false;
            UpdateColor();
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



        public Color RedColor
        {
            get { return (Color)GetValue(RedColorProperty); }
            set { SetValue(RedColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RedColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RedColorProperty =
            DependencyProperty.Register("RedColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Color.FromRgb(255, 0, 0)));



        public Color YellowColor
        {
            get { return (Color)GetValue(YellowColorProperty); }
            set { SetValue(YellowColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for YellowColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YellowColorProperty =
            DependencyProperty.Register("YellowColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Color.FromRgb(255,255,0)));



        public Color GreenColor
        {
            get { return (Color)GetValue(GreenColorProperty); }
            set { SetValue(GreenColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GreenColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GreenColorProperty =
            DependencyProperty.Register("GreenColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Color.FromRgb(0,255,0)));




        public Color CyanColor
        {
            get { return (Color)GetValue(CyanColorProperty); }
            set { SetValue(CyanColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CyanColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CyanColorProperty =
            DependencyProperty.Register("CyanColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Color.FromRgb(0,255,255)));



        public Color BlueColor
        {
            get { return (Color)GetValue(BlueColorProperty); }
            set { SetValue(BlueColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BlueColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlueColorProperty =
            DependencyProperty.Register("BlueColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Color.FromRgb(0,0,255)));




        public Color VioletColor
        {
            get { return (Color)GetValue(VioletColorProperty); }
            set { SetValue(VioletColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VioletColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VioletColorProperty =
            DependencyProperty.Register("VioletColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Color.FromRgb(255,0,255)));





    }

    public class ColorEventArgs
    {
        public ColorEventArgs(Color color)
        {
            Color = color;
        }
        public Color Color { get; set; }
    }
}
