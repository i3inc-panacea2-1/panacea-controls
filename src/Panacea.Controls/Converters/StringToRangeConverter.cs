using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Panacea.Controls.Converters
{
    /// <summary>
    /// Returns 0 for a range of 0 to 1 if the given string is not empty, and the second parameter if the string is empty.
    /// </summary>
    internal class StringToRangeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string text = null;
            if(values[0] is TextBox)
            {
                text = (values[0] as TextBox).Text;
            }
            else if (values[0] is PasswordBox)
            {
                text = (values[0] as PasswordBox).Password;
            }
            else if(values[0] is ComboBox)
            {
                if ((values[0] as ComboBox).IsFocused || (values[0] as ComboBox).IsDropDownOpen) text = " ";
                else if((values[0] as ComboBox).SelectedItem != null)
                {
                    text = (values[0] as ComboBox).SelectedItem?.ToString();
                }
                else
                {
                    text = (values[0] as ComboBox).SelectedValue?.ToString();
                }
            }
            
            if (!string.IsNullOrEmpty(text))
            {
                return 0.0;
            }
            return (double)values[1];

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new Exception("Not implemented");
        }
    }
}
