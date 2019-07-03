using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Panacea.Controls.Behaviors
{
    public class ButtonBehaviors : DependencyObject
    {
        public static ICommand GetMouseDownCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(MouseDownCommandProperty);
        }

        public static void SetMouseDownCommand(DependencyObject d, string value)
        {
            d.SetValue(MouseDownCommandProperty, value);
        }

        public static readonly DependencyProperty MouseDownCommandProperty =
            DependencyProperty.RegisterAttached("ScrollsHorizontally", typeof(ICommand), typeof(ButtonBehaviors), new PropertyMetadata(null, OnMouseDownCommandChanged));

        private static void OnMouseDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = d as Button;
            if (b == null) return;
            b.MouseDown -= B_MouseDown;
            if(e.NewValue != null)
                b.MouseDown += B_MouseUp;
        }

        private static void B_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var b = sender as Button;
            GetMouseDownCommand(b)?.Execute(b.GetValue(Button.CommandParameterProperty));
        }
        
        public static ICommand GetMouseUpCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(MouseUpCommandProperty);
        }

        public static void SetMouseUpCommand(DependencyObject d, string value)
        {
            d.SetValue(MouseUpCommandProperty, value);
        }

        public static readonly DependencyProperty MouseUpCommandProperty =
            DependencyProperty.RegisterAttached("ScrollsHorizontally", typeof(ICommand), typeof(ButtonBehaviors), new PropertyMetadata(null, OnMouseUpCommandChanged));

        private static void OnMouseUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = d as Button;
            if (b == null) return;
            b.MouseUp -= B_MouseUp;
            if (e.NewValue != null)
                b.MouseUp += B_MouseUp;
        }

        private static void B_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var b = sender as Button;
            GetMouseUpCommand(b)?.Execute(b.GetValue(Button.CommandParameterProperty));
        }
    }
}
