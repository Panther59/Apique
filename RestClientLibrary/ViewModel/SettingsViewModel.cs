// <copyright file="SettingsViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel
{
	using System;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Xml.Serialization;
	using DataLibrary;
	using RestClientLibrary.Common;
	using RestClientLibrary.Model;
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
		/// The certificates field
		/// </summary>
		private ObservableCollection<CertificateViewModel> certificates;

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
		private RelayCommand addSavedCertificateCommand;
		private RelayCommand addCertificateFileCommand;
		private RelayCommand<CertificateViewModel> removeCertificateCommand;

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
			this.Certificates = new ObservableCollection<CertificateViewModel>();
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

		/// <summary>
		/// Gets or sets the Certificates
		/// </summary>
		public ObservableCollection<CertificateViewModel> Certificates
		{
			get
			{
				return this.certificates;
			}

			set
			{
				this.certificates = value;
				this.OnPropertyChanged("Certificates");
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
		/// Gets the AddSavedCertificateCommand
		/// </summary>
		[XmlIgnore]
		public RelayCommand AddSavedCertificateCommand
		{
			get
			{
				if (this.addSavedCertificateCommand == null)
				{
					this.addSavedCertificateCommand = new RelayCommand(command => this.ExecuteAddSavedCertificate());
				}

				return this.addSavedCertificateCommand;
			}
		}

		/// <summary>
		/// Gets the AddCertificateFileCommand
		/// </summary>
		[XmlIgnore]
		public RelayCommand AddCertificateFileCommand
		{
			get
			{
				if (this.addCertificateFileCommand == null)
				{
					this.addCertificateFileCommand = new RelayCommand(command => this.ExecuteAddCertificateFile());
				}

				return this.addCertificateFileCommand;
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

		[XmlIgnore]
		public RelayCommand<CertificateViewModel> RemoveCertificateCommand
		{
			get
			{
				if (removeCertificateCommand == null)
				{
					removeCertificateCommand = new RelayCommand<CertificateViewModel>(command => RemoveCertificateClicked(command));
				}
				return removeCertificateCommand;
			}
			set { removeCertificateCommand = value; }
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

		public static SettingsViewModel Parse(SettingsModel model)
		{
			SettingsViewModel vm = new SettingsViewModel();
			vm.CertificateSupport = model.CertificateSupport ?? true;
			vm.ShowRequestContentInHistory = model.ShowRequestContentInHistory ?? false;
			vm.SearchInStatus = model.SearchInStatus ?? true;
			vm.MaxHistoryDays = model.MaxHistoryDays ?? 90;
			vm.SearchInHeaders = model.SearchInHeaders ?? true;
			vm.SearchInRequest = model.SearchInRequest ?? true;
			vm.Certificates = new ObservableCollection<CertificateViewModel>(model.Certificates?.Select(x => CertificateViewModel.Parse(x)) ?? new ObservableCollection<CertificateViewModel>());

			return vm;
		}

		public static SettingsModel ToModel(SettingsViewModel model)
		{
			SettingsModel vm = new SettingsModel();
			vm.CertificateSupport = model.CertificateSupport;
			vm.ShowRequestContentInHistory = model.ShowRequestContentInHistory;
			vm.SearchInStatus = model.SearchInStatus;
			vm.MaxHistoryDays = model.MaxHistoryDays;
			vm.SearchInHeaders = model.SearchInHeaders;
			vm.SearchInRequest = model.SearchInRequest;
			vm.Certificates = model.Certificates?.Select(x => x.ToModel())?.ToList();

			return vm;
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

		private void RemoveCertificateClicked(CertificateViewModel command)
		{
			this.Certificates.Remove(command);
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

		private void ExecuteAddSavedCertificate()
		{
			var cert = this._view.AddNewCertificate(false);
			if (cert != null)
			{
				if (!this.Certificates.Any(x => x.Thumbprint == cert.Thumbprint))
				{
					this.Certificates.Add(cert);
				}
			}
		}

		private void ExecuteAddCertificateFile()
		{
			var cert = this._view.AddNewCertificate(true);
			if (cert != null)
			{
				if (!this.Certificates.Any(x => x.FriendlyName == cert.Thumbprint))
				{
					this.Certificates.Add(cert);
				}
			}
		}

		#endregion

		#endregion
	}
}
