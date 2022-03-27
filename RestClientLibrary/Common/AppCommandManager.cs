namespace RestClientLibrary.Common
{
    using DataLibrary;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Documents;

    /// <summary>
    /// Defines the <see cref = "CommandManager"/>
    /// </summary>
    public static class AppCommandManager
    {
        /// <summary>
        /// Defines the setVariableValue
        /// </summary>
        private static readonly RoutedUICommand setVariableValueCommand = new RoutedUICommand("Set the variable value", "SetVariableValueCommand", typeof(CommandManager));

        /// <summary>
        /// Defines the useVariable
        /// </summary>
        private static readonly RoutedUICommand useVariableCommand = new RoutedUICommand("Use the variable", "AddVariableCommand", typeof(CommandManager));

        /// <summary>
        /// Initializes static members of the <see cref = "AppCommandManager"/> class.
        /// </summary>
        static AppCommandManager()
        {
            // Register CommandBinding for all windows.
            System.Windows.Input.CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Window), new CommandBinding(SetVariableValueCommand, SetVariableValueCommand_Executed, SetVariableValueCommand_CanExecute));
            System.Windows.Input.CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Window), new CommandBinding(UseVariableCommand, UseVariableCommand_Executed, UseVariableCommand_CanExecute));
        }

        /// <summary>
        /// Defines the SetVariableValue
        /// </summary>
        public static event SetVariableValueHandler SetVariableValue;

        /// <summary>
        /// Gets the SetVariableValueCommand
        /// </summary>
        public static RoutedUICommand SetVariableValueCommand
        {
            get
            {
                return setVariableValueCommand;
            }
        }

        /// <summary>
        /// Gets the UseVariableCommand
        /// </summary>
        public static RoutedUICommand UseVariableCommand
        {
            get
            {
                return useVariableCommand;
            }
        }

        /// <summary>
        /// The SetVariableValueCommand_CanExecute
        /// </summary>
        /// <param name = "sender">The <see cref = "object "/></param>
        /// <param name = "e">The <see cref = "CanExecuteRoutedEventArgs"/></param>
        internal static void SetVariableValueCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!VerifType<TextBox>(e.OriginalSource) &&
               !VerifType<RichTextBox>(e.OriginalSource) &&
               !VerifType<AdvanceTextEditor.Editing.TextArea>(e.OriginalSource))
                return;

            var textBox = e.OriginalSource as TextBox;
            var richTextBox = e.OriginalSource as RichTextBox;
            var advTextBox = e.OriginalSource as AdvanceTextEditor.Editing.TextArea;

            if (textBox != null)
            {
                string input = textBox.SelectedText;
                if (string.IsNullOrEmpty(input))
                {
                    return;
                }
            }
            else if (richTextBox != null)
            {
                if (richTextBox.Selection.Text.Length == 0)
                {
                    return;
                }
            }
            else if (advTextBox != null)
            {
                string input = advTextBox.Selection.GetText();
                if (string.IsNullOrEmpty(input))
                {
                    return;
                }
            }

            e.CanExecute = true;
        }

        // TODO: replace UIElement type by type of parameter's binded object
        /// <summary>
        /// The SetVariableValueCommand_Executed
        /// </summary>
        /// <param name = "sender">The <see cref = "object "/></param>
        /// <param name = "e">The <see cref = "ExecutedRoutedEventArgs"/></param>
        internal static void SetVariableValueCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!VerifType<TextBox>(e.OriginalSource) &&
               !VerifType<RichTextBox>(e.OriginalSource) &&
               !VerifType<AdvanceTextEditor.Editing.TextArea>(e.OriginalSource))
                return;

            try
            {
                var textBox = e.OriginalSource as TextBox;
                var richTextBox = e.OriginalSource as RichTextBox;
                var advTextBox = e.OriginalSource as AdvanceTextEditor.Editing.TextArea;
                string input = null;

                if (textBox != null)
                {
                    input = textBox.SelectedText;
                }
                else if (richTextBox != null)
                {
                    input = richTextBox.Selection.Text;
                }
                else if (advTextBox != null)
                {
                    input = advTextBox.Selection.GetText();
                }

                RaiseSetVariableValue(e.Parameter?.ToString(), input);
            }
            catch (System.Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }
        /// <summary>
        /// The SetVariableValueCommand_CanExecute
        /// </summary>
        /// <param name = "sender">The <see cref = "object "/></param>
        /// <param name = "e">The <see cref = "CanExecuteRoutedEventArgs"/></param>
        internal static void UseVariableCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!VerifType<TextBox>(e.OriginalSource) &&
                !VerifType<RichTextBox>(e.OriginalSource) &&
                !VerifType<AdvanceTextEditor.Editing.TextArea>(e.OriginalSource))
                return;

            ////var textBox = e.OriginalSource as TextBox;
            ////var advTextBox = e.OriginalSource as AdvanceTextEditor.Editing.TextArea;

            ////if (textBox != null)
            ////{
            ////    string input = textBox.SelectedText;
            ////    if (string.IsNullOrEmpty(input))
            ////    {
            ////        return;
            ////    }
            ////}
            ////else if (advTextBox != null)
            ////{
            ////    string input = advTextBox.Selection.GetText();
            ////    if (string.IsNullOrEmpty(input))
            ////    {
            ////        return;
            ////    }
            ////}

            e.CanExecute = true;
        }

        /// <summary>
        /// The UseVariableCommand_Executed
        /// </summary>
        /// <param name = "sender">The <see cref = "object "/></param>
        /// <param name = "e">The <see cref = "ExecutedRoutedEventArgs"/></param>
        internal static void UseVariableCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!VerifType<TextBox>(e.OriginalSource) &&
                !VerifType<RichTextBox>(e.OriginalSource) &&
                !VerifType<AdvanceTextEditor.Editing.TextArea>(e.OriginalSource))
                return;
            try
            {
                string parameter = e.Parameter as string;

                var parts = parameter.Split('|');
                if (parts.Length == 3)
                {
                    var textBox = e.OriginalSource as TextBox;
                    var richTextBox = e.OriginalSource as RichTextBox;
                    var advTextBox = e.OriginalSource as AdvanceTextEditor.Editing.TextArea;

                    if (textBox != null)
                    {
                        string input = textBox.SelectedText;
                        if (string.IsNullOrEmpty(input) == false)
                        {
                            textBox.SelectedText = "{{" + parts[1] + "}}";
                        }
                        else
                        {
                            var oldText = textBox.Text;
                            var index = textBox.CaretIndex;
                            var newText = oldText.Insert(index, "{{" + parts[1] + "}}");

                            textBox.Text = newText;
                            textBox.CaretIndex = index + ("{{" + parts[1] + "}}").Length;
                        }
                    }
                    else if (richTextBox != null)
                    {
                        string richText = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
                        //if (string.IsNullOrEmpty(richText) == false)
                        //{
                        richTextBox.Selection.Text = "{{" + parts[1] + "}}";
                        //}
                        //else
                        //{
                        //    var oldText = textBox.Text;
                        //    var index = textBox.CaretIndex;
                        //    var newText = oldText.Insert(index, "{{" + parts[1] + "}}");

                        //    textBox.Text = newText;
                        //    textBox.CaretIndex = index + ("{{" + parts[1] + "}}").Length;
                        //}
                    }
                    else if (advTextBox != null)
                    {
                        advTextBox.Selection.ReplaceSelectionWithText("{{" + parts[1] + "}}");
                    }

                }
            }
            catch (System.Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        /// <summary>
        /// The RaiseSetVariableValue
        /// </summary>
        /// <param name="key">The <see cref="string"/></param>
        /// <param name="text">The <see cref="string"/></param>
        private static void RaiseSetVariableValue(string key, string text)
        {
            if (SetVariableValue != null)
            {
                SetVariableValue(key, text);
            }
        }

        /// <summary>
        /// The VerifType
        /// </summary>
        /// <param name = "o">The <see cref = "object "/></param>
        /// <returns>The <see cref = "bool "/></returns>
        private static bool VerifType<T>(object o)
        {
            if (o == null)
                return false;
            if (!o.GetType().Equals(typeof(T)))
                return false;
            return true;
        }

        public delegate void SetVariableValueHandler(string key, string text);
    }
}
