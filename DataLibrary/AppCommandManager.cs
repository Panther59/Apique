using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using Newtonsoft.Json;

namespace DataLibrary
{
    /// <summary>
    /// Defines the <see cref="CommandManager" />
    /// </summary>
    public class AppCommandManager
    {
        private static readonly RoutedUICommand covertJsonToXmlCommand = new RoutedUICommand("Converts Json string into Xml", "CovertJsonToXmlCommand", typeof(CommandManager));

        public static RoutedUICommand CovertJsonToXmlCommand
        {
            get
            {
                return covertJsonToXmlCommand;
            }
        }

        private static readonly RoutedUICommand covertXmlToJsonCommand = new RoutedUICommand("Converts Xml string into Json", "CovertXmlToJsonCommand", typeof(CommandManager));

        public static RoutedUICommand CovertXmlToJsonCommand
        {
            get
            {
                return covertXmlToJsonCommand;
            }
        }

        static AppCommandManager()
        {
            // Register CommandBinding for all windows.
            System.Windows.Input.CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Window), new CommandBinding(CovertJsonToXmlCommand, CovertJsonToXmlCommand_Executed, CovertJsonToXmlCommand_CanExecute));
            System.Windows.Input.CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Window), new CommandBinding(CovertXmlToJsonCommand, CovertXmlToJsonCommand_Executed, CovertXmlToJsonCommand_CanExecute));
        }

        // TODO: replace UIElement type by type of parameter's binded object
        internal static void CovertJsonToXmlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!verifType<TextBox>(e.OriginalSource)) return;
            try
            {

                var textBox = e.OriginalSource as TextBox;
                string input = textBox.Text;

                    var outObj = JsonConvert.DeserializeObject(input);
                XmlDocument doc = JsonConvert.DeserializeXmlNode(input);
                string xmlText = doc.InnerText;

                textBox.Text = xmlText;
            }
            catch (System.Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        internal static void CovertJsonToXmlCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!verifType<TextBox>(e.OriginalSource)) return;

            e.CanExecute = true;
        }

        // TODO: replace UIElement type by type of parameter's binded object
        internal static void CovertXmlToJsonCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!verifType<TextBox>(e.OriginalSource)) return;

            try
            {
                var textBox = e.OriginalSource as TextBox;
                string input = textBox.Text;

                // To convert an XML node contained in string xml into a JSON string   
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(input);
                string jsonText = JsonConvert.SerializeXmlNode(doc);

                textBox.Text = jsonText;
            }
            catch (System.Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        internal static void CovertXmlToJsonCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!verifType<TextBox>(e.OriginalSource)) return;

            e.CanExecute = true;
        }

        private static bool verifType<T>(object o)
        {
            if (o == null) return false;
            if (!o.GetType().Equals(typeof(T))) return false;
            return true;
        }
    }
}