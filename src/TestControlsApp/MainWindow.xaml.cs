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

namespace TestControlsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : PanaceaWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Toast_Click(object sender, RoutedEventArgs e)
        {
            ToastNotification not = new ToastNotification("test message", this);
            //not.Opacity = 0;
            not.Show();
        }

        private void Dialog_Click(object sender, RoutedEventArgs e)
        {
            DialogBox dlg = new DialogBox("Test Messsage", this);
            dlg.Show();
        }
    }
}
