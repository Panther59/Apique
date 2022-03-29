// <copyright file="GlobalSetupViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>22-08-2017</date>

namespace RestClientLibrary.ViewModel
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Threading.Tasks;
	using DataLibrary;
	using RestClientLibrary.Common;
	using RestClientLibrary.Model;
	using RestClientLibrary.View;

	/// <summary>
	/// Defines the <see cref = "GlobalSetupViewModel"/>
	/// </summary>
	public class GlobalSetupViewModel : BaseViewModel
	{
		#region Fields

		/// <summary>
		/// Defines the view
		/// </summary>
		private IGlobalSetupView view;
		private RelayCommand deleteWorkspaceCommand;

		/// <summary>
		/// The certificates field
		/// </summary>
		private ObservableCollection<CertificateViewModel> certificates;

		/// <summary>
		/// The environments field
		/// </summary>
		private ObservableCollection<EnvironmentViewModel> environments;

		/// <summary>
		/// The variables field
		/// </summary>
		private ObservableCollection<KeyValueViewModel> variables;

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
		/// The cloneEnvironmentCommand field
		/// </summary>
		private RelayCommand<EnvironmentViewModel> cloneEnvironmentCommand;
		private RelayCommand<EnvironmentViewModel> viewEnvironmentCommand;

		/// <summary>
		/// The deleteEnvironmentCommand field
		/// </summary>
		private RelayCommand<EnvironmentViewModel> deleteEnvironmentCommand;

		/// <summary>
		/// The environmentChangedCommand field
		/// </summary>
		private RelayCommand addEnvironmentCommand;
		private RelayCommand selectedWorkspaceChangedCommand;

		/// <summary>
		/// The removeCertificateCommand field
		/// </summary>
		private RelayCommand<CertificateViewModel> removeCertificateCommand;

		/// <summary>
		/// The removeVariableCommand field
		/// </summary>
		private RelayCommand<KeyValueViewModel> removeVariableCommand;
		private ObservableCollection<string> workspaces;
		private ObservableCollection<string> allWorkspaces;
		private string selectedWorkspace;

		#endregion

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref = "GlobalSetupViewModel"/> class.
		/// </summary>
		/// <param name = "view">The <see cref = "IGlobalSetupView"/></param>
		public GlobalSetupViewModel()
		{
		}

		public GlobalSetupViewModel(IGlobalSetupView view) : this()
		{
			this.view = view;
		}

		public void AttachView(IGlobalSetupView view)
		{
			this.view = view;
		}

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

		public List<EnvironmentViewModel> allEnvironments;

		/// <summary>
		/// Gets or sets the Environments
		/// </summary>
		public ObservableCollection<EnvironmentViewModel> Environments
		{
			get
			{
				return this.environments;
			}

			set
			{
				this.environments = value;
				this.OnPropertyChanged("Environments");
			}
		}

		/// <summary>
		/// Gets or sets the SelectedEnvironment
		/// </summary>
		public EnvironmentViewModel SelectedEnvironment { get; set; }

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
		public ObservableCollection<string> AllWorkspaces
		{
			get
			{
				return this.allWorkspaces;
			}

			set
			{
				this.allWorkspaces = value;
				this.OnPropertyChanged("AllWorkspaces");
			}
		}

		public string SelectedWorkspace
		{
			get
			{
				return this.selectedWorkspace;
			}

			set
			{
				this.selectedWorkspace = value;
				this.OnPropertyChanged("SelectedWorkspace");
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

		public RelayCommand SelectedWorkspaceChangedCommand
		{
			get
			{
				if (this.selectedWorkspaceChangedCommand == null)
				{
					this.selectedWorkspaceChangedCommand = new RelayCommand(command => this.ExecuteSelectedWorkspaceChanged());
				}

				return this.selectedWorkspaceChangedCommand;
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
		/// Gets the CloneEnvironmentCommand
		/// </summary>
		public RelayCommand<EnvironmentViewModel> CloneEnvironmentCommand
		{
			get
			{
				if (this.cloneEnvironmentCommand == null)
				{
					this.cloneEnvironmentCommand = new RelayCommand<EnvironmentViewModel>(command => this.ExecuteCloneEnvironment(command), can => CanCloneEnvironmentExecuted());
				}

				return this.cloneEnvironmentCommand;
			}
		}

		/// <summary>
		/// Gets the CloneEnvironmentCommand
		/// </summary>
		public RelayCommand<EnvironmentViewModel> ViewEnvironmentCommand
		{
			get
			{
				if (this.viewEnvironmentCommand == null)
				{
					this.viewEnvironmentCommand = new RelayCommand<EnvironmentViewModel>(command => this.ExecuteViewEnvironment(command));
				}

				return this.viewEnvironmentCommand;
			}
		}


		/// <summary>
		/// Gets the DeleteEnvironmentCommand
		/// </summary>
		public RelayCommand<EnvironmentViewModel> DeleteEnvironmentCommand
		{
			get
			{
				if (this.deleteEnvironmentCommand == null)
				{
					this.deleteEnvironmentCommand = new RelayCommand<EnvironmentViewModel>(command => this.ExecuteDeleteEnvironment(command), can => this.CanDeleteEnvironmentExecute());
				}

				return this.deleteEnvironmentCommand;
			}
		}

		public RelayCommand DeleteWorkspaceCommand
		{
			get
			{
				if (this.deleteWorkspaceCommand == null)
				{
					this.deleteWorkspaceCommand = new RelayCommand(command => this.ExecuteDeleteWorkspace(), can => this.CanDeleteWorkspaceExecute());
				}

				return this.deleteWorkspaceCommand;
			}
		}

		/// <summary>
		/// Gets the EnvironmentChangedCommand
		/// </summary>
		public RelayCommand AddEnvironmentCommand
		{
			get
			{
				if (this.addEnvironmentCommand == null)
				{
					this.addEnvironmentCommand = new RelayCommand(command => this.ExecuteAddEnvironmentCommand());
				}

				return this.addEnvironmentCommand;
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
		/// The LoadData
		/// </summary>
		public void LoadData(EnvironmentModel selectedEnvironment, string workspace)
		{
			var settings = AppDataHelper.LoadSettingsData();


			if (selectedEnvironment != null && selectedEnvironment.Workspace == null)
			{
				selectedEnvironment.Workspace = Constants.DefaultWorkspace;
			}

			this.SelectedWorkspace = selectedEnvironment?.Workspace ?? workspace ?? Constants.DefaultWorkspace;
			this.Environments = new ObservableCollection<EnvironmentViewModel>(this.allEnvironments.Where(x => x.Workspace == this.SelectedWorkspace));
		}

		/// <summary>
		/// The SaveData
		/// </summary>
		/// <returns>The <see cref = "bool "/></returns>
		public async Task<bool> SaveData()
		{
			bool anyError = this.ValidateSave();
			if (anyError == false)
			{
				var model = this.ToModel();
				await AppDataHelper.SaveGlobalVariablesData(model);
			}

			return anyError;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// The AddNewEnvironment
		/// </summary>
		/// <param name="input">The <see cref="EnvironmentViewModel"/></param>
		private void AddNewEnvironment(EnvironmentViewModel input)
		{
			var environment = this.view.AddNewEnvironment(this, input);
			if (environment != null)
			{
				if (!this.allEnvironments.Any(x => x.Guid == environment.Guid))
				{
					this.allEnvironments.Add(environment);
				}

				this.SelectedEnvironment = environment;
				this.Environments = new ObservableCollection<EnvironmentViewModel>(this.allEnvironments?.Where(x => x.Workspace == this.SelectedWorkspace) ?? new List<EnvironmentViewModel>());
				//if (environment != null)
				//{
				//	this.SelectedEnvironment = this.Environments.FirstOrDefault(x => x.Name == this.SelectedEnvironment?.Name);
				//}

				SaveData();
			}
			else
			{
				//this.SelectedEnvironment = this.Environments.First();
			}
		}

		private void ExecuteViewEnvironment(EnvironmentViewModel command)
		{
			this.AddNewEnvironment(command);
		}

		/// <summary>
		/// The CanCloneEnvironmentExecuted
		/// </summary>
		/// <returns>The <see cref="bool"/></returns>
		private bool CanCloneEnvironmentExecuted()
		{
			return true;
		}

		/// <summary>
		/// The CanDeleteEnvironmentExecute
		/// </summary>
		/// <returns>The <see cref="bool"/></returns>
		private bool CanDeleteEnvironmentExecute()
		{
			return CanCloneEnvironmentExecuted();
		}

		/// <summary>
		/// Executes AddNewCertificate
		/// </summary>
		private void ExecuteAddNewCertificate()
		{
			var certificate = this.view.AddNewCertificate();
			if (certificate != null)
			{
				if (this.Certificates == null)
				{
					this.Certificates = new ObservableCollection<CertificateViewModel>();
				}

				this.Certificates.Add(certificate);

				SaveData();
			}
		}

		private void ExecuteSelectedWorkspaceChanged()
		{
			if (this.SelectedWorkspace != null && this.SelectedWorkspace == Constants.AddNew)
			{
				string workspace = this.view.AddNewWorkspace();
				if (!string.IsNullOrEmpty(workspace))
				{
					this.AllWorkspaces.Insert(AllWorkspaces.Count - 1, workspace);
					this.Workspaces.Add(workspace);
					this.SelectedWorkspace = workspace;
				}
				else
				{
					this.SelectedWorkspace = Constants.DefaultWorkspace;
				}


			}
			else
			{
				this.Environments = new ObservableCollection<EnvironmentViewModel>(this.allEnvironments?.Where(x => x.Workspace == this.SelectedWorkspace) ?? new List<EnvironmentViewModel>());
				//if (selectedEnvironment != null)
				//{
				//	this.SelectedEnvironment = this.Environments.FirstOrDefault(x => x.Name == selectedEnvironment.Name);
				//}
				//else
				//{
				//	//this.SelectedEnvironment = this.Environments.First();
				//}
			}
		}

		/// <summary>
		/// Executes AddNewVariable
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
		/// Executes CloneEnvironment
		/// </summary>
		private void ExecuteCloneEnvironment(EnvironmentViewModel vm)
		{
			this.AddNewEnvironment(this.GetClonedObject(vm));
		}

		/// <summary>
		/// Executes DeleteEnvironment
		/// </summary>
		private void ExecuteDeleteEnvironment(EnvironmentViewModel vm)
		{
			this.Environments.Remove(vm);

			SaveData();
			//this.SelectedEnvironment = this.Environments.FirstOrDefault();
		}

		/// <summary>
		/// Executes EnvironmentChanged
		/// </summary>
		private void ExecuteAddEnvironmentCommand()
		{
			this.AddNewEnvironment(new EnvironmentViewModel() { Workspace = this.SelectedWorkspace });
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
		private async Task ExecuteRemoveVariable(KeyValueViewModel variable)
		{
			this.Variables.Remove(variable);

			var model = this.ToModel();
			await AppDataHelper.SaveGlobalVariablesData(model);
		}

		/// <summary>
		/// The GetClonedObject
		/// </summary>
		/// <param name="input">The <see cref="EnvironmentViewModel"/></param>
		/// <returns>The <see cref="EnvironmentViewModel"/></returns>
		private EnvironmentViewModel GetClonedObject(EnvironmentViewModel input)
		{
			if (input == null)
			{
				return null;
			}

			return new EnvironmentViewModel
			{
				Name = input.Name + " - clone",
				Variables = new ObservableCollection<KeyValueViewModel>(input.Variables ?? new ObservableCollection<KeyValueViewModel>()),
				DefaultCertificate = input.DefaultCertificate,
				Workspace = input.Workspace
			};
		}

		/// <summary>
		/// The ToModel
		/// </summary>
		/// <returns>The <see cref = "GlobalVariableModel"/></returns>
		public GlobalVariableModel ToModel()
		{
			GlobalVariableModel output = new GlobalVariableModel();
			if (this.Variables != null)
			{
				output.Variables = this.Variables.Select(x => x.ToModel()).ToList();
			}

			if (this.Certificates != null)
			{
				output.Certificates = this.Certificates.Select(x => x.ToModel()).ToList();
			}

			if (this.Environments != null)
			{
				var environmentsToSave = this.allEnvironments.Where(x => x.Name != Constants.AddNew && x.Name != Constants.Select);
				output.Environments = environmentsToSave.Select(x => x.ToModel()).ToList();
			}

			output.Workspaces = this.Workspaces?.Where(x => x != Constants.AddNew).ToList();

			return output;
		}

		public static GlobalSetupViewModel Parse(GlobalVariableModel model)
		{
			GlobalSetupViewModel output = new GlobalSetupViewModel();
			output.Variables = new ObservableCollection<KeyValueViewModel>(model.Variables?.Select(x => KeyValueViewModel.Parse(x)) ?? new ObservableCollection<KeyValueViewModel>());
			output.Certificates = new ObservableCollection<CertificateViewModel>(model.Certificates?.Select(x => CertificateViewModel.Parse(x)) ?? new ObservableCollection<CertificateViewModel>());

			output.allEnvironments = model.Environments?.Select(x => EnvironmentViewModel.Parse(x)).ToList() ?? new List<EnvironmentViewModel>();

			var ws = model.Workspaces?.ToList() ?? new List<string>();
			if (ws.Count == 0)
			{
				ws.Add(Constants.DefaultWorkspace);
			}

			output.Workspaces = new ObservableCollection<string>(ws);
			ws.Add(Constants.AddNew);
			output.AllWorkspaces = new ObservableCollection<string>(ws);

			return output;
		}

		/// <summary>
		/// The ValidateSave
		/// </summary>
		/// <returns>The <see cref = "bool "/></returns>
		private bool ValidateSave()
		{
			return false;
		}

		private bool CanDeleteWorkspaceExecute()
		{
			return this.SelectedWorkspace != null && this.SelectedWorkspace != Constants.DefaultWorkspace && this.SelectedWorkspace != Constants.AddNew;
		}

		private void ExecuteDeleteWorkspace()
		{
			var ws = this.SelectedWorkspace;
			this.Workspaces.Remove(ws);
			this.SelectedWorkspace = Constants.DefaultWorkspace;

			SaveData();
		}

		#endregion

		#endregion
	}
}
