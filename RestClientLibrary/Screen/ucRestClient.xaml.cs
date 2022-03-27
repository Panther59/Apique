using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AdvanceTextEditor.CodeCompletion;
using DataLibrary;
using RestClientLibrary.Model;
using RestClientLibrary.View;
using RestClientLibrary.ViewModel;

namespace RestClientLibrary.Screen
{
    /// <summary>
    /// Interaction logic for ucRestClient.xaml
    /// </summary>
    public partial class ucRestClient : BaseUserControl, IRestClientView
    {
        private RestClientViewModel _viewModel;
        private CompletionWindow completionWindow;

        public ucRestClient()
        {
            InitializeComponent();
            this.Loaded += ucRestClient_Loaded;
            this.DataContextChanged += this.UcRestClient_DataContextChanged;
            this.txtRequestContent.TextArea.TextEntered += this.TextArea_TextEntered;
        }

        private void UcRestClient_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _viewModel = this.DataContext as RestClientViewModel;
            _viewModel?.AttachView(this as IRestClientView);
        }

        private void TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            try
            {

                AdvanceTextEditor.Editing.TextArea editor = sender as AdvanceTextEditor.Editing.TextArea;

                string lastChar = null;
                if (editor.Document.TextLength > 1)
                {
                    lastChar = editor.Document.GetCharAt(editor.Caret.Offset - 2).ToString();
                }
                string currentText = e.Text;

                if (lastChar == "{" && currentText == "{")
                {
                    // open code completion after the user has pressed dot:
                   completionWindow = new CompletionWindow(editor);
                    var ucWorkspace = this.TryFindParent<ucWorkspace>();
                    if (ucWorkspace != null && ucWorkspace.Parent != null && ucWorkspace.Parent is Grid)
                    {
                        completionWindow.Resources = (ucWorkspace.Parent as Grid).Resources;
                    }
                    // provide AvalonEdit with the data:
                    IEnumerable<MyCompletionData> data = _viewModel.GetVariablesForSuggestion();

                    var originalData = completionWindow.CompletionList.CompletionData;
                    if (data != null)
                    {
                        data.ToList().ForEach(x => originalData.Add(x));
                    }
                    completionWindow.Show();
                    completionWindow.Closed += delegate {
                        completionWindow = null;
                    };
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        void ucRestClient_Loaded(object sender, RoutedEventArgs e)
        {
            //if (_viewModel == null)
            //{
            //    _viewModel = new MainViewModel((IMainView)this);
            //}
            //this.DataContext = _viewModel;
        }

        public bool? OpenSaveRequestWindow(RestClientLibrary.ViewModel.SaveRequestViewModel data)
        {
            SaveRequestWindow window = new SaveRequestWindow();
            var ucWorkspace = this.TryFindParent<ucWorkspace>();
            if (ucWorkspace != null && ucWorkspace.Parent != null && ucWorkspace.Parent is Grid)
            {
                window.Resources = (ucWorkspace.Parent as Grid).Resources;
            }
            window.LoadData(data);
            return window.ShowDialog(this);
        }

        public void ExpandCollapseFoldings(bool isExpanded, bool isJson)
        {
            if (isJson)
            {
                if (isExpanded)
                {
                    this.JsonResponse.ExpandAllFoldings();
                }
                else
                {
                    this.JsonResponse.CollapseAllFoldings();
                }
            }
            else
            {
                if (isExpanded)
                {
                    this.XmlResponse.ExpandAllFoldings();
                }
                else
                {
                    this.XmlResponse.CollapseAllFoldings();
                }
            }
        }
    }
}
