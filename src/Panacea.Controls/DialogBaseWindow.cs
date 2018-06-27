using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace Panacea.Controls
{
    public class DialogBaseWindow: Window
    {
        public DialogBaseWindow() { }
        public DialogBaseWindow(PanaceaWindow owner) :base()
        {
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Owner = owner;
            ShowInTaskbar = false;
        }
        public async Task FadeOut(int miliseconds, double opacity=1)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation(opacity, 0, new Duration(new TimeSpan(0, 0, 0, 0, miliseconds)));

            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            sb.Children.Add(da);

            this.BeginStoryboard(sb);

            await Task.Delay(miliseconds);
        }
        public async Task FadeIn(int miliseconds, double opacity = 1)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation(0, opacity, new Duration(new TimeSpan(0, 0, 0, 0, miliseconds)));

            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            sb.Children.Add(da);

            this.BeginStoryboard(sb);

            await Task.Delay(miliseconds);
        }
    }
}
