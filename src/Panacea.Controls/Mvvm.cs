using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Panacea.Controls
{
    public class Mvvm
    {
        #region Label
        public static bool GetAutoManageLifeCycle(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoManageLifeCycleProperty);
        }

        public static void SetAutoManageLifeCycle(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoManageLifeCycleProperty, value);
        }

        public static readonly DependencyProperty AutoManageLifeCycleProperty =
            DependencyProperty.RegisterAttached(
                "AutoManageLifeCycle",
                typeof(string),
                typeof(Material),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, OnManageLifeCycleChanged));


        private static void OnManageLifeCycleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;
            if (element == null) return;
            if((bool)e.NewValue == true)
            {
                element.Loaded += Element_Loaded;
                element.Unloaded += Element_Unloaded;
            }
            else
            {
                element.Loaded -= Element_Loaded;
                element.Unloaded -= Element_Unloaded;
            }
        }


        private static void Element_Unloaded(object sender, RoutedEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element == null) return;
            var context = element.DataContext as IViewModelWithEvents;
            if (context == null) return;

            context.OnUnloaded();
            element.Unloaded -= Element_Unloaded;
        }

        private static void Element_Loaded(object sender, RoutedEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element == null) return;
            var context = element.DataContext as IViewModelWithEvents;
            if (context == null) return;

            context.OnLoaded();
            element.Loaded -= Element_Loaded;
        }

        #endregion
    }

    public interface IViewModelWithEvents
    {
        void OnLoaded();

        void OnUnloaded();
    }

    public class ViewModelBase : IViewModelWithEvents, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void OnLoaded()
        {
            
        }

        public virtual void OnUnloaded()
        {
            
        }
    }

    public class ViewModelAttribute : Attribute
    {
        public Type ViewModelType { get; protected set; }

        public ViewModelAttribute(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }
    }

    public static class ViewLocator
    {
        public static Type FindView(Type viewModelType)
        {

            var type = from a in AppDomain.CurrentDomain.GetAssemblies()
                       from t in a.GetTypes()
                       let attr = t.GetCustomAttributes(typeof(ViewModelAttribute), true)
                       where attr.Length == 1 && (attr.First() as ViewModelAttribute).ViewModelType == viewModelType
                       select t;

            return type.FirstOrDefault();
        }

        public static Type FindView<T>()
        {

            return FindView(typeof(T));
        }
    }
}
