using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

    public class ViewLocator
    {
        Func<Type, object> _factory;
        public ViewLocator(Func<Type, object> factory)
        {
            _factory = factory;
        }

        Hashtable _cache = new Hashtable();
        public FrameworkElement GetView(ViewModelBase viewModel)
        {
            var viewType = FindView(viewModel.GetType());
            var instance = Activator.CreateInstance(viewType) as FrameworkElement;
            var property = viewType.GetProperties().FirstOrDefault(p => typeof(ViewModelBase).IsAssignableFrom(p.PropertyType));
            if (property != null)
            {
                property.SetValue(instance, viewModel);
            }
            return instance;
        }

        public FrameworkElement GetView<T>()where T:ViewModelBase
        {
            var viewType = FindView(typeof(T));
            var instance = Activator.CreateInstance(viewType) as FrameworkElement;
            var property = viewType.GetProperties().FirstOrDefault(p => typeof(ViewModelBase).IsAssignableFrom(p.PropertyType));
            if (property != null)
            {
                var viewModel = _factory(typeof(T));
                property.SetValue(instance, viewModel);
            }
            return instance;
        }

        public Type FindView(Type viewModelType)
        {
            if (_cache.Contains(viewModelType)) return _cache[viewModelType] as Type;
            var type = from a in AppDomain.CurrentDomain.GetAssemblies()
                       from t in GetTypesSafely(a)
                       let attr = t.GetCustomAttributes(typeof(ViewModelAttribute), true)
                       where attr.Length == 1 && (attr.First() as ViewModelAttribute).ViewModelType == viewModelType
                       select t;
            if (type.Count() != 1) throw new Exception($"No view found for '{viewModelType.FullName}'.");
            _cache.Add(viewModelType, type.First());
            return type.First();
        }

        public Type FindView<T>()
        {
            return FindView(typeof(T));
        }

        private IEnumerable<Type> GetTypesSafely(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(x => x != null);
            }
        }
    }
}
