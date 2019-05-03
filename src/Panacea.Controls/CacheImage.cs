using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Panacea.Controls
{
    public class CacheImage : Image
    {
        public static event EventHandler<string> ImageUrlChanged;

        static CacheImage()
        {
          
        }

        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageUrl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageUrlProperty =
            DependencyProperty.Register("ImageUrl", typeof(string), typeof(CacheImage), new PropertyMetadata(null, ImageUrlChanged2));

        private static void ImageUrlChanged2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageUrlChanged?.Invoke(d, e.NewValue.ToString());
        }
    }
}
