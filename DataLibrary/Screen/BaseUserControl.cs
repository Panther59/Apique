using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;

namespace DataLibrary
{
    public class BaseUserControl : UserControl, IBaseView, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public BaseUserControl()
        {
           
        }

        public void CloseParentWindow()
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        public void MessageShow(string title, string message)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                MessageWindow messageBox = new MessageWindow(message, title);
                Window parent = Window.GetWindow(this);
                if (parent != null)
                {
                    messageBox.Owner = parent;
                }
                bool? result = messageBox.ShowDialog();
            }));
        }


        public bool ConfirmationBox(string title, string message)
        {
            return this.Dispatcher.Invoke(new Func<bool>(() =>
            {
                ConfirmationWindow window = new ConfirmationWindow(title, message);
                Window parent = Window.GetWindow(this);
                if (parent != null)
                {
                    window.Owner = parent;
                }
                bool? result = window.ShowDialog();
                if (result.HasValue)
                {
                    return result.Value;
                }
                else
                {
                    return false;
                }
            }));
        }
    }
}
