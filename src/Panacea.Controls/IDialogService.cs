using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Panacea.Controls
{
    public interface IDialogService
    {
        void Show(string title, FrameworkElement content, string negativeText, ICommand negativeCommand, string positiveText, ICommand positiveCommand, bool fitToContent = true);
        Window Owner { get; }
    }
}
