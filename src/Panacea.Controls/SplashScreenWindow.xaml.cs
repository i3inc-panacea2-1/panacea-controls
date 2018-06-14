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
using System.Windows.Shapes;

namespace Panacea.Controls
{
    /// <summary>
    /// Interaction logic for SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : Window
    {
        private readonly Func<Task> _loadingFunc;

        public SplashScreenWindow()
        {
            InitializeComponent();
        }
        public SplashScreenWindow(Brush imageColor, Geometry imageData, string text, Func<Task> loadingFunc) :this()
        {
            _loadingFunc = loadingFunc;
            ImageColor = imageColor;
            ImageData = imageData;
            Text = text;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _loadingFunc();
            Close();
        }


        public Brush ImageColor
        {
            get { return (Brush)GetValue(ImageColorProperty); }
            set { SetValue(ImageColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageColorProperty =
            DependencyProperty.Register("ImageColor", typeof(Brush), typeof(SplashScreenWindow), new PropertyMetadata(Brushes.White));


        public Geometry ImageData
        {
            get { return (Geometry)GetValue(ImageDataProperty); }
            set { SetValue(ImageDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageDataProperty =
            DependencyProperty.Register("ImageData", typeof(Geometry), typeof(SplashScreenWindow), new PropertyMetadata(null));




        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SplashScreenWindow), new PropertyMetadata(""));


    }
}
