namespace RestClientLibrary.Screen
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using AdvanceTextEditor.CodeCompletion;
    using DataLibrary;
    using RestClientLibrary.Model;
    using RestClientLibrary.View;
    using RestClientLibrary.ViewModel.Automations;

    /// <summary>
    /// Interaction logic for ucRuntimeCode.xaml
    /// </summary>
    public partial class ucRuntimeCode : BaseUserControl, IRuntimeCodeView
    {
        private CompletionWindow completionWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref = "ucRuntimeCode"/> class.
        /// </summary>
        public ucRuntimeCode()
        {
            InitializeComponent();
            this.DataContextChanged += this.UcRuntimeCode_DataContextChanged;
        }

        /// <summary>
        /// The ViewCodePreview
        /// </summary>
        /// <param name="code">The <see cref="string"/></param>
        public void ViewCodePreview(string code)
        {
            RuntimeCodeWindow window = new RuntimeCodeWindow();
            var ucWorkspace = this.TryFindParent<ucWorkspace>();
            if (ucWorkspace != null && ucWorkspace.Parent != null && ucWorkspace.Parent is Grid)
            {
                window.Resources = (ucWorkspace.Parent as Grid).Resources;
            }

            window.LoadData(code);
            window.ShowDialog(this);
        }

        /// <summary>
        /// The UcRuntimeCode_DataContextChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/></param>
        private void UcRuntimeCode_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = this.DataContext as RuntimeCodeViewModel;
            if (vm != null)
            {
                vm.AttachView(this);
            }
        }

        ///// <summary>
        ///// The TextArea_TextEntered
        ///// </summary>
        ///// <param name = "sender">The <see cref = "object "/></param>
        ///// <param name = "e">The <see cref = "TextCompositionEventArgs"/></param>
        //private void TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        //{
        //    try
        //    {
        //        AdvanceTextEditor.Editing.TextArea editor = sender as AdvanceTextEditor.Editing.TextArea;
        //        string lastChar = null;
        //        if (editor.Document.TextLength > 1 && editor.Caret.Offset >= 2)
        //        {
        //            lastChar = editor.Document.GetCharAt(editor.Caret.Offset - 2).ToString();
        //        }

        //        string currentText = e.Text;
        //        if (lastChar == "{" && currentText == "{")
        //        {
        //            // open code completion after the user has pressed dot:
        //            completionWindow = new CompletionWindow(editor);
        //            var ucWorkspace = this.TryFindParent<ucWorkspace>();
        //            if (ucWorkspace != null && ucWorkspace.Parent != null && ucWorkspace.Parent is Grid)
        //            {
        //                completionWindow.Resources = (ucWorkspace.Parent as Grid).Resources;
        //            }

        //            // provide AvalonEdit with the data:
        //            IEnumerable<MyCompletionData> data = ucWorkspace.ViewModel.RestClient.GetVariablesForSuggestion();
        //            var originalData = completionWindow.CompletionList.CompletionData;
        //            if (data != null)
        //            {
        //                data.ToList().ForEach(x => originalData.Add(x));
        //            }

        //            completionWindow.Show();
        //            completionWindow.Closed += delegate
        //            {
        //                completionWindow = null;
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.LogException(ex);
        //    }
        //}
    }
}
