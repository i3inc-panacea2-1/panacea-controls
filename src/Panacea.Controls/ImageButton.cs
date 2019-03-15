using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Panacea.Controls
{
    public class ImageButton: Button
    {
        public static readonly DependencyProperty ImageProperty =
DependencyProperty.Register("Image", typeof(string),
typeof(ImageButton),
new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register("TextVisibility", typeof(Visibility),
                typeof(ImageButton),
                new FrameworkPropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(String),
                typeof(ImageButton),
                new FrameworkPropertyMetadata(""));
         /*
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Brush),
                typeof(ImageButton),
                new FrameworkPropertyMetadata(Brushes.White));

       

        public Brush TextColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
        */
        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public Visibility TextVisibility
        {
            get { return (Visibility)GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }
    }
}

