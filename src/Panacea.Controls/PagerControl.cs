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

namespace Panacea.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Panacea.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Panacea.Controls;assembly=Panacea.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:PagerControl/>
    ///
    /// </summary>
    public class PagerControl : Control
    {
        static PagerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PagerControl), new FrameworkPropertyMetadata(typeof(PagerControl)));
        }

        public PagerControl()
        {
            ChangePageCommand = new RelayCommand((args) =>
            {
                CurrentPage = (int)args;
            });
            NextCommand = new RelayCommand((args) =>
            {
                CurrentPage++;
            },
            (args) => CurrentPage < MaxPages);

            PreviousCommand = new RelayCommand((args) =>
            {
                CurrentPage--;
            },
            (args) => CurrentPage > 1);
            FirstCommand = new RelayCommand(args =>
            {
                CurrentPage = 1;
            });
            LastCommand = new RelayCommand(args =>
            {
                CurrentPage = MaxPages;
            });
        }

        public ICommand ChangePageCommand { get;  }


        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(int), typeof(PagerControl), new PropertyMetadata(-1, OnCurrentPageChanged));

        private static void OnCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pager = d as PagerControl;
            pager.DotsVisibility = pager.MaxPages > 0 ? Visibility.Visible : Visibility.Collapsed;
            var lst = new List<int>();
            for (var i = pager.CurrentPage -1; i > 0 && i >= pager.CurrentPage - 2; i--)
            {
                lst.Insert(0, i);
            }
            pager.LeftPart = lst;

            var lst2 = new List<int>();
            for (var i = pager.CurrentPage +1; i <= pager.MaxPages && i <= pager.CurrentPage + 2; i++)
            {
                lst2.Add(i);
            }
            pager.RightPart = lst2;
            pager.FirstPageVisibility = pager.CurrentPage > 3 ? Visibility.Visible : Visibility.Collapsed;

            pager.LastPageVisibility = pager.CurrentPage <= pager.MaxPages - 3 ? Visibility.Visible : Visibility.Collapsed;
        }

        public int MaxPages
        {
            get { return (int)GetValue(MaxPagesProperty); }
            set { SetValue(MaxPagesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxPages.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxPagesProperty =
            DependencyProperty.Register("MaxPages", typeof(int), typeof(PagerControl), new PropertyMetadata(-1, OnCurrentPageChanged));



        public List<int> LeftPart
        {
            get { return (List<int>)GetValue(LeftPartProperty); }
            set { SetValue(LeftPartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftPart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftPartProperty =
            DependencyProperty.Register("LeftPart", typeof(List<int>), typeof(PagerControl), new PropertyMetadata(null));


        public List<int> RightPart
        {
            get { return (List<int>)GetValue(RightPartProperty); }
            set { SetValue(RightPartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightPart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightPartProperty =
            DependencyProperty.Register("RightPart", typeof(List<int>), typeof(PagerControl), new PropertyMetadata(null));

        public Visibility DotsVisibility
        {
            get { return (Visibility)GetValue(DotsVisibilityProperty); }
            set { SetValue(DotsVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DotsVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DotsVisibilityProperty =
            DependencyProperty.Register("DotsVisibility", typeof(Visibility), typeof(PagerControl), new PropertyMetadata(Visibility.Collapsed));




        public Visibility FirstPageVisibility
        {
            get { return (Visibility)GetValue(FirstPageVisibilityProperty); }
            set { SetValue(FirstPageVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FirstPageVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FirstPageVisibilityProperty =
            DependencyProperty.Register("FirstPageVisibility", typeof(Visibility), typeof(PagerControl), new PropertyMetadata(Visibility.Collapsed));


        public Visibility LastPageVisibility
        {
            get { return (Visibility)GetValue(LastPageVisibilityProperty); }
            set { SetValue(LastPageVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FirstPageVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastPageVisibilityProperty =
            DependencyProperty.Register("LastPageVisibility", typeof(Visibility), typeof(PagerControl), new PropertyMetadata(Visibility.Collapsed));



        public ICommand FirstCommand     
        {
            get;
        }


        public ICommand LastCommand
        {
            get;
        }



        public ICommand PreviousCommand
        {
            get;
        }

      
        public ICommand NextCommand
        {
            get;
        }

        
    }
}
