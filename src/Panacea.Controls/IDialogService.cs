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
        void Show(string title, string text, bool fitToContent = true);
        void Show(string title, string text, string negativeText, string positiveText, ICommand negativeCommand, ICommand positiveCommand, bool fitToContent = true);
        Window Owner { get; }
    }
}
