namespace RestClientLibrary.Screen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using AdvanceTextEditor.CodeCompletion;
    using DataLibrary;
    using RestClientLibrary.Model;

    /// <summary>
    /// Interaction logic for ucHeaders.xaml
    /// </summary>
    public partial class ucHeaders : BaseUserControl
    {
        /// <summary>
        /// Defines the completionWindow
        /// </summary>
        private CompletionWindow completionWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref = "ucHeaders"/> class.
        /// </summary>
        public ucHeaders()
        {
            InitializeComponent();
            this.txtRawHeaders.TextArea.TextEntered += this.TextArea_TextEntered;
        }

        /// <summary>
        /// The TextArea_TextEntered
        /// </summary>
        /// <param name = "sender">The <see cref = "object "/></param>
        /// <param name = "e">The <see cref = "TextCompositionEventArgs"/></param>
        private void TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            try
            {
                AdvanceTextEditor.Editing.TextArea editor = sender as AdvanceTextEditor.Editing.TextArea;
                string lastChar = null;
                if (editor.Document.TextLength > 1 && editor.Caret.Offset >= 2)
                {
                    lastChar = editor.Document.GetCharAt(editor.Caret.Offset - 2).ToString();
                }

                string currentText = e.Text;
                if (lastChar == "{" && currentText == "{")
                {
                    // open code completion after the user has pressed dot:
                    completionWindow = new CompletionWindow(editor);
                    var ucMain = this.TryFindParent<ucMain>();
                    if (ucMain != null && ucMain.Parent != null && ucMain.Parent is Grid)
                    {
                        completionWindow.Resources = (ucMain.Parent as Grid).Resources;
                    }

                    // provide AvalonEdit with the data:
                    IEnumerable<MyCompletionData> data = ucMain.ViewModel.SelectedRestClient.GetVariablesForSuggestion();
                    var originalData = completionWindow.CompletionList.CompletionData;
                    if (data != null)
                    {
                        data.ToList().ForEach(x => originalData.Add(x));
                    }

                    completionWindow.Show();
                    completionWindow.Closed += delegate
                    {
                        completionWindow = null;
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }
    }
}
