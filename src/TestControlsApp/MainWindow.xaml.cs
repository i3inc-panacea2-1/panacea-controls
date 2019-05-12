using Panacea.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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

namespace TestControlsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public string ValidatedText
        {
            get { return (string)GetValue(ValidatedTextProperty); }
            set { SetValue(ValidatedTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValidatedText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValidatedTextProperty =
            DependencyProperty.Register("ValidatedText", typeof(string), typeof(MainWindow), new PropertyMetadata(null));


        public MainWindow()
        {
            CacheImage.ImageUrlChanged += CacheImage_OnImageUrl;

            InitializeComponent();
            EventManager.RegisterClassHandler(
                typeof(UIElement),
                Keyboard.PreviewGotKeyboardFocusEvent,
                (KeyboardFocusChangedEventHandler)OnPreviewGotKeyboardFocus);

            
        }

        private void CacheImage_OnImageUrl(object sender, string e)
        {
            var image = sender as CacheImage;
            if (!image.IsLoaded)
            {
                image.Loaded += Image_Loaded;
                return;
            }
            var task = SetImage(image, e);
           
        }

        async Task SetImage(CacheImage image, string url)
        {
            var uri = new Uri(new Uri("https://cdn.pixabay.com"), url);
            var download = await new WebClient().DownloadDataTaskAsync(uri);
            var img2 = new BitmapImage();
            img2.BeginInit();
            img2.CreateOptions |= BitmapCreateOptions.IgnoreColorProfile;
            img2.CacheOption = BitmapCacheOption.OnLoad;
            img2.StreamSource = new MemoryStream(download);

            img2.EndInit();
            img2.Freeze();
            image.Source = img2;
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            var image = sender as CacheImage;
            image.Loaded -= Image_Loaded;
            var task = SetImage(image, image.ImageUrl);
        }

        void OnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //Debug.WriteLine(e.NewFocus.GetType().Name);
        }
        
    }
}
