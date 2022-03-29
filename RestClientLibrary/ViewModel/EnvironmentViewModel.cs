// <copyright file="EnvironmentViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>12-08-2017</date>

namespace RestClientLibrary.ViewModel
{
	using System;
	using System.Collections.ObjectModel;
	using System.Linq;
	using DataLibrary;
	using RestClientLibrary.Common;
	using RestClientLibrary.Model;
	using RestClientLibrary.View;

	/// <summary>
	/// Defines the <see cref = "EnvironmentViewModel"/>
	/// </summary>
	public class EnvironmentViewModel : BaseViewModel
	{
		public EnvironmentViewModel()
		{
			if (string.IsNullOrEmpty(this.Guid))
			{
				this.Guid = System.Guid.NewGuid().ToString();
			}
		}

		#region Fields

		/// <summary>
		/// The certificates field
		/// </summary>
		private ObservableCollection<CertificateViewModel> certificates;

		/// <summary>
		/// The guid field
		/// </summary>
		private string guid;

		/// <summary>
		/// The name field
		/// </summary>
		private string name;

		/// <summary>
		/// The variables field
		/// </summary>
		private ObservableCollection<KeyValueViewModel> variables;
		private ObservableCollection<string> workspaces;
		private string workspace;
		private string defaultDertificate;
		

		public string DefaultCertificate
		{
			get
			{
				return this.defaultDertificate;
			}

			set
			{
				this.defaultDertificate = value;
				this.OnPropertyChanged("DefaultCertificate");
			}
		}
		/// <summary>
		/// Defines the view
		/// </summary>
		private IEnvironmentView view;

		public void LoadData(GlobalSetupViewModel globalVariables)
		{
			var settings = AppDataHelper.LoadSettingsData(null);
			this.Workspaces = globalVariables.Workspaces;
			if (string.IsNullOrEmpty(this.Workspace))
			{
				this.Workspace = Constants.DefaultWorkspace;
			}

			if (settings.Certificates != null)
			{
				this.Certificates = new ObservableCollection<CertificateViewModel>(settings.Certificates);
			}

			if (this.DefaultCertificate != null)
			{
				this.DefaultCertificate = this.Certificates.FirstOrDefault(x => x.Name == this.DefaultCertificate)?.Name;
			}
		}

		#region Commands

		/// <summary>
		/// The addNewCertificateCommand field
		/// </summary>
		private RelayCommand addNewCertificateCommand;

		/// <summary>
		/// The addNewVariableCommand field
		/// </summary>
		private RelayCommand addNewVariableCommand;

		/// <summary>
		/// The removeCertificateCommand field
		/// </summary>
		private RelayCommand<CertificateViewModel> removeCertificateCommand;

		/// <summary>
		/// The removeVariableCommand field
		/// </summary>
		private RelayCommand<KeyValueViewModel> removeVariableCommand;

		#endregion

		#endregion

		#region Properties

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
		public ObservableCollection<string> Workspaces
		{
			get
			{
				return this.workspaces;
			}

			set
			{
				this.workspaces = value;
				this.OnPropertyChanged("Workspaces");
			}
		}

		public string Workspace
		{
			get
			{
				return this.workspace;
			}

			set
			{
				this.workspace = value;
				this.OnPropertyChanged("Workspace");
			}
		}

		/// <summary>
		/// Gets or sets the Guid
		/// </summary>
		public string Guid
		{
			get
			{
				return this.guid;
			}

			set
			{
				this.guid = value;
				this.OnPropertyChanged("Guid");
			}
		}

		/// <summary>
		/// Gets or sets the Name
		/// </summary>
		public string Name
		{
			get
			{
				return this.name;
			}

			set
			{
				this.name = value;
				this.OnPropertyChanged("Name");
			}
		}

		/// <summary>
		/// Gets or sets the Variables
		/// </summary>
		public ObservableCollection<KeyValueViewModel> Variables
		{
			get
			{
				return this.variables;
			}

			set
			{
				this.variables = value;
				this.OnPropertyChanged("Variables");
			}
		}

		#region Commands

		/// <summary>
		/// Gets the AddNewCertificateCommand
		/// </summary>
		public RelayCommand AddNewCertificateCommand
		{
			get
			{
				if (this.addNewCertificateCommand == null)
				{
					this.addNewCertificateCommand = new RelayCommand(command => this.ExecuteAddNewCertificate());
				}

				return this.addNewCertificateCommand;
			}
		}

