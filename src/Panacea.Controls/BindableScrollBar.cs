using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;

namespace Panacea.Controls
{
    public class BindableScrollBar : ScrollBar
    {
        public ScrollViewer BoundScrollViewer
        {
            get { return (ScrollViewer)GetValue(BoundScrollViewerProperty); }
            set { SetValue(BoundScrollViewerProperty, value); }
        }
        // Using a DependencyProperty as the backing store for BoundScrollViewer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BoundScrollViewerProperty =
            DependencyProperty.Register("BoundScrollViewer", typeof(ScrollViewer), typeof(BindableScrollBar), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnBoundScrollViewerPropertyChanged)));

        private static void OnBoundScrollViewerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as BindableScrollBar;
            var old = e.OldValue as ScrollViewer;
            if (old != null)
            {
                old.ScrollChanged -= control.BoundScrollChanged;
            }
            var newViewer = e.NewValue as ScrollViewer;
            if (newViewer != null)
            {
                newViewer.ScrollChanged += control.BoundScrollChanged;
            }
            BindableScrollBar sender = d as BindableScrollBar;
            if (sender != null && e.NewValue != null)
            {
                sender.UpdateBindings();
            }
        }

        public bool AutoHide
        {
            get { return (bool)GetValue(AutoHideProperty); }
            set { SetValue(AutoHideProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoHide.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoHideProperty =
            DependencyProperty.Register("AutoHide", typeof(bool), typeof(BindableScrollBar), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnAutoHidePropertyChanged)));

        private static void OnAutoHidePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BindableScrollBar sender = d as BindableScrollBar;
            if (sender.AutoHide == true)
                sender.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableScrollBar"/> class.
        /// </summary>
        /// <param name="scrollViewer">The scroll viewer.</param>
        /// <param name="o">The o.</param>
        public BindableScrollBar(ScrollViewer scrollViewer, Orientation o)
            : base()
        {
            this.Orientation = o;
            BoundScrollViewer = scrollViewer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableScrollBar"/> class.
        /// </summary>
        public BindableScrollBar() :
            base()
        { }

        private void UpdateBindings()
        {
            AddHandler(ScrollBar.ScrollEvent, new ScrollEventHandler(OnScroll));

            Minimum = 0;
            if (Orientation == Orientation.Horizontal)
            {
                SetBinding(ScrollBar.MaximumProperty, (new Binding("ScrollableWidth") { Source = BoundScrollViewer, Mode = BindingMode.OneWay }));
                SetBinding(ScrollBar.ViewportSizeProperty, (new Binding("ViewportWidth") { Source = BoundScrollViewer, Mode = BindingMode.OneWay }));
            }
            else
            {
                this.SetBinding(ScrollBar.MaximumProperty, (new Binding("ScrollableHeight") { Source = BoundScrollViewer, Mode = BindingMode.OneWay }));
                this.SetBinding(ScrollBar.ViewportSizeProperty, (new Binding("ViewportHeight") { Source = BoundScrollViewer, Mode = BindingMode.OneWay }));
            }
            LargeChange = 242;
            SmallChange = 16;
        }

        private bool _delay = false;
        private Task _task = null;
        private void BoundScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange == 0 && e.VerticalChange == 0) return;

            switch (this.Orientation)
            {
                case Orientation.Horizontal:
                    this.Value = e.HorizontalOffset;
                    break;
                case Orientation.Vertical:
                    this.Value = e.VerticalOffset;
                    break;
                default:
                    break;
            }
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => this.Visibility = Visibility.Visible));
            if (_task != null && _task.Status == TaskStatus.Running)
            {
                _delay = true;
                return;
            }
            if (_task != null && _task.Status == TaskStatus.RanToCompletion)
            {
                _task.Dispose();
                _task = null;
            }
            _delay = true;
            _task = new Task(() =>
            {
                while (_delay)
                {
                    _delay = false;
                    Thread.Sleep(300);
                }
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => this.Visibility = Visibility.Hidden));

            });
            _task.Start();

        }

        private void OnScroll(object sender, ScrollEventArgs e)
        {
            switch (this.Orientation)
            {
                case Orientation.Horizontal:
                    this.BoundScrollViewer.ScrollToHorizontalOffset(e.NewValue);
                    break;
                case Orientation.Vertical:
                    this.BoundScrollViewer.ScrollToVerticalOffset(e.NewValue);
                    break;
                default:
                    break;
            }
        }
    }
}
