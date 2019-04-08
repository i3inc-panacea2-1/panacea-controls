using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Panacea.Controls
{
    public class StarControl : Control
    {
        static StarControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StarControl), new FrameworkPropertyMetadata(typeof(StarControl)));
        }

        private const Int32 STAR_SIZE = 12;
        Grid gdStar;
        Rectangle mask;
        Path starForeground, starOutline;

        public StarControl()
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            mask = Template.FindName("mask", this) as Rectangle;
            gdStar = Template.FindName("gdStar", this) as Grid;
            starForeground = Template.FindName("starForeground", this) as Path;
            starOutline = Template.FindName("starOutline", this) as Path;

            gdStar.Width = STAR_SIZE;
            gdStar.Height = STAR_SIZE;
            gdStar.Clip = new RectangleGeometry
            {
                Rect = new Rect(0, 0, STAR_SIZE, STAR_SIZE)
            };

            mask.Width = STAR_SIZE;
            mask.Height = STAR_SIZE;

        }



        public Thickness StarMargin
        {
            get { return (Thickness)GetValue(StarMarginProperty); }
            set { SetValue(StarMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StarMagrin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StarMarginProperty =
            DependencyProperty.Register("StarMargin", typeof(Thickness), typeof(StarControl), new PropertyMetadata(null));



        #region BackgroundColor

        /// <summary>
        ///     BackgroundColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor",
                typeof(SolidColorBrush), typeof(StarControl),
                new FrameworkPropertyMetadata(Brushes.Transparent));

        /// <summary>
        ///     Gets or sets the BackgroundColor property.
        /// </summary>
        public SolidColorBrush BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }


        #endregion

        #region StarForegroundColor

        /// <summary>
        ///     StarForegroundColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarForegroundColorProperty =
            DependencyProperty.Register("StarForegroundColor", typeof(Brush),
                typeof(StarControl),
                new FrameworkPropertyMetadata(Brushes.Transparent, OnForegroundChanged));

        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as StarControl)?.Update();
        }


        /// <summary>
        ///     Gets or sets the StarForegroundColor property.
        /// </summary>
        public Brush StarForegroundColor
        {
            get { return (Brush)GetValue(StarForegroundColorProperty); }
            set { SetValue(StarForegroundColorProperty, value); }
        }

        #endregion

        #region StarOutlineColor

        /// <summary>
        ///     StarOutlineColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarOutlineColorProperty =
            DependencyProperty.Register("StarOutlineColor", typeof(Brush),
                typeof(StarControl),
                new FrameworkPropertyMetadata(Brushes.Transparent));

        /// <summary>
        ///     Gets or sets the StarOutlineColor property.
        /// </summary>
        public Brush StarOutlineColor
        {
            get { return (Brush)GetValue(StarOutlineColorProperty); }
            set { SetValue(StarOutlineColorProperty, value); }
        }


        #endregion

        #region Value

        /// <summary>
        ///     Value Dependency Property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Decimal),
                typeof(StarControl),
                new FrameworkPropertyMetadata((Decimal)0.0,
                    OnValueChanged,
                    CoerceValueValue));

        /// <summary>
        ///     Gets or sets the Value property.
        /// </summary>
        public Decimal Value
        {
            get { return (Decimal)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        ///     Handles changes to the Value property.
        /// </summary>
        private static void OnValueChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(MinimumProperty);
            d.CoerceValue(MaximumProperty);
            var starControl = (StarControl)d;
            starControl?.Update();
        }

        void Update()
        {
            if (StarForegroundColor == Brushes.Transparent) return;
            if (Value == 0.0m)
            {
                StarForegroundActiveColor = Brushes.Gray;
            }
            else
            {
                StarForegroundActiveColor = StarForegroundColor;
            }

            var marginLeftOffset = (Int32)(Value * STAR_SIZE);
            StarMargin = new Thickness(marginLeftOffset, 0, 0, 0);
            InvalidateArrange();
            InvalidateMeasure();
            InvalidateVisual();
        }



        public Brush StarForegroundActiveColor
        {
            get { return (Brush)GetValue(StarForegroundActiveColorProperty); }
            set { SetValue(StarForegroundActiveColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StarForegroundActiveColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StarForegroundActiveColorProperty =
            DependencyProperty.Register("StarForegroundActiveColor", typeof(Brush), typeof(StarControl), new PropertyMetadata(Brushes.Transparent));



        /// <summary>
        ///     Coerces the Value value.
        /// </summary>
        private static object CoerceValueValue(DependencyObject d, object value)
        {
            var starControl = (StarControl)d;
            var current = (Decimal)value;
            if (current < starControl.Minimum) current = starControl.Minimum;
            if (current > starControl.Maximum) current = starControl.Maximum;
            return current;
        }

        #endregion

        #region Maximum

        /// <summary>
        ///     Maximum Dependency Property
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(Decimal),
                typeof(StarControl),
                new FrameworkPropertyMetadata((Decimal)1.0));

        /// <summary>
        ///     Gets or sets the Maximum property.
        /// </summary>
        public Decimal Maximum
        {
            get { return (Decimal)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        #endregion

        #region Minimum

        /// <summary>
        ///     Minimum Dependency Property
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(Decimal),
                typeof(StarControl),
                new FrameworkPropertyMetadata((Decimal)0.0));

        /// <summary>
        ///     Gets or sets the Minimum property.
        /// </summary>
        public Decimal Minimum
        {
            get { return (Decimal)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        #endregion
    }
}
