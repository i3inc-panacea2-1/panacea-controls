using Panacea.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class MainWindow : Window
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

            EventManager.RegisterClassHandler(
                typeof(UIElement),
                Keyboard.PreviewGotKeyboardFocusEvent,
                (KeyboardFocusChangedEventHandler)OnPreviewGotKeyboardFocus);


        }

        void OnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //Debug.WriteLine(e.NewFocus.GetType().Name);
        }
        
    }
}
