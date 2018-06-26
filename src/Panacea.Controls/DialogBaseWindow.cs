using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Panacea.Controls
{
    public class DialogBaseWindow: Window
    {
        public DialogBaseWindow() { }
        public DialogBaseWindow(Window owner) :base()
        {
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Owner = owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SizeToContent = SizeToContent.WidthAndHeight;
            ShowInTaskbar = false;
        }
    }
}
