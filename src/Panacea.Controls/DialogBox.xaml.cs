using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Panacea.Controls
{
    /// <summary>
    /// Interaction logic for DialogBox.xaml
    /// </summary>
    public partial class DialogBox : DialogBaseWindow
    {
        public DialogBox(string title, string text, PanaceaWindow owner, bool fitToContent = true):base()
        {
            InitializeComponent();

            Owner = owner;
            Text = text;
            Title = title;
            
            ResizeToOwner();
            
        }
        void ResizeToOwner()
        {
            var element = Owner.Content as FrameworkElement;
            Width = element.ActualWidth;
            Height = element.ActualHeight;

            var p = element.PointToScreen(new Point(0, 0));
            Left = p.X;
            Top = p.Y;
        }

        private void Owner_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeToOwner();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DialogBox), new PropertyMetadata(null));

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            AnimatedClose();
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            Owner.SizeChanged += Owner_SizeChanged;
            Owner.LocationChanged += Owner_LocationChanged;
        }

        private void Owner_LocationChanged(object sender, EventArgs e)
        {
            ResizeToOwner();
        }

        private void Main_Unloaded(object sender, RoutedEventArgs e)
        {
            Owner.SizeChanged -= Owner_SizeChanged;
            Owner.LocationChanged -= Owner_LocationChanged;
        }
    }
}
