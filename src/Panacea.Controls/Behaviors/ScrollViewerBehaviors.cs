using System.Windows;
using System.Windows.Controls;

namespace Panacea.Controls.Behaviors
{
    public class ScrollViewerBehaviors : DependencyObject
    {
        public static bool GetScrollsHorizontally(DependencyObject d)
        {
            return (bool)d.GetValue(ScrollsHorizontallyProperty);
        }

        public static void SetScrollsHorizontally(DependencyObject d, string value)
        {
            d.SetValue(ScrollsHorizontallyProperty, value);
        }


        // Using a DependencyProperty as the backing store for ScrollsHorizontally.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollsHorizontallyProperty =
            DependencyProperty.RegisterAttached("ScrollsHorizontally", typeof(bool), typeof(ScrollViewerBehaviors), new PropertyMetadata(false, OnScrollHorizontallyChanged));

        private static void OnScrollHorizontallyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scroll = d as ScrollViewer;
            if (d == null) return;
            scroll.PreviewMouseWheel += ScrollViewerBehaviors_PreviewMouseWheel;
        }

        private static void ScrollViewerBehaviors_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var scrollviewer = sender as ScrollViewer;
            var offset = scrollviewer.HorizontalOffset - (e.Delta * 2 / 6);
            if (scrollviewer.ExtentWidth > scrollviewer.ViewportWidth)
            {
                e.Handled = true;
                if (offset < 0)
                {
                    scrollviewer.ScrollToHorizontalOffset(0);
                }
                else if (offset > scrollviewer.ExtentWidth)
                {
                    scrollviewer.ScrollToHorizontalOffset(scrollviewer.ExtentWidth);
                }
                else
                {
                    scrollviewer.ScrollToHorizontalOffset(offset);
                }
            }
        }
    }
}
