﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Panacea.Controls
{
    /// <summary>
    /// Interaction logic for ToastNotification.xaml
    /// </summary>
    public partial class ToastNotification : DialogBaseWindow
    {
        public ToastNotification(string text, int miliseconds = 3000) : base(200, 0.85)
        {
            InitializeComponent();
            Text = text;
            SizeToContent = SizeToContent.WidthAndHeight;
            Left = Screen.PrimaryScreen.WorkingArea.Left + Screen.PrimaryScreen.WorkingArea.Width / 2;
            Top = Screen.PrimaryScreen.WorkingArea.Top + Screen.PrimaryScreen.WorkingArea.Height - 50;
            _miliseconds = miliseconds;
        }


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ToastNotification), new PropertyMetadata(null));
        private readonly int _miliseconds;

        private async void Main_Loaded(object sender, RoutedEventArgs e)
        {
            //await FadeIn(300, 0.85);
            await Task.Delay(_miliseconds);

            //await FadeOut(300, 0.85);

            AnimatedClose();
        }
        
    }
}
