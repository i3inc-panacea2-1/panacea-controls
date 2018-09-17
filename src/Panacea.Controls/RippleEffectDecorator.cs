using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Panacea.Controls
{
    public class RippleEffectDecorator : ContentControl
    {
        static RippleEffectDecorator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RippleEffectDecorator), new FrameworkPropertyMetadata(typeof(RippleEffectDecorator)));
        }

        public static Brush GetHighlightBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HighlightBackgroundProperty);
        }

        public static void SetHighlightBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(HighlightBackgroundProperty, value);
        }

        public Brush HighlightBackground
        {
            get { return (Brush)GetValue(HighlightBackgroundProperty); }
            set { SetValue(HighlightBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HighlightBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighlightBackgroundProperty =
            DependencyProperty.RegisterAttached("HighlightBackground", 
                typeof(Brush), 
                typeof(RippleEffectDecorator), 
                new FrameworkPropertyMetadata(Brushes.Blue, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        Ellipse ellipse;
        Grid grid;
        Storyboard animation;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ellipse = GetTemplateChild("PART_ellipse") as Ellipse;
            grid = GetTemplateChild("PART_grid") as Grid;
            grid.IsHitTestVisible = true;
            PreviewMouseDown += Grid_MouseDown;
            TouchDown += Grid_PreviewTouchDown;
            animation = grid.FindResource("PART_animation") as Storyboard;
        }

        private void Grid_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            var factor = .9;
            var targetWidth = Math.Max(ActualWidth, ActualHeight) * 1;
            var mousePosition = e.GetTouchPoint(this);
            
            var startMargin = new Thickness(mousePosition.Position.X, mousePosition.Position.Y, 0, 0);
            //set initial margin to mouse position
            ellipse.Margin = startMargin;
            //set the to value of the animation that animates the width to the target width
            (animation.Children[0] as DoubleAnimation).To = targetWidth;
            //set the to and from values of the animation that animates the distance relative to the container (grid)
            (animation.Children[1] as ThicknessAnimation).From = startMargin;
            (animation.Children[1] as ThicknessAnimation).To = new Thickness(mousePosition.Position.X - targetWidth / factor, mousePosition.Position.Y - targetWidth / factor, 0, 0);
            ellipse.BeginStoryboard(animation);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var factor = 1.8;
            var targetWidth = Math.Max(ActualWidth, ActualHeight) * 3;
            var mousePosition = e.GetPosition(this);
           
            var startMargin = new Thickness(mousePosition.X, mousePosition.Y, 0, 0);
            //set initial margin to mouse position
            ellipse.Margin = startMargin;
            //set the to value of the animation that animates the width to the target width
            (animation.Children[0] as DoubleAnimation).To = targetWidth;
            //set the to and from values of the animation that animates the distance relative to the container (grid)
            (animation.Children[1] as ThicknessAnimation).From = startMargin;
            (animation.Children[1] as ThicknessAnimation).To = new Thickness(mousePosition.X - targetWidth / factor, mousePosition.Y - targetWidth / factor, 0, 0);
            ellipse.BeginStoryboard(animation);
        }
    }
}
