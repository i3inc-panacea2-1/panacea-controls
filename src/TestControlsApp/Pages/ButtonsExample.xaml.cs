using Panacea.Controls;
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

namespace TestControlsApp.Pages
{
    /// <summary>
    /// Interaction logic for ButtonsExample.xaml
    /// </summary>
    public partial class ButtonsExample : UserControl
    {
        public ButtonsExample()
        {
            TestAsyncCommand = new AsyncCommand(async (args) =>
             {
                 await Task.Delay(2000);
             }, (args) => true);
            InitializeComponent();
        }

        public IAsyncCommand TestAsyncCommand { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var w = new OverlayWindow(Window.GetWindow(this));
            w.AllowsTransparency = true;
            w.Opacity = .5;
            w.Background = Brushes.Black;
            w.Show();
        }
    }
}
