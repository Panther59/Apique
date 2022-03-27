// <copyright file="ucsettings.xaml.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>12-08-2017</date>

namespace RestClientLibrary.Screen
{
    using System.Windows;
    using DataLibrary;
    using Microsoft.Win32;
    using RestClientLibrary.Screen.Windows;
    using RestClientLibrary.View;
    using RestClientLibrary.ViewModel;

    /// <summary>
    /// Interaction logic for ucSettings.xaml
    /// </summary>
    public partial class ucSettings : BaseUserControl, ISettingsView
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ucSettings"/> class.
        /// </summary>
        public ucSettings()
        {
            InitializeComponent();
            this.DataContextChanged += this.UcSettings_DataContextChanged;
        }

        public CertificateViewModel AddNewCertificate(bool file)
        {
            AddCertificateWindow window = new AddCertificateWindow();
            window.IsCertPath = file;
            window.Resources = (System.Windows.Window.GetWindow(this)).Resources;
            window.ShowDialog(this);

            return window.Certificate;
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The OpenExportWindow
        /// </summary>
        public void OpenExportWindow()
        {
            ExportDataWindow window = new ExportDataWindow(null);
            window.Resources = Window.GetWindow(this).Resources;
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
            window.Resources = Window.GetWindow(this).Resources;
            window.ShowDialog(this);

            return window.IsImported;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The UcSettings_DataContextChanged
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/></param>
        private void UcSettings_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext is SettingsViewModel)
            {
                ((SettingsViewModel)this.DataContext).AttachView(this as ISettingsView);
            }
        }

        #endregion

        #endregion
    }
}
