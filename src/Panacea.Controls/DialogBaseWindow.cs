using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly int _animationMiliseconds;
        private readonly double _opacity;

        public DialogBaseWindow() { }
        public DialogBaseWindow(PanaceaWindow owner) :base()
        {
            _animationMiliseconds = 200;
            _opacity = 1;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Owner = owner;
            ShowInTaskbar = false;
        }

        public DialogBaseWindow(PanaceaWindow owner, int animationMiliseconds, double opacity = 1) : base()
        {
            _animationMiliseconds = animationMiliseconds;
            _opacity = opacity;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Owner = owner;
            ShowInTaskbar = false;
        }
        public async Task FadeOut(int miliseconds)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation(_opacity, 0, new Duration(new TimeSpan(0, 0, 0, 0, miliseconds)));

            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            sb.Children.Add(da);

            this.BeginStoryboard(sb);

            await Task.Delay(miliseconds);
        }
        public async Task FadeIn(int miliseconds)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation(0, _opacity, new Duration(new TimeSpan(0, 0, 0, 0, miliseconds)));

            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            sb.Children.Add(da);

            this.BeginStoryboard(sb);

            await Task.Delay(miliseconds);
        }

        protected override async void OnActivated(EventArgs e)
        {
            await FadeIn(_animationMiliseconds);
            base.OnActivated(e);
        }

        protected virtual async void AnimatedClose()
        {
            await FadeOut(_animationMiliseconds);
            Close();
        }
    }
}
