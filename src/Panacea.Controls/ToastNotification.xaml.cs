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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Panacea.Controls
{
    /// <summary>
    /// Interaction logic for ToastNotification.xaml
    /// </summary>
    public partial class ToastNotification : DialogBaseWindow
    {
        public ToastNotification(string text, PanaceaWindow owner) : base(owner)
        {
            InitializeComponent();
            Text = text;
            SizeToContent = SizeToContent.WidthAndHeight;
            Left = Screen.PrimaryScreen.WorkingArea.Left + Screen.PrimaryScreen.WorkingArea.Width / 2;
            Top = Screen.PrimaryScreen.WorkingArea.Top + Screen.PrimaryScreen.WorkingArea.Height - 50;
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

            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation(0.85, 0, new Duration(new TimeSpan(0, 0, 0, 0, 200)));
            //< DoubleAnimation Duration = "00:00:00.200" Storyboard.TargetProperty = "Opacity" From = "0" To = "0.85" />
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            sb.Children.Add(da);

            this.BeginStoryboard(sb);

            await Task.Delay(200);

            Close();
        }
        
    }
}
