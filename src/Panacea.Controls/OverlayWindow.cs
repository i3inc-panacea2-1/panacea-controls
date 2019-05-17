using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Panacea.Controls
{
    public class OverlayWindow : NonFocusableWindow
    {
        double _dpiX;
        double _dpiY;

        private readonly Window _rootWindow;

        public OverlayWindow(Window rootWindow)
        {
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            ShowInTaskbar = false;
            _rootWindow = rootWindow;
            Setup();
        }

        private void Setup()
        {
            if (_rootWindow == null) return;
            ResizeMode = ResizeMode.NoResize;

            var dpiXProperty = typeof(SystemParameters).GetProperty("DpiX", BindingFlags.NonPublic | BindingFlags.Static);
            var dpiYProperty = typeof(SystemParameters).GetProperty("Dpi", BindingFlags.NonPublic | BindingFlags.Static);

            _dpiX = (int)dpiXProperty.GetValue(null, null) / 96.0;
            _dpiY = (int)dpiYProperty.GetValue(null, null) / 96.0;

            var window = this;
            if (_rootWindow.IsLoaded) window.Owner = _rootWindow;

            window.Top = _rootWindow.Top / _dpiY;
            window.Left = _rootWindow.Left / _dpiX;
            window.Width = ((UIElement)_rootWindow.Content).RenderSize.Width;
            window.Height = ((UIElement)_rootWindow.Content).RenderSize.Height;
            _rootWindow.LocationChanged += RootWindow_LocationChanged;

            _rootWindow.SizeChanged += RootWindow_SizeChanged;

            _rootWindow.StateChanged += RootWindow_StateChanged;

            window.Loaded += Window_Loaded;

            window.Closed += Window_Closed;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _rootWindow.Focus();
            _rootWindow.LocationChanged -= RootWindow_LocationChanged;

            _rootWindow.SizeChanged -= RootWindow_SizeChanged;

            _rootWindow.StateChanged -= RootWindow_StateChanged;

            Loaded -= Window_Loaded;

            Closed -= Window_Closed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_rootWindow.IsLoaded)
            {
                _rootWindow.Loaded += rootWindow_Loaded;
                return;
            }
            Width = ((UIElement)_rootWindow.Content).RenderSize.Width;
            Height = ((UIElement)_rootWindow.Content).RenderSize.Height;
            Top = _rootWindow.PointToScreen(new Point(0d, 0d)).Y / _dpiY;
            Left = _rootWindow.PointToScreen(new Point(0d, 0d)).X / _dpiX;

        }

        private void RootWindow_StateChanged(object sender, EventArgs e)
        {
            if (!_rootWindow.IsLoaded)
                return;
            Top = _rootWindow.PointToScreen(new Point(0d, 0d)).Y / _dpiY;
            Left = _rootWindow.PointToScreen(new Point(0d, 0d)).X / _dpiX;
            Width = ((UIElement)_rootWindow.Content).RenderSize.Width;
            Height = ((UIElement)_rootWindow.Content).RenderSize.Height;
        }

        private void RootWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!_rootWindow.IsLoaded)
                return;
            Width = ((UIElement)_rootWindow.Content).RenderSize.Width;
            Height = ((UIElement)_rootWindow.Content).RenderSize.Height;
        }

        private void RootWindow_LocationChanged(object sender, EventArgs e)
        {
            if (!_rootWindow.IsLoaded)
                return;
            Top = _rootWindow.PointToScreen(new Point(0d, 0d)).Y / _dpiY;
            Left = _rootWindow.PointToScreen(new Point(0d, 0d)).X / _dpiX;
        }

        void rootWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Owner = _rootWindow;
            Width = ((UIElement)_rootWindow.Content).RenderSize.Width;
            Height = ((UIElement)_rootWindow.Content).RenderSize.Height;
            Top = _rootWindow.PointToScreen(new Point(0d, 0d)).Y / _dpiY;
            Left = _rootWindow.PointToScreen(new Point(0d, 0d)).X / _dpiX;
            _rootWindow.Loaded -= rootWindow_Loaded;
        }

    }
}
