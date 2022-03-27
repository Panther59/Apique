// <copyright file="ucMain.xaml.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.Screen
{
    using DataLibrary;
    using Microsoft.Win32;
    using RestClientLibrary.Common;
    using RestClientLibrary.Model;
    using RestClientLibrary.Screen.Windows;
    using RestClientLibrary.View;
    using RestClientLibrary.ViewModel;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ucMain.xaml
    /// </summary>
    public partial class ucMain : BaseUserControl, IMainView
    {
        #region Fields

        /// <summary>
        /// Defines the 
        /// </summary>
        private MainViewModel _viewModel;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ucMain"/> class.
        /// </summary>
        public ucMain()
        {
            InitializeComponent();
            WindowServiceHandler.ConfirmationBox += this.WindowServiceHandler_ConfirmationBox;
            WindowServiceHandler.MessageBox += this.WindowServiceHandler_MessageBox;
            this.Loaded += ucMain_Loaded;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the HistoryView
        /// </summary>
        public IHistoryView HistoryView
        {
            get { return this.ucHistory as IHistoryView; }
        }

        /// <summary>
        /// Gets or sets the ViewModel
        /// </summary>
        public MainViewModel ViewModel { get => this._viewModel; set => this._viewModel = value; }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The OpenExportWindow
        /// </summary>
        public void OpenExportWindow()
        {
            ExportDataWindow window = new ExportDataWindow(null);
            window.Resources = (this.Parent as Grid).Resources;
            window.ShowDialog(this);
        }

        /// <summary>
        /// The OpenFile
        /// </summary>
        /// <param name="filter">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public string OpenFile(string filter)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = filter;
            dialog.Multiselect = false;
            var result = dialog.ShowDialog();
            return result.HasValue && result.Value ? dialog.FileName : null;
        }

        /// <summary>
        /// The OpenImportWindow
        /// </summary>
        /// <param name="path">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool OpenImportWindow(string path)
        {
            ExportDataWindow window = new ExportDataWindow(path);
            window.Resources = (this.Parent as Grid).Resources;
            window.ShowDialog(this);

            return window.IsImported;
        }

        /// <summary>
        /// The OpenSaveRequestWindow
        /// </summary>
        /// <param name="data">The <see cref="RestClientLibrary.ViewModel.SaveRequestViewModel"/></param>
        /// <returns>The <see cref="bool?"/></returns>
        public bool? OpenSaveRequestWindow(RestClientLibrary.ViewModel.SaveRequestViewModel data)
        {
            SaveRequestWindow window = new SaveRequestWindow();
            window.Resources = (this.Parent as Grid).Resources;
            window.LoadData(data);
            return window.ShowDialog(this);
        }

        /// <inheritdoc />
        public void OpenTestRunner()
        {
            TestRunnerWindow window = new TestRunnerWindow();
            window.Resources = (this.Parent as Grid).Resources;
            window.ShowDialog(this);
        }

        /// <summary>
        /// The SaveFile
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string SaveFile()
        {
            SaveFileDialog window = new SaveFileDialog();
            window.Filter = "Text Files|*.txt";
            window.FileName = string.Format("Request/Response-{0}", DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            var result = window.ShowDialog(Window.GetWindow(this));
            if (result.HasValue && result.Value)
            {
                return window.FileName;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// The ViewEnvironmentWindow
        /// </summary>
        /// <param name="globalData">The <see cref="GlobalVariableModel"/></param>
        /// <param name="environment">The <see cref="EnvironmentModel"/></param>
        /// <returns>The <see cref="EnvironmentModel"/></returns>
        public EnvironmentModel ViewEnvironmentWindow(GlobalVariableModel globalData, EnvironmentModel environment)
        {
            GlobalSetupWindow window = new GlobalSetupWindow();
            window.Resources = (this.Parent as Grid).Resources;
            window.Environment = environment;
            window.GlobalData = globalData;
            window.ShowDialog(this);

            return window.Environment;
        }

        /// <summary>
        /// The ViewSettingsWindow
        /// </summary>
        public void ViewSettingsWindow(SettingsViewModel settings)
        {
            ViewAppSettingsWindow window = new ViewAppSettingsWindow();
            window.Resources = (this.Parent as Grid).Resources;
            window.ViewModel = settings;
            window.ShowDialog(this);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The ucMain_Loaded
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="RoutedEventArgs"/></param>
        private void ucMain_Loaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel == null)
            {
                ViewModel = new MainViewModel((IMainView)this);
            }
            this.DataContext = ViewModel;
        }

        /// <summary>
        /// The WindowServiceHandler_ConfirmationBox
        /// </summary>
        /// <param name="title">The <see cref="string"/></param>
        /// <param name="message">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool WindowServiceHandler_ConfirmationBox(string title, string message)
        {
            return this.ConfirmationBox(title, message);
        }

        /// <summary>
        /// The WindowServiceHandler_MessageBox
        /// </summary>
        /// <param name="title">The <see cref="string"/></param>
        /// <param name="message">The <see cref="string"/></param>
        private void WindowServiceHandler_MessageBox(string title, string message)
        {
            this.MessageShow(title, message);
        }

        #endregion

        #endregion
    }
}
