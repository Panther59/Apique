// <copyright file="SettingsViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using System;
    using System.Xml.Serialization;
    using DataLibrary;
    using RestClientLibrary.Common;
    using RestClientLibrary.View;

    /// <summary>
    /// Defines the <see cref="SettingsViewModel" />
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// Defines the 
        /// </summary>
        private bool _isCertificatePopupOpen;

        /// <summary>
        /// Defines the 
        /// </summary>
        private int _maxHistoryDays = 90;

        /// <summary>
        /// Defines the 
        /// </summary>
        private bool _searchInHeaders;

        /// <summary>
        /// Defines the 
        /// </summary>
        private bool _searchInRequest;

        /// <summary>
        /// Defines the 
        /// </summary>
        private bool _searchInStatus;

        /// <summary>
        /// Defines the 
        /// </summary>
        private bool _showRequestContentInHistory;

        /// <summary>
        /// Defines the 
        /// </summary>
        private ISettingsView _view;

        /// <summary>
        /// The certificateSupport field
        /// </summary>
        private bool certificateSupport;

        #region Commands

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand _saveButtonClickedCommand;

        /// <summary>
        /// The exportDataCommand field
        /// </summary>
        private RelayCommand exportDataCommand;

        /// <summary>
        /// The importDataCommand field
        /// </summary>
        private RelayCommand importDataCommand;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="view">The <see cref="IWorkspaceView"/></param>
        public SettingsViewModel(ISettingsView view)
            : this()
        {
            AttachView(view);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        public SettingsViewModel()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether CertificateSupport
        /// </summary>
        public bool CertificateSupport
        {
            get
            {
                return this.certificateSupport;
            }

            set
            {
                this.certificateSupport = value;
                this.OnPropertyChanged("CertificateSupport");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsCertificatePopupOpen
        /// </summary>
        [XmlIgnore]
        public bool IsCertificatePopupOpen
        {
            get { return _isCertificatePopupOpen; }
            set
            {
                _isCertificatePopupOpen = value;
                OnPropertyChanged("IsCertificatePopupOpen");
            }
        }

        /// <summary>
        /// Gets or sets the MaxHistoryDays
        /// </summary>
        public int MaxHistoryDays
        {
            get { return _maxHistoryDays; }
            set
            {
                _maxHistoryDays = value;
                OnPropertyChanged("MaxHistoryDays");
            }
        }

        /// <summary>
        /// Gets or sets the ParentViewModel
        /// </summary>
        [XmlIgnore]
        public WorkspaceViewModel ParentViewModel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether SearchInHeaders
        /// </summary>
        public bool SearchInHeaders
        {
            get { return _searchInHeaders; }
            set
            {
                _searchInHeaders = value;
                this.OnPropertyChanged("SearchInHeaders");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether SearchInRequest
        /// </summary>
        public bool SearchInRequest
        {
            get { return _searchInRequest; }
            set
            {
                _searchInRequest = value;
                this.OnPropertyChanged("SearchInRequest");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether SearchInStatus
        /// </summary>
        public bool SearchInStatus
        {
            get { return _searchInStatus; }
            set
            {
                _searchInStatus = value;
                this.OnPropertyChanged("SearchInStatus");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ShowRequestContentInHistory
        /// </summary>
        public bool ShowRequestContentInHistory
        {
            get { return _showRequestContentInHistory; }
            set
            {
                _showRequestContentInHistory = value;
                OnPropertyChanged("ShowRequestContentInHistory");
            }
        }

        #region Commands

        /// <summary>
        /// Gets the ExportDataCommand
        /// </summary>
        [XmlIgnore]
        public RelayCommand ExportDataCommand
        {
            get
            {
                if (this.exportDataCommand == null)
                {
                    this.exportDataCommand = new RelayCommand(command => this.ExecuteExportData());
                }

                return this.exportDataCommand;
            }
        }

        /// <summary>
        /// Gets the ImportDataCommand
        /// </summary>
        [XmlIgnore]
        public RelayCommand ImportDataCommand
        {
            get
            {
                if (this.importDataCommand == null)
                {
                    this.importDataCommand = new RelayCommand(command => this.ExecuteImportData());
                }

                return this.importDataCommand;
            }
        }

        /// <summary>
        /// Gets or sets the SaveButtonClickedCommand
        /// </summary>
        [XmlIgnore]
        public RelayCommand SaveButtonClickedCommand
        {
            get
            {
                if (_saveButtonClickedCommand == null)
                {
                    _saveButtonClickedCommand = new RelayCommand(command => SaveButtonClicked());
                }
                return _saveButtonClickedCommand;
            }
            set { _saveButtonClickedCommand = value; }
        }

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The AttachView
        /// </summary>
        /// <param name="view">The <see cref="IWorkspaceView"/></param>
        public void AttachView(ISettingsView view)
        {
            _view = view;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Executes ExportData
        /// </summary>
        private void ExecuteExportData()
        {
            this._view.OpenExportWindow();
        }

        /// <summary>
        /// Executes ImportData
        /// </summary>
        private void ExecuteImportData()
        {
            var path = this._view.OpenFile(Constants.ExportImportFilter);
            if (path != null)
            {
                bool imported = this._view.OpenImportWindow(path);
                if (imported)
                {
                    this.ParentViewModel?.ReloadAfterImport();
                }
            }
        }

        /// <summary>
        /// The SaveButtonClicked
        /// </summary>
        private void SaveButtonClicked()
        {
            try
            {
                AppDataHelper.SaveSettingsData(this);
                if (this._view != null)
                {
                    this._view.CloseParentWindow();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        #endregion

        #endregion
    }
}
