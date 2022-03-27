using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataLibrary
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : BaseWindow
    {
        public MessageResult Result { get; set; }
        public MessageButton Button { get; set; }

        string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        string _title;
        public string Titletext
        {
            get { return _title; }
            set { _title = value; }
        }
        public MessageWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MessageWindow_Loaded);
        }

        public MessageWindow(string message)
            : this()
        {
            Message = message;
            Titletext = "Error";
        }

        public MessageWindow(string message, string title)
            : this()
        {
            Message = message;
            Titletext = title;
        }

        public MessageWindow(string message, string title, MessageButton button)
            : this()
        {
            this.Message = message;
            this.Titletext = title;
            this.Button = button;
            if (button == MessageButton.YesNoCancel)
            {
                grdYesNoCancel.Visibility = System.Windows.Visibility.Visible;
                grdOk.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                grdYesNoCancel.Visibility = System.Windows.Visibility.Collapsed;
                grdOk.Visibility = System.Windows.Visibility.Visible;
            }
        }

        void MessageWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageResult.No;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageResult.Cancel;
            this.Close();
        }

        public static MessageResult Show(string message, string title)
        {
            MessageWindow window = new MessageWindow(message, title);
            window.ShowDialog();
            return window.Result;
        }

        public static MessageResult Show(string message, string title, MessageButton button)
        {
            MessageWindow window = new MessageWindow(message, title, button);
            window.ShowDialog();
            return window.Result;
        }

        public static MessageResult Show(string message, string title, Window owner)
        {
            MessageWindow window = new MessageWindow(message, title);
            window.Owner = owner;
            window.ShowDialog();
            return window.Result;
        }

        public static MessageResult Show(string message, string title, MessageButton button, Window owner)
        {
            MessageWindow window = new MessageWindow(message, title, button);
            window.Owner = owner;
            window.ShowDialog();
            return window.Result;
        }

    }

    public enum MessageResult
    {
        Yes,
        No,
        Cancel
    }

    public enum MessageButton
    {
        Ok,
        YesNoCancel
    }
}
