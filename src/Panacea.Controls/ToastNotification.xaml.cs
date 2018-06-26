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
    /// Interaction logic for ToastNotification.xaml
    /// </summary>
    public partial class ToastNotification : Window
    {
        public ToastNotification(string text)
        {
            InitializeComponent();
            Text = text;
        }


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ToastNotification), new PropertyMetadata(null));

        private async void Main_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(3000);
            Close();
        }
    }
}
