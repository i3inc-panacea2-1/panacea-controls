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
    public partial class MaterialIcon : TextBlock
    {
        static MaterialIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaterialIcon), new FrameworkPropertyMetadata(typeof(MaterialIcon)));
            TextProperty.OverrideMetadata(typeof(MaterialIcon), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnTextChanged)));
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as MaterialIcon;
            element.IconVisibility = element.Text == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public Visibility IconVisibility
        {
            get { return (Visibility)GetValue(IconVisibilityProperty); }
            private set { SetValue(IconVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register("IconVisibility", typeof(Visibility), typeof(MaterialIcon), new PropertyMetadata(Visibility.Collapsed));



        public MaterialIcon()
        {

        }
       
    }
    
}
