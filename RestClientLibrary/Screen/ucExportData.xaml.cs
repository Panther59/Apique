// <copyright file="ucExportData.xaml.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>11-08-2017</date>

namespace RestClientLibrary.Screen
{
    using System.Windows;
    using DataLibrary;
    using RestClientLibrary.Common;
    using RestClientLibrary.View;
    using RestClientLibrary.ViewModel;

    /// <summary>
    /// Interaction logic for ucExportData.xaml
    /// </summary>
    public partial class ucExportData : BaseUserControl, IExportView
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ucExportData"/> class.
        /// </summary>
        public ucExportData()
        {
            InitializeComponent();
            this.Loaded += this.UcExportData_Loaded;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Path
        /// </summary>
        public string Path
        {
            get; set;
        }
        public ExportDataViewModel ViewModel { get; private set; }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The SaveFileDialog
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string SaveFileDialog()
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = Constants.ExportImportFilter;
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                return dialog.FileName;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The UcExportData_Loaded
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="RoutedEventArgs"/></param>
        private void UcExportData_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel = new ExportDataViewModel();
            this.ViewModel.AttachView(this);
            if (this.Path != null)
            {
                this.ViewModel.ImportData(Path);
            }
            else
            {
                this.ViewModel.LoadData();
            }

            this.DataContext = this.ViewModel;
        }

        #endregion

        #endregion
    }
}
