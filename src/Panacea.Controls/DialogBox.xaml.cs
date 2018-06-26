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
        public DialogBox(string text, PanaceaWindow owner):base(owner)
        {
            InitializeComponent();

            Text = text;

            Width = Screen.PrimaryScreen.WorkingArea.Width-20;
            Height = Screen.PrimaryScreen.WorkingArea.Height-20;
            Left = Screen.PrimaryScreen.WorkingArea.Left+10;
            Top = Screen.PrimaryScreen.WorkingArea.Top+10;

            //WindowStartupLocation = WindowStartupLocation.CenterOwner;
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
            Close();
        }
    }
}
