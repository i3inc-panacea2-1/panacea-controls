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


        public void Show(string title, string text, bool fitToContent = true)
        {
            DialogBox dlg = new DialogBox(title, text, Owner, fitToContent);
            dlg.Show();
        }

        public void Show(string title, string text, string negativeText, string positiveText, ICommand negativeCommand, ICommand positiveCommand, bool fitToContent = true)
        {
            DialogBox dlg = new DialogBox(title, text, Owner, negativeText, positiveText, negativeCommand, positiveCommand, fitToContent);
            dlg.Show();
        }
    }
}
