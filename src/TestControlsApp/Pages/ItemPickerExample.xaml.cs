﻿using System;
using System.Collections;
using System.Collections.Generic;
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
    /// Interaction logic for ItemPickerExample.xaml
    /// </summary>
    public partial class ItemPickerExample : UserControl
    {
        public ItemPickerExample()
        {
            InitializeComponent();
            Items = new List<string>()
            {
                "asfdsf",
                "sdfsdfs",
                "sadfdasfs",
                "asdfdsf",
                "",
                "safasdf",
                "sadfsaf",
                "asdfsad",
                "asdfasd",
                "",
                "asdfas",
                "",
                "dasfdasf","asfdsf",
                "sdfsdfs",
                "sadfdasfs",
                "asdfdsf",
                "",
                "safasdf",
                "sadfsaf",
                "asdfsad",
                "asdfasd",
                "",
                "asdfas",
                "",
                "dasfdasf","asfdsf",
                "sdfsdfs",
                "sadfdasfs",
                "asdfdsf",
                "",
                "safasdf",
                "sadfsaf",
                "asdfsad",
                "asdfasd",
                "",
                "asdfas",
                "",
                "dasfdasf","asfdsf",
                "sdfsdfs",
                "sadfdasfs",
                "asdfdsf",
                "",
                "safasdf",
                "sadfsaf",
                "asdfsad",
                "asdfasd",
                "",
                "asdfas",
                "",
                "dasfdasf",

            };
        }



        public IEnumerable Items
        {
            get { return (IEnumerable)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(IEnumerable), typeof(ItemPickerExample), new PropertyMetadata(null));


    }
}
