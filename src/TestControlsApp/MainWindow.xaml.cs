using Panacea.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
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


        public string ValidatedText
        {
            get { return (string)GetValue(ValidatedTextProperty); }
            set { SetValue(ValidatedTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValidatedText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValidatedTextProperty =
            DependencyProperty.Register("ValidatedText", typeof(string), typeof(MainWindow), new PropertyMetadata(null));


        public MainWindow()
        {
            InitializeComponent();
            dialog = new DialogService(this);
            foreach (var info in CultureInfo.GetCultures(CultureTypes.AllCultures).Take(10))
            {
                Combo.Items.Add(info);
            }


        }
        IDialogService dialog;
        private void Toast_Click(object sender, RoutedEventArgs e)
        {
            ToastNotification not = new ToastNotification("test message");
            //not.Opacity = 0;
            not.Show();
        }

        private void Dialog_Click(object sender, RoutedEventArgs e)
        {
            dialog.Show("Test Title",new TextBlock() { Text = "Test Messsage" }, null, null, "Yes",
                 new RelayCommand((args) => {
                    ToastNotification not = new ToastNotification("YES");
                    not.Show();
                }));
        }

        private void Dialog_Click2(object sender, RoutedEventArgs e)
        {
            dialog.Show("Test Title", new TextBlock() { Text = "Test Messsage" }, "No", new RelayCommand((args)=>
            {
                ToastNotification not = new ToastNotification("NO");
                not.Show();
            }), "Yes",
                   new RelayCommand((args) => {
                       ToastNotification not = new ToastNotification("YES");
                       not.Show();
                   }));
        }
    }
}
