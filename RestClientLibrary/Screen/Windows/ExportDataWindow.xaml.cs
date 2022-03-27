// <copyright file="ExportDataWindow.xaml.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>13-08-2017</date>

namespace RestClientLibrary.Screen.Windows
{
    using DataLibrary;

    /// <summary>
    /// Interaction logic for ExportDataWindow.xaml
    /// </summary>
    public partial class ExportDataWindow : BaseWindow
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportDataWindow"/> class.
        /// </summary>
        /// <param name="path">The <see cref="string"/></param>
        public ExportDataWindow(string path)
        {
            InitializeComponent();
            this.ExportData.Path = path;
            this.Closing += this.ExportDataWindow_Closing;
        }

        private void ExportDataWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.IsImported = this.ExportData.ViewModel.IsImportSuccessful;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether IsImported
        /// </summary>
        public bool IsImported
        {
            get; set;
        }

        #endregion
    }
}
