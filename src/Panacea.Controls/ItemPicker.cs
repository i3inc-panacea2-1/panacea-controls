using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Panacea.Controls
{
    public class ItemPicker : Control
    {
        static ItemPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemPicker), new FrameworkPropertyMetadata(typeof(ItemPicker)));
        }

        public ICommand SelectCommand { get; }
        public ItemPicker()
        {
            SelectedItems = new ObservableCollection<object>();
            SelectCommand = new RelayCommand(args =>
            {
                var cont = args as ItemContainer;
                if (!AllowMultipleSelections)
                {
                    SelectedItems.Clear();
                    foreach (ItemContainer item in GetChildrenOfType<ItemContainer>(Template.FindName("ItemControl", this) as ItemsControl)
                    .Where(it => it != cont))
                    {
                        item.IsSelected = false;
                    }
                }
                if (cont.IsSelected && !SelectedItems.Contains(cont.ChildItem))
                {

                    SelectedItems.Add(cont.ChildItem);
                }
                else if (!cont.IsSelected && SelectedItems.Contains(cont.ChildItem))
                {
                    SelectedItems.Remove(cont.ChildItem);
                }
            });
        }

        public static List<T> GetChildrenOfType<T>(DependencyObject depObj) where T : DependencyObject
        {
            var result = new List<T>();
            if (depObj == null) return null;
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(depObj);
            while (queue.Count > 0)
            {
                var currentElement = queue.Dequeue();
                var childrenCount = VisualTreeHelper.GetChildrenCount(currentElement);
                for (var i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(currentElement, i);
                    if (child is T)
                        result.Add(child as T);
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ItemPicker), new PropertyMetadata(default(IEnumerable), OnItemsChanged));

        private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as ItemPicker;
            if (me != null)
            {
                if (me.CurrentIndex != 1)
                    me.CurrentIndex = 1;
                else me.OnIndexChanged();
            }
        }



        public bool AllowMultipleSelections
        {
            get { return (bool)GetValue(AllowMultipleSelectionsProperty); }
            set { SetValue(AllowMultipleSelectionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowMultipleSelections.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowMultipleSelectionsProperty =
            DependencyProperty.Register("AllowMultipleSelections", typeof(bool), typeof(ItemPicker), new PropertyMetadata(false));



        public IEnumerable CurrentPage
        {
            get { return (IEnumerable)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(IEnumerable), typeof(ItemPicker), new PropertyMetadata(null));



        public int ItemsPerPage
        {
            get { return (int)GetValue(ItemsPerPageProperty); }
            set { SetValue(ItemsPerPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsPerPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsPerPageProperty =
            DependencyProperty.Register("ItemsPerPage", typeof(int), typeof(ItemPicker), new PropertyMetadata(6));



        public int MaxPages
        {
            get { return (int)GetValue(MaxPagesProperty); }
            set { SetValue(MaxPagesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxPages.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxPagesProperty =
            DependencyProperty.Register("MaxPages", typeof(int), typeof(ItemPicker), new PropertyMetadata(0));


        public int CurrentIndex
        {
            get { return (int)GetValue(CurrentIndexProperty); }
            set { SetValue(CurrentIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentIndexProperty =
            DependencyProperty.Register("CurrentIndex", typeof(int), typeof(ItemPicker), new PropertyMetadata(1, OnIndexChanged));

        private static void OnIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var me = d as ItemPicker;
            me?.OnIndexChanged();
        }

        private void OnIndexChanged()
        {
            if (ItemsSource == null)
            {
                CurrentPage = null;
                return;
            }
            MaxPages = (int)Math.Ceiling((double)(ItemsSource.Cast<object>().Count() / ItemsPerPage));
            CurrentPage = ItemsSource.Cast<object>().Skip(CurrentIndex * ItemsPerPage).Take(ItemsPerPage);

            Dispatcher.BeginInvoke(new Action(() =>
            {
                var itemsc = Template.FindName("ItemControl", this) as ItemsControl;
                if (itemsc != null)
                {
                    foreach (ItemContainer item in GetChildrenOfType<ItemContainer>(itemsc))
                    {
                        item.IsSelected = SelectedItems.Contains(item.ChildItem);
                    }
                }
            }), DispatcherPriority.Loaded);


        }



        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(ItemPicker), new PropertyMetadata(null));



        public ObservableCollection<object> SelectedItems
        {
            get { return (ObservableCollection<object>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(ObservableCollection<object>), typeof(ItemPicker), new PropertyMetadata(null));



    }

    public class ItemContainer : ContentControl
    {
        public ItemContainer()
        {
            PreviewMouseDown += ItemContainer_PreviewMouseDown;

        }

        private void ItemContainer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            IsSelected = !IsSelected;
            Command?.Execute(this);
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(ItemContainer), new PropertyMetadata(false));



        public object ChildItem
        {
            get { return (object)GetValue(ChildItemProperty); }
            set { SetValue(ChildItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Child.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChildItemProperty =
            DependencyProperty.Register("ChildItem", typeof(object), typeof(ItemContainer), new PropertyMetadata(null));




        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ItemContainer), new PropertyMetadata(null));



    }
}
