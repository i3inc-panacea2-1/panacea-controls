using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Panacea.Controls
{
    public class RatingControl : Control
    {
        static RatingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RatingControl), new FrameworkPropertyMetadata(typeof(RatingControl)));
        }

        #region Ctor

        public RatingControl()
        {
            Stars = new ObservableCollection<StarModel>();
        }

        #endregion

        #region BackgroundColor

        /// <summary>
        ///     BackgroundColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(SolidColorBrush),
                typeof(RatingControl),
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
                typeof(RatingControl),
                new FrameworkPropertyMetadata(Brushes.Transparent));

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
                typeof(RatingControl),
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
                typeof(RatingControl),
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
            var ratingsControl = (RatingControl)d;
            SetupStars(ratingsControl);
        }


        /// <summary>
        ///     Coerces the Value value.
        /// </summary>
        private static object CoerceValueValue(DependencyObject d, object value)
        {
            var ratingsControl = (RatingControl)d;
            var current = (Decimal)value;
            if (current < ratingsControl.Minimum) current = ratingsControl.Minimum;
            if (current > ratingsControl.Maximum) current = ratingsControl.Maximum;
            return current;
        }

        #endregion

        #region NumberOfStars

        /// <summary>
        ///     NumberOfStars Dependency Property
        /// </summary>
        public static readonly DependencyProperty NumberOfStarsProperty =
            DependencyProperty.Register("NumberOfStars", typeof(Int32), typeof(RatingControl),
                new FrameworkPropertyMetadata(5,
                    OnNumberOfStarsChanged,
                    CoerceNumberOfStarsValue));

        /// <summary>
        ///     Gets or sets the NumberOfStars property.
        /// </summary>
        public Int32 NumberOfStars
        {
            get { return (Int32)GetValue(NumberOfStarsProperty); }
            set { SetValue(NumberOfStarsProperty, value); }
        }

        /// <summary>
        ///     Handles changes to the NumberOfStars property.
        /// </summary>
        private static void OnNumberOfStarsChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(MinimumProperty);
            d.CoerceValue(MaximumProperty);
            var ratingsControl = (RatingControl)d;
            SetupStars(ratingsControl);
        }


        /// <summary>
        ///     Coerces the NumberOfStars value.
        /// </summary>
        private static object CoerceNumberOfStarsValue(DependencyObject d, object value)
        {
            var ratingsControl = (RatingControl)d;
            var current = (Int32)value;
            if (current < ratingsControl.Minimum) current = ratingsControl.Minimum;
            if (current > ratingsControl.Maximum) current = ratingsControl.Maximum;
            return current;
        }

        #endregion

        #region Maximum

        /// <summary>
        ///     Maximum Dependency Property
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(Int32), typeof(RatingControl),
                new FrameworkPropertyMetadata(10));

        /// <summary>
        ///     Gets or sets the Maximum property.
        /// </summary>
        public Int32 Maximum
        {
            get { return (Int32)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        #endregion

        #region Minimum

        /// <summary>
        ///     Minimum Dependency Property
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(Int32), typeof(RatingControl),
                new FrameworkPropertyMetadata(0));

        /// <summary>
        ///     Gets or sets the Minimum property.
        /// </summary>
        public Int32 Minimum
        {
            get { return (Int32)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        #endregion

        #region Private Helpers

        /// <summary>
        ///     Sets up stars when Value or NumberOfStars properties change
        ///     Will only show up to the number of stars requested (up to Maximum)
        ///     so if Value > NumberOfStars * 1, then Value is clipped to maximum
        ///     number of full stars
        /// </summary>
        /// <param name="ratingsControl"></param>
        private static void SetupStars(RatingControl ratingsControl)
        {
            Decimal localValue = ratingsControl.Value;
            if (ratingsControl.Maximum > ratingsControl.NumberOfStars)
            {
                localValue /= (decimal)(ratingsControl.Maximum / (double)ratingsControl.NumberOfStars);
            }
            ratingsControl.Stars.Clear();
            for (int i = 0; i < ratingsControl.NumberOfStars; i++)
            {
                var star = new StarModel();
                //star.BackgroundColor = ratingsControl.BackgroundColor;
                //star.StarForegroundColor = ratingsControl.StarForegroundColor;
                //star.StarOutlineColor = ratingsControl.StarOutlineColor;
                if (localValue > 1)
                    star.Value = 1.0m;
                else if (localValue > 0)
                {
                    star.Value = localValue;
                }
                else
                {
                    star.Value = 0.0m;
                }

                localValue -= 1.0m;
                ratingsControl.Stars.Add(star);
            }
        }



        internal ObservableCollection<StarModel> Stars
        {
            get { return (ObservableCollection<StarModel>)GetValue(StarsProperty); }
            set { SetValue(StarsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty StarsProperty =
            DependencyProperty.Register("Stars", typeof(ObservableCollection<StarModel>), typeof(RatingControl), new PropertyMetadata(null));



        #endregion
    }

    internal class StarModel : INotifyPropertyChanged
    {
        decimal _value;
        public decimal Value
        {
            get => _value; set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
