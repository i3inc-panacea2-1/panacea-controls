using System;
using System.Collections.Generic;
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
    ///     <MyNamespace:TabControlEx/>
    ///
    /// </summary>
    public class TabControlEx : TabControl
    {



        public Brush LineColor
        {
            get { return (Brush)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineColorProperty =
            DependencyProperty.Register("LineColor", typeof(Brush), typeof(TabControlEx), new PropertyMetadata(Brushes.Transparent));



        static TabControlEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabControlEx), new FrameworkPropertyMetadata(typeof(TabControlEx)));
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            UpdateLine();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            var border = Template.FindName("PART_border", this) as Border;
            border.Margin = new Thickness(0, 0, 0, 0);
            border.Width = 0;
            UpdateLine();
        }

        private void UpdateLine()
        {
            var border = Template.FindName("PART_border", this) as Border;
            var panel = Template.FindName("HeaderPanel", this) as TabPanel;
            if (border == null || panel == null) return;
            var item = panel.Children.Cast<TabItem>().First(t => t.IsSelected);
            var point = item.TranslatePoint(new Point(0, 0), panel);
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
            Storyboard.SetTargetProperty(animation, new PropertyPath(MarginProperty));


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
            Storyboard.SetTargetProperty(animation3, new PropertyPath(WidthProperty));

            var presenter = Template.FindName("PART_SelectedContentHost", this) as FrameworkElement;
            presenter.Opacity = 0;
            var animation2 = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            Storyboard.SetTarget(animation2, presenter);
            Storyboard.SetTargetProperty(animation2, new PropertyPath(OpacityProperty));

            // Create a storyboard to contain the animation.
            Storyboard myColorAnimatedButtonStoryboard = new Storyboard();

            myColorAnimatedButtonStoryboard.Children.Add(animation);
            myColorAnimatedButtonStoryboard.Children.Add(animation2);
            myColorAnimatedButtonStoryboard.Children.Add(animation3);
            myColorAnimatedButtonStoryboard.Begin();

        }
    }
}
