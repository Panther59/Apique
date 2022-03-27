using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace DataLibrary
{
    public class BaseWindow : Window, IBaseView, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public BaseWindow()
        {
           
        }

        public bool? ShowDialog(DependencyObject owner)
        {
            Window parent = Window.GetWindow(owner);
            this.Owner = parent;
            return this.ShowDialog();
        }

        public void Show(DependencyObject owner)
        {
            Window parent = Window.GetWindow(owner);
            this.Owner = parent;
            this.Show();
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
            MessageWindow messageBox = new MessageWindow(title, message);
            messageBox.Owner = this;
            bool? result = messageBox.ShowDialog();
        }


        public bool ConfirmationBox(string title, string message)
        {
            ConfirmationWindow window = new ConfirmationWindow(title, message);
            window.Owner = this;
            bool? result = window.ShowDialog();
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return false;
            }
        }
    }
}