		/// <summary>
		/// Gets the AddNewVariableCommand
		/// </summary>
		public RelayCommand AddNewVariableCommand
		{
			get
			{
				if (this.addNewVariableCommand == null)
				{
					this.addNewVariableCommand = new RelayCommand(command => this.ExecuteAddNewVariable());
				}

				return this.addNewVariableCommand;
			}
		}

		/// <summary>
		/// Gets the RemoveCertificateCommand
		/// </summary>
		public RelayCommand<CertificateViewModel> RemoveCertificateCommand
		{
			get
			{
				if (this.removeCertificateCommand == null)
				{
					this.removeCertificateCommand = new RelayCommand<CertificateViewModel>(command => this.ExecuteRemoveCertificate(command));
				}

				return this.removeCertificateCommand;
			}
		}

		/// <summary>
		/// Gets the RemoveVariableCommand
		/// </summary>
		public RelayCommand<KeyValueViewModel> RemoveVariableCommand
		{
			get
			{
				if (this.removeVariableCommand == null)
				{
					this.removeVariableCommand = new RelayCommand<KeyValueViewModel>(command => this.ExecuteRemoveVariable(command));
				}

				return this.removeVariableCommand;
			}
		}

		#endregion

		#endregion

		#region Methods

		#region Public Methods

		/// <summary>
		/// The Parse
		/// </summary>
		/// <param name = "input">The <see cref = "EnvironmentModel"/></param>
		/// <returns>The <see cref = "EnvironmentViewModel"/></returns>
		public static EnvironmentViewModel Parse(EnvironmentModel input)
		{
			if (input == null)
			{
				return null;
			}

			EnvironmentViewModel output = new EnvironmentViewModel();
			output.Guid = input.Guid;
			output.Name = input.Name;
			output.Workspace = input.Workspace;
			if (input.Variables != null)
			{
				output.Variables = new ObservableCollection<KeyValueViewModel>(input.Variables.Select(x => KeyValueViewModel.Parse(x)));
			}

			if (input.DefaultCertificate != null)
			{
				output.DefaultCertificate = input.DefaultCertificate;
			}

			return output;
		}

		/// <summary>
		/// The AttachView
		/// </summary>
		/// <param name="view">The <see cref="IEnvironmentView"/></param>
		public void AttachView(IEnvironmentView view)
		{
			this.view = view;
		}

		/// <summary>
		/// The IsValid
		/// </summary>
		/// <returns>The <see cref="bool"/></returns>
		public bool IsValid()
		{
			return !string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(this.Workspace);
		}

		/// <summary>
		/// The ToModel
		/// </summary>
		/// <returns>The <see cref = "EnvironmentModel"/></returns>
		public EnvironmentModel ToModel()
		{
			return new EnvironmentModel
			{
				Guid = this.Guid,
				Name = this.Name,
				DefaultCertificate = this.DefaultCertificate,
				Variables = this.Variables?.Select(x => x.ToModel()).ToList(),
				Workspace = this.Workspace
			};
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Executes AddNewCertificate
		/// </summary>
		private void ExecuteAddNewCertificate()
		{
			var certificate = this.view?.AddNewCertificate();
			if (certificate != null)
			{
				if (this.Certificates == null)
				{
					this.Certificates = new ObservableCollection<CertificateViewModel>();
				}

				this.Certificates.Add(certificate);
			}
		}

		/// <summary>
		/// Executes AddNewVariableCommand
		/// </summary>
		private void ExecuteAddNewVariable()
		{
			if (this.Variables == null)
			{
				this.Variables = new ObservableCollection<KeyValueViewModel>();
			}

			this.Variables.Add(new KeyValueViewModel());
		}

		/// <summary>
		/// Executes RemoveCertificate
		/// </summary>
		private void ExecuteRemoveCertificate(CertificateViewModel input)
		{
			if (this.Certificates != null)
			{
				this.Certificates.Remove(input);
			}
		}

		/// <summary>
		/// Executes RemoveVariable
		/// </summary>
		private void ExecuteRemoveVariable(KeyValueViewModel variable)
		{
			this.Variables.Remove(variable);
		}

		#endregion

		#endregion
	}
}
