// <copyright file="RuntimeCodeViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>13-08-2017</date>

namespace RestClientLibrary.ViewModel.Automations
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataLibrary;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Emit;
    using RestClientLibrary.View;

    /// <summary>
    /// Defines the <see cref = "RuntimeCodeViewModel"/>
    /// </summary>
    public class RuntimeCodeViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// The codeCaretIndex field
        /// </summary>
        private int codeCaretIndex;

        /// <summary>
        /// The editableCode field
        /// </summary>
        private string editableCode;

        /// <summary>
        /// Defines the endCode
        /// </summary>
        private string endCode;

        /// <summary>
        /// The errors field
        /// </summary>
        private ObservableCollection<CodeErrorViewModel> errors;

        /// <summary>
        /// The hasBuildErrors field
        /// </summary>
        private bool hasBuildErrors;

        /// <summary>
        /// Defines the 
        /// </summary>
        private string indentation;

        /// <summary>
        /// The isCodePresent field
        /// </summary>
        private bool isCodePresent;

        /// <summary>
        /// The snippets field
        /// </summary>
        private List<SnippetViewModel> snippets;

        /// <summary>
        /// Defines the startCode
        /// </summary>
        private string startCode;

        /// <summary>
        /// The statusMessage field
        /// </summary>
        private string statusMessage;

        /// <summary>
        /// Defines the view
        /// </summary>
        private IRuntimeCodeView view;

        #region Commands

        /// <summary>
        /// The addCodeSnippetCommand field
        /// </summary>
        private RelayCommand<SnippetViewModel> addCodeSnippetCommand;

        /// <summary>
        /// The codeChangedCommand field
        /// </summary>
        private RelayCommand codeChangedCommand;

        /// <summary>
        /// The compileCodeCommand field
        /// </summary>
        private RelayCommand compileCodeCommand;

        /// <summary>
        /// The previewCodeCommand field
        /// </summary>
        private RelayCommand previewCodeCommand;

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Code
        /// </summary>
        public string Code
        {
            get
            {
                return this.startCode + this.GetIndentationCode(this.EditableCode) + this.endCode;
            }
        }

        /// <summary>
        /// Gets or sets the CodeCaretIndex
        /// </summary>
        public int CodeCaretIndex
        {
            get
            {
                return this.codeCaretIndex;
            }

            set
            {
                this.codeCaretIndex = value;
                this.OnPropertyChanged("CodeCaretIndex");
            }
        }

        /// <summary>
        /// Gets or sets the EditableCode
        /// </summary>
        public string EditableCode
        {
            get
            {
                return this.editableCode;
            }

            set
            {
                this.editableCode = value;
                this.IsCodePresent = !string.IsNullOrWhiteSpace(value);
                this.OnPropertyChanged("EditableCode");
            }
        }

        /// <summary>
        /// Gets or sets the Errors
        /// </summary>
        public ObservableCollection<CodeErrorViewModel> Errors
        {
            get
            {
                return this.errors;
            }

            set
            {
                this.errors = value;
                this.OnPropertyChanged("Errors");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether HasBuildErrors
        /// </summary>
        public bool HasBuildErrors
        {
            get
            {
                return this.hasBuildErrors;
            }

            set
            {
                this.hasBuildErrors = value;
                this.OnPropertyChanged("HasBuildErrors");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsCodePresent
        /// </summary>
        public bool IsCodePresent
        {
            get
            {
                return this.isCodePresent;
            }

            set
            {
                this.isCodePresent = value;
                this.OnPropertyChanged("IsCodePresent");
            }
        }

        /// <summary>
        /// Gets or sets the Snippets
        /// </summary>
        public List<SnippetViewModel> Snippets
        {
            get
            {
                return this.snippets;
            }

            set
            {
                this.snippets = value;
                this.OnPropertyChanged("Snippets");
            }
        }

        /// <summary>
        /// Gets or sets the StatusMessage
        /// </summary>
        public string StatusMessage
        {
            get
            {
                return this.statusMessage;
            }

            set
            {
                this.statusMessage = value;
                this.OnPropertyChanged("StatusMessage");
            }
        }

        #region Commands

        /// <summary>
        /// Gets the AddCodeSnippetCommand
        /// </summary>
        public RelayCommand<SnippetViewModel> AddCodeSnippetCommand
        {
            get
            {
                if (this.addCodeSnippetCommand == null)
                {
                    this.addCodeSnippetCommand = new RelayCommand<SnippetViewModel>(command => this.ExecuteAddCodeSnippet(command));
                }

                return this.addCodeSnippetCommand;
            }
        }

        /// <summary>
        /// Gets the CodeChangedCommand
        /// </summary>
        public RelayCommand CodeChangedCommand
        {
            get
            {
                if (this.codeChangedCommand == null)
                {
                    this.codeChangedCommand = new RelayCommand(command => this.ExecuteCodeChanged());
                }

                return this.codeChangedCommand;
            }
        }

        /// <summary>
        /// Gets the CompileCodeCommand
        /// </summary>
        public RelayCommand CompileCodeCommand
        {
            get
            {
                if (this.compileCodeCommand == null)
                {
                    this.compileCodeCommand = new RelayCommand(command => this.ExecuteCompileCode(), can => this.IsCodePresent);
                }

                return this.compileCodeCommand;
            }
        }

        /// <summary>
        /// Gets the PreviewCodeCommand
        /// </summary>
        public RelayCommand PreviewCodeCommand
        {
            get
            {
                if (this.previewCodeCommand == null)
                {
                    this.previewCodeCommand = new RelayCommand(command => this.ExecutePreviewCode(), can => this.IsCodePresent);
                }

                return this.previewCodeCommand;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The AttachView
        /// </summary>
        /// <param name = "view">The <see cref = "IRuntimeCodeView"/></param>
        public void AttachView(IRuntimeCodeView view)
        {
            this.view = view;
        }

        /// <summary>
        /// The CompileCode
        /// </summary>
        /// <returns>The <see cref = "bool "/></returns>
        public bool CompileCode()
        {
            bool buildSuccess = false;
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(this.Code);
            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location), MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location), MetadataReference.CreateFromFile(typeof(ExecuteScriptViewModel).Assembly.Location) };
            CSharpCompilation compilation = CSharpCompilation.Create(assemblyName, syntaxTrees: new[] { syntaxTree }, references: references, options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            using (var ms = new MemoryStream())
            {
                var errorlist = new List<CodeErrorViewModel>();
                EmitResult result = compilation.Emit(ms);
                if (!result.Success)
                {
                    this.HasBuildErrors = true;
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);
                    foreach (Diagnostic diagnostic in failures)
                    {
                        CodeErrorViewModel error = new CodeErrorViewModel();
                        error.Code = diagnostic.Id;
                        error.Description = diagnostic.GetMessage();
                        error.Severity = diagnostic.Severity.ToString();
                        errorlist.Add(error);
                    }
                }
                else
                {
                    this.HasBuildErrors = false;
                    buildSuccess = true;
                }

                this.Errors = new ObservableCollection<CodeErrorViewModel>(errorlist);
                return buildSuccess;
            }
        }

        /// <summary>
        /// The LoadCode
        /// </summary>
        /// <param name = "start">The <see cref = "string "/></param>
        /// <param name = "end">The <see cref = "string "/></param>
        public void LoadCode(string start, string indentation, string end)
        {
            this.startCode = start;
            this.indentation = indentation;
            this.endCode = end;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Executes AddCodeSnippet
        /// </summary>
        private void ExecuteAddCodeSnippet(SnippetViewModel snippet)
        {
            if (snippet == null)
            {
                return;
            }

            var text = this.EditableCode ?? string.Empty;
            var index = this.CodeCaretIndex;

            if (index > text.Length)
            {
                index = text.Length;
            }

            text = text.Insert(index, snippet.Code + Environment.NewLine);

            this.EditableCode = text;
            this.CodeCaretIndex = index + (snippet.Code + Environment.NewLine).Length;
        }

        /// <summary>
        /// Executes CodeChanged
        /// </summary>
        private void ExecuteCodeChanged()
        {
            this.IsCodePresent = string.IsNullOrWhiteSpace(this.EditableCode) == false;
            this.HasBuildErrors = false;
            this.Errors = null;
            this.StatusMessage = string.Empty;
        }

        /// <summary>
        /// Executes CompileCode
        /// </summary>
        private void ExecuteCompileCode()
        {
            try
            {
                Task.Run(() =>
                {
                    this.StatusMessage = "Building....please wait...";
                    bool buildSucceded = this.CompileCode();
                    this.StatusMessage = buildSucceded ? "Build succeeded" : "Build failed";
                });
            }
            catch (Exception ex)
            {
                this.view.MessageShow("Error", ex.Message);
                this.LogException(ex);
            }
        }

        /// <summary>
        /// Executes PreviewCode
        /// </summary>
        private void ExecutePreviewCode()
        {
            view.ViewCodePreview(this.Code);
        }

        /// <summary>
        /// The GetIndentationCode
        /// </summary>
        /// <param name="editableCode">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string GetIndentationCode(string editableCode)
        {
            var linesInText = editableCode.GetString().Replace("\r\n", "\n").Split('\n');

            StringBuilder stringWithRowNumbers = new StringBuilder();

            foreach (var line in linesInText)
            {
                stringWithRowNumbers.AppendLine(this.indentation + line);
            }
            string result = stringWithRowNumbers.ToString();

            return result;
        }

        #endregion

        #endregion
    }
}
