using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Panacea.Controls
{
    public class DialogService: IDialogService
    {
        public DialogService(Window owner)
        {
            Owner = owner;
        }

        public Window Owner { get; }

        public void Show(string title, FrameworkElement content, string negativeText, ICommand negativeCommand, string positiveText, ICommand positiveCommand, bool fitToContent = true)
        {
            DialogBox dlg = new DialogBox(Owner, title, content, negativeText, negativeCommand, positiveText, positiveCommand, fitToContent);
            dlg.Show();
        }
    }
}
