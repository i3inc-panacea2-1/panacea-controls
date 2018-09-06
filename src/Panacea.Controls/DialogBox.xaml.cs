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
        public DialogBox(string title, string text, Window owner, bool fitToContent = true):base()
        {
            InitializeComponent();

            Owner = owner;
            Text = text;
            Title = title;
            PositiveText = "Close";
            PositiveCommand = new RelayCommand(() =>
            {
                AnimatedClose();
            });
            
            ResizeToOwner();
            
        }
        public DialogBox(string title, string text, Window owner, string negativeText, string positiveTest,
            ICommand negativeCommand, ICommand positiveCommand, bool fitToContent = true) : base()
        {
            InitializeComponent();

            Owner = owner;
            Text = text;
            Title = title;
            NegativeButtonVisibility = Visibility.Visible;
            NegativeText = negativeText;
            PositiveText = positiveTest;
            NegativeCommand = new RelayCommand(()=> {
                negativeCommand.Execute(null);
                AnimatedClose();
            });
            PositiveCommand = new RelayCommand(() => {
                positiveCommand.Execute(null);
                AnimatedClose();
            });

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



        public Visibility NegativeButtonVisibility
        {
            get { return (Visibility)GetValue(NegativeButtonVisibilityProperty); }
            set { SetValue(NegativeButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NegativeButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NegativeButtonVisibilityProperty =
            DependencyProperty.Register("NegativeButtonVisibility", typeof(Visibility), typeof(DialogBox), new PropertyMetadata(Visibility.Collapsed));



        public Visibility PositiveButtonVisibility
        {
            get { return (Visibility)GetValue(PositiveButtonVisibilityProperty); }
            set { SetValue(PositiveButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PositiveButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositiveButtonVisibilityProperty =
            DependencyProperty.Register("PositiveButtonVisibility", typeof(Visibility), typeof(DialogBox), new PropertyMetadata(Visibility.Visible));




        public string NegativeText
        {
            get { return (string)GetValue(NegativeTextProperty); }
            set { SetValue(NegativeTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NegativeText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NegativeTextProperty =
            DependencyProperty.Register("NegativeText", typeof(string), typeof(DialogBox), new PropertyMetadata(null));



        public string PositiveText
        {
            get { return (string)GetValue(PositiveTextProperty); }
            set { SetValue(PositiveTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PositiveText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositiveTextProperty =
            DependencyProperty.Register("PositiveText", typeof(string), typeof(DialogBox), new PropertyMetadata(null));



        public ICommand PositiveCommand
        {
            get { return (ICommand)GetValue(PositiveCommandProperty); }
            set { SetValue(PositiveCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PositiveCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositiveCommandProperty =
            DependencyProperty.Register("PositiveCommand", typeof(ICommand), typeof(DialogBox), new PropertyMetadata(null));



        public ICommand NegativeCommand
        {
            get { return (ICommand)GetValue(NegativeCommandProperty); }
            set { SetValue(NegativeCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NegativeCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NegativeCommandProperty =
            DependencyProperty.Register("NegativeCommand", typeof(ICommand), typeof(DialogBox), new PropertyMetadata(null));




        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DialogBox), new PropertyMetadata(null));


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
