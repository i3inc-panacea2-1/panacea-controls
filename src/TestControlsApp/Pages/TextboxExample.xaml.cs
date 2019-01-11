using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestControlsApp.Pages
{
    /// <summary>
    /// Interaction logic for TextboxExample.xaml
    /// </summary>
    public partial class TextboxExample : UserControl
    {
        public TextboxExample()
        {
            InitializeComponent();
            foreach (var info in CultureInfo.GetCultures(CultureTypes.AllCultures).Take(10))
            {
                Combo.Items.Add(info);
                Combo2.Items.Add(info);
            }
        }
    }
}
