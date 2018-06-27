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
        public DialogBox(string title, string text, PanaceaWindow owner, bool fitToContent = true):base(owner)
        {
            InitializeComponent();

            Text = text;
            Title = title;

            if (fitToContent == true)
            {
                MaxWidth = Screen.PrimaryScreen.WorkingArea.Width - 20;
                MaxHeight = Screen.PrimaryScreen.WorkingArea.Height - 20;
                MinWidth = Screen.PrimaryScreen.WorkingArea.Width * 0.2;
                MinHeight = Screen.PrimaryScreen.WorkingArea.Height * 0.2;
                SizeToContent = SizeToContent.WidthAndHeight;
            }
            else
            {
                Width = Screen.PrimaryScreen.WorkingArea.Width - 20;
                Height = Screen.PrimaryScreen.WorkingArea.Height - 20;
            }

            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DialogBox), new PropertyMetadata(null));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Owner.IsEnabled = true;
            Owner.Opacity = 1;
            Close();
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            Owner.IsEnabled = false;
            Owner.Opacity = 0.5;
        }
    }
}
