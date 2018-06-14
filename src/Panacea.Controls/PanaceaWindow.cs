using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Panacea.Controls
{
    public class PanaceaWindow: Window
    {
        public PanaceaWindow():base()
        {
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            Left = Screen.PrimaryScreen.WorkingArea.Left;
            Top = Screen.PrimaryScreen.WorkingArea.Top;
        }
    }
}
