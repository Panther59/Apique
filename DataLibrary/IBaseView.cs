using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace DataLibrary
{
    public interface IBaseView
    {
        Dispatcher Dispatcher { get; }
        void MessageShow(string title, string message);
        bool ConfirmationBox(string title, string message);

        void CloseParentWindow();
    }
}
