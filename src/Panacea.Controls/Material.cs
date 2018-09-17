﻿using Panacea.Controls.Converters;
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
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Panacea.Controls
{
    public class Material
    {
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
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

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
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
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
            
            var element = sender as Control;
            if (element == null) return;
            if (element.Template == null) return;
            var presenter = element.Template.FindName("PART_SelectedContentHost", element) as FrameworkElement;
            if (presenter == null) return;
            presenter.Opacity = 0;
            var animation2 = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(200)
            };
            
            Storyboard.SetTarget(animation2, presenter);
            Storyboard.SetTargetProperty(animation2, new PropertyPath(FrameworkElement.OpacityProperty));
            Storyboard myColorAnimatedButtonStoryboard = new Storyboard();
            myColorAnimatedButtonStoryboard.Children.Add(animation2);
            myColorAnimatedButtonStoryboard.Begin();
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
            if (animate)
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

        #region SliderLabelConverter
        public static Brush GetSliderLabelConverter(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SliderLabelConverterProperty);
        }

        public static void SetSliderLabelConverter(DependencyObject obj, Brush value)
        {
            obj.SetValue(SliderLabelConverterProperty, value);
        }

        public static readonly DependencyProperty SliderLabelConverterProperty =
            DependencyProperty.RegisterAttached(
                "SliderLabel",
                typeof(IValueConverter),
                typeof(Material),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region Icon
        public static ControlTemplate GetIcon(DependencyObject obj)
        {
            return (ControlTemplate)obj.GetValue(IconProperty);
        }

        public static void SetIcon(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached(
                "Icon",
                typeof(ControlTemplate),
                typeof(Material),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

    }
}
