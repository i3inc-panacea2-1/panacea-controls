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
    public partial class MaterialIcon : Control
    {
        static MaterialIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaterialIcon), new FrameworkPropertyMetadata(typeof(MaterialIcon)));
        }

        
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(MaterialIconType), typeof(MaterialIcon), new FrameworkPropertyMetadata(MaterialIconType.ic_none, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(MaterialIcon.OnIconChanged)));
        public static readonly DependencyProperty IconResourceProperty = DependencyProperty.Register(nameof(IconResource), typeof(object), typeof(MaterialIcon), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public MaterialIcon()
        {

        }

        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MaterialIconType newValue = (MaterialIconType)e.NewValue;
            if (newValue == MaterialIconType.ic_none)
                return;
            string str = newValue.ToString();
            object resource = ResManager.Resources[str];
            d.SetValue(IconResourceProperty, resource);
        }

        public MaterialIconType Icon
        {
            get
            {
                return (MaterialIconType)GetValue(IconProperty);
            }
            set
            {
                SetValue(IconProperty, value);
            }
        }

        public object IconResource
        {
            get
            {
                return GetValue(IconResourceProperty);
            }
            set
            {
                SetValue(IconResourceProperty, value);
            }
        }
    }


    internal static class ResManager
    {
        public static ResourceDictionary Resources = new ResourceDictionary();

        static ResManager()
        {
            Resources.Source = new Uri("pack://application:,,,/Panacea.Controls;component/MaterialIconResources.xaml", UriKind.Absolute);
        }
    }
}
