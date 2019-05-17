using Panacea.Controls.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Panacea.Controls
{
    public class Material : DependencyObject
    {
        static Material()
        {


        }


        #region Busy

        public static double GetRelativeFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(RelativeFontSizeProperty);
        }

        public static void SetRelativeFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(RelativeFontSizeProperty, value);
        }

        public static readonly DependencyProperty RelativeFontSizeProperty =
            DependencyProperty.RegisterAttached(
                "RelativeFontSize",
                typeof(double),
                typeof(Material),
                new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender, OnRelativeFontSizeChanged));


        private static void OnRelativeFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("s: " + e.NewValue);
            if (GetRelativeFontSizeRatio(d) <= 0.0) return;
            d.SetValue(TextBlock.FontSizeProperty, ((double)e.NewValue * GetRelativeFontSizeRatio(d)));
        }

        public static double GetRelativeFontSizeRatio(DependencyObject obj)
        {
            return (double)obj.GetValue(RelativeFontSizeRatioProperty);
        }

        public static void SetRelativeFontSizeRatio(DependencyObject obj, double value)
        {
            obj.SetValue(RelativeFontSizeRatioProperty, value);
        }

        public static readonly DependencyProperty RelativeFontSizeRatioProperty =
            DependencyProperty.RegisterAttached(
                "RelativeFontSizeRatio",
                typeof(double),
                typeof(Material),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender, OnRelativeFontSizeChanged1));

        private static void OnRelativeFontSizeChanged1(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
            if ((double)e.NewValue <= 0.0) return;
            Console.WriteLine("r: " + GetRelativeFontSize(d));
            d.SetValue(TextElement.FontSizeProperty, GetRelativeFontSize(d) * (double)e.NewValue);

        }

        #endregion


        #region Busy
        public static string GetBusy(DependencyObject obj)
        {
            return (string)obj.GetValue(BusyProperty);
        }

        public static void SetBusy(DependencyObject obj, bool value)
        {
            obj.SetValue(BusyProperty, value);
        }

        public static readonly DependencyProperty BusyProperty =
            DependencyProperty.RegisterAttached(
                "Busy",
                typeof(bool),
                typeof(Material),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None));

        #endregion

        #region Label
        public static string GetLabel(DependencyObject obj)
        {
            return (string)obj.GetValue(LabelProperty);
        }

        public static void SetLabel(DependencyObject obj, string value)
        {
            obj.SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.RegisterAttached(
                "Label",
                typeof(string),
                typeof(Material),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        #endregion

        #region Hint
        public static string GetHint(DependencyObject obj)
        {
            return (string)obj.GetValue(HintProperty);
        }

        public static void SetHint(DependencyObject obj, string value)
        {
            obj.SetValue(HintProperty, value);
        }

        public static readonly DependencyProperty HintProperty =
            DependencyProperty.RegisterAttached(
                "Hint",
                typeof(string),
                typeof(Material),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
        #endregion

        #region HighlightColor
        public static Brush GetHighlightColor(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HighlightColorProperty);
        }

        public static void SetHighlightColor(DependencyObject obj, Brush value)
        {
            obj.SetValue(HighlightColorProperty, value);
        }

        public static readonly DependencyProperty HighlightColorProperty =
            DependencyProperty.RegisterAttached(
                "HighlightColor",
                typeof(Brush),
                typeof(Material),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region HightlightEnabled
        public static bool GetHighlightEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(HighlightEnabledProperty);
        }

        public static void SetHighlightEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(HighlightEnabledProperty, value);

            if (obj is TabControl)
            {
                var tc = obj as TabControl;
                if (value)
                {
                    tc.SelectionChanged += Tc_SelectionChanged;
                    tc.Loaded += Tc_Initialized;
                }
                else
                {
                    tc.SelectionChanged -= Tc_SelectionChanged;
                    tc.Loaded -= Tc_Initialized;
                }
            }
        }

        public static readonly DependencyProperty HighlightEnabledProperty =
            DependencyProperty.RegisterAttached(
                "HighlightEnabled",
                typeof(bool),
                typeof(Material),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, HighlightEnabledChanged));
        private static void HighlightEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            SetHighlightEnabled(sender, (bool)args.NewValue);
        }

        private static void Tc_Initialized(object sender, EventArgs e)
        {
            UpdateLine(sender as Control, false);
        }

        private static void Tc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.AddedItems != null && !e.AddedItems.Cast<object>().Any(c => c is TabItem)) return;
            UpdateLine(sender as Control);


        }



        private static void UpdateLine(Control element, bool animate = true)
        {
            if (element == null) return;
            if (element.Template == null) return;
            var border = element.Template.FindName("PART_border", element) as Border;
            var panel = element.Template.FindName("HeaderPanel", element) as TabPanel;
            if (border == null || panel == null) return;
            var item = panel.Children.Cast<TabItem>().FirstOrDefault(t => t.IsSelected);
            if (item == null) return;
            var point = item.TranslatePoint(new Point(0, 0), element);
            if (animate && item.ActualWidth > 0)
            {
                var animation = new ThicknessAnimation
                {
                    From = border.Margin,
                    To = new Thickness(point.X, 0, 0, 0),
                    Duration = TimeSpan.FromMilliseconds(200),
                    EasingFunction = new SineEase()
                    {
                        EasingMode = EasingMode.EaseInOut
                    }
                };
                Storyboard.SetTarget(animation, border);
                Storyboard.SetTargetProperty(animation, new PropertyPath(FrameworkElement.MarginProperty));


                var animation3 = new DoubleAnimation
                {
                    From = border.ActualWidth,
                    To = item.ActualWidth,
                    Duration = TimeSpan.FromMilliseconds(200),
                    EasingFunction = new SineEase()
                    {
                        EasingMode = EasingMode.EaseInOut
                    }
                };
                Storyboard.SetTarget(animation3, border);
                Storyboard.SetTargetProperty(animation3, new PropertyPath(FrameworkElement.WidthProperty));



                // Create a storyboard to contain the animation.
                Storyboard myColorAnimatedButtonStoryboard = new Storyboard();

                myColorAnimatedButtonStoryboard.Children.Add(animation);

                myColorAnimatedButtonStoryboard.Children.Add(animation3);
                myColorAnimatedButtonStoryboard.Begin();
            }
            else
            {
                border.Margin = new Thickness(point.X, 0, 0, 0);
                border.Width = item.ActualWidth;
            }
        }
        #endregion

        #region Icon
        public static string GetIcon(DependencyObject obj)
        {
            return (string)obj.GetValue(IconProperty);
        }

        public static void SetIcon(DependencyObject obj, string value)
        {
            obj.SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached(
                "Icon",
                typeof(string),
                typeof(Material),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
        #endregion

        #region Slider
        public static bool GetShowValuePopup(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowValuePopupProperty);
        }

        public static void SetShowValuePopup(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowValuePopupProperty, value);
        }

        public static readonly DependencyProperty ShowValuePopupProperty =
            DependencyProperty.RegisterAttached(
                "ShowValuePopup",
                typeof(bool),
                typeof(Material),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None));
        #endregion


        #region AsyncCommand
        public static bool GetAsyncCommand(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowValuePopupProperty);
        }

        public static void SetAsyncCommand(DependencyObject obj, IAsyncCommand value)
        {
            obj.SetValue(ShowValuePopupProperty, value);
        }

        public static readonly DependencyProperty AsyncCommandProperty =
            DependencyProperty.RegisterAttached(
                "AsyncCommand",
                typeof(IAsyncCommand),
                typeof(Material),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, OnAsyncCommandChanged));

        private static void OnAsyncCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var button = d as Button;
            if (button == null) return;

            button.Click -= Button_Click;
            if (e.NewValue != null)
            {
                button.Click += Button_Click;
            }
        }

        private static async void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            button.PreviewMouseDown += Button_PreviewMouseDown;
            button.PreviewMouseUp += Button_PreviewMouseUp;
            button.SetValue(BusyProperty, true);
            try
            {
                var asyncCommand = button.GetValue(AsyncCommandProperty) as IAsyncCommand;
                await asyncCommand.Execute(button.CommandParameter);
            }
            finally
            {
                button.SetValue(BusyProperty, false);
                button.PreviewMouseDown -= Button_PreviewMouseDown;
                button.PreviewMouseUp -= Button_PreviewMouseUp;
            }
        }

        private static void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private static void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        #endregion

    }

    public interface IAsyncCommand
    {
        bool CanExecute(object args);
        Task Execute(object args);
    }

    public class AsyncCommand : IAsyncCommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Func<object, bool> _canExecute;

        public AsyncCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object args) => _canExecute(args);

        public Task Execute(object args) => _execute(args);
    }
}
