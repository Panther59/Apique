// <copyright file="MainViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>09-08-2017</date>

namespace RestClientLibrary.ViewModel
{
	using DataLibrary;
	using RestClientLibrary.Common;
	using RestClientLibrary.Model;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.IO;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Timers;
	using System.Windows.Input;

	/// <summary>
	/// Defines the <see cref="WorkspaceViewModel" />
	/// </summary>
	public class WorkspaceViewModel : BaseViewModel
	{
		/// <summary>
		/// Defines the 
		/// </summary>
		private HistoryViewModel _history;

		///// <summary>
		///// Defines the 
		///// </summary>
		//private RestClientViewModel _restClient;

		/// <summary>
		/// Defines the 
		/// </summary>
		private SaveRequestViewModel _saveRequest;

		/// <summary>
		/// Defines the 
		/// </summary>
		private RelayCommand<RestClientViewModel> _saveRequestCommand;

		/// <summary>
		/// Defines the 
		/// </summary>
		private int _selectedMainTab;

		/// <summary>
		/// Defines the 
		/// </summary>
		private RelayCommand<RestClientViewModel> _sendEmailCommand;

		/// <summary>
		/// Defines the 
		/// </summary>
		private SettingsViewModel _settings;

		/// <summary>
		/// Defines the 
		/// </summary>
		private RelayCommand _tabChangedCommand;

		/// <summary>
		/// Defines the 
		/// </summary>
		private View.IWorkspaceView _view;

		/// <summary>
		/// The closeRestClientCommand field
		/// </summary>
		private RelayCommand<RestClientViewModel> closeRestClientCommand;

		/// <summary>
		/// Defines the 
		/// </summary>
		private int counter = 0;

		/// <summary>
		/// The environmentChangedCommand field
		/// </summary>
		private RelayCommand<EnvironmentViewModel> environmentChangedCommand;

		/// <summary>
		/// The environmentManageCommand field
		/// </summary>
		private RelayCommand environmentManageCommand;

		/// <summary>
		/// The environments field
		/// </summary>
		private ObservableCollection<EnvironmentViewModel> environments;

		/// <summary>
		/// The environmentsViewData field
		/// </summary>
		private ViewEnvironmentsViewModel environmentsViewData;

		/// <summary>
		/// The globalVariables field
		/// </summary>
		private GlobalSetupViewModel globalVariables;

		/// <summary>
		/// The newRestClientCommand field
		/// </summary>
		private RelayCommand newRestClientCommand;

		/// <summary>
		/// The restClients field
		/// </summary>
		private ObservableCollection<RestClientViewModel> restClients;

		/// <summary>
		/// The saveAsRequestCommand field
		/// </summary>
		private RelayCommand<RestClientViewModel> saveAsRequestCommand;

		/// <summary>
		/// The selectedEnvironment field
		/// </summary>
		private EnvironmentViewModel selectedEnvironment;

		/// <summary>
		/// The selectedRestClient field
		/// </summary>
		private RestClientViewModel selectedRestClient;

		private string selectedWorkspace;

		/// <summary>
		/// The setMenus field
		/// </summary>
		private ObservableCollection<CustomContextMenuViewModel> setMenus;

		/// <summary>
		/// The testCommand field
		/// </summary>
		private RelayCommand<string> testCommand;

		/// <summary>
		/// The useVariableMenus field
		/// </summary>
		private ObservableCollection<CustomContextMenuViewModel> useVariableMenus;

		/// <summary>
		/// Defines the 
		/// </summary>
		private List<KeyValueViewModel> variables;

		/// <summary>
		/// The viewAppSettingsCommand field
		/// </summary>
		private RelayCommand viewAppSettingsCommand;

		/// <summary>
		/// The viewEnvironmentSettingsCommand field
		/// </summary>
		private RelayCommand viewEnvironmentSettingsCommand;

		/// <summary>
		/// The viewTestRunnerCommand field
		/// </summary>
		private RelayCommand viewTestRunnerCommand;

		private RelayCommand<string> workspaceChangedCommand;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkspaceViewModel"/> class.
		/// </summary>
		public WorkspaceViewModel()
		{
			var timer = new Timer();
			timer.Interval = 60 * 1000; // 60 secs
			timer.Elapsed += Timer_Elapsed;
			//timer.Start();
			MasterEventHandler.SaveSession += MasterEventHandler_SaveSession;
			MasterEventHandler.RemoveSession += MasterEventHandler_RemoveSession;
			MasterEventHandler.ReloadSavedRequest += MasterEventHandler_ReloadSavedRequest;
			MasterEventHandler.AppClosing += MasterEventHandler_AppClosing; ;

			Common.AppCommandManager.SetVariableValue += this.AppCommandManager_SetVariableValue;
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Task.Run(() => MasterEventHandler_AppClosing());
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkspaceViewModel"/> class.
		/// </summary>
		/// <param name="mainView">The <see cref="View.IWorkspaceView"/></param>
		public WorkspaceViewModel(View.IWorkspaceView mainView)
			: this()
		{
			this._view = mainView;
			LoadData();
		}

		///// <summary>
		///// Gets the Certificates
		///// </summary>
		//public ObservableCollection<CertificateModel> Certificates
		//{
		//	get
		//	{
		//		return this.certificates;
		//	}
		//	set
		//	{
		//		this.certificates = value;
		//		this.OnPropertyChanged("Certificates");
		//	}
		//}


		/// <summary>
		/// Gets the CloseRestClientCommand
		/// </summary>
		public RelayCommand<RestClientViewModel> CloseRestClientCommand
		{
			get
			{
				if (this.closeRestClientCommand == null)
				{
					this.closeRestClientCommand = new RelayCommand<RestClientViewModel>(command => this.ExecuteCloseRestClient(command), can => this.CanCloseRestClientExecute(can));
				}

				return this.closeRestClientCommand;
			}
		}

		/// <summary>
		/// Gets the EnvironmentChangedCommand
		/// </summary>
		public RelayCommand<EnvironmentViewModel> EnvironmentChangedCommand
		{
			get
			{
				if (this.environmentChangedCommand == null)
				{
					this.environmentChangedCommand = new RelayCommand<EnvironmentViewModel>(command => this.ExecuteEnvironmentChanged(command));
				}

				return this.environmentChangedCommand;
			}
		}

		/// <summary>
		/// Gets the EnvironmentManageCommand
		/// </summary>
		public RelayCommand EnvironmentManageCommand
		{
			get
			{
				if (this.environmentManageCommand == null)
				{
					this.environmentManageCommand = new RelayCommand(command => this.ExecuteEnvironmentManage());
				}

				return this.environmentManageCommand;
			}
		}

		//private List<EnvironmentViewModel> allEnvironments;



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
		/// Gets or sets the EnvironmentsViewData
		/// </summary>
		public ViewEnvironmentsViewModel EnvironmentsViewData
		{
			get
			{
				return this.environmentsViewData;
			}

			set
			{
				this.environmentsViewData = value;
				this.OnPropertyChanged("EnvironmentsViewData");
			}
		}

		/// <summary>
		/// Gets or sets the GlobalVariables
		/// </summary>
		public GlobalSetupViewModel GlobalVariables
		{
			get
			{
				return this.globalVariables;
			}

			set
			{
				this.globalVariables = value;
				this.SetViewEnvData();
			}
		}

		/// <summary>
		/// Gets or sets the History
		/// </summary>
		public HistoryViewModel History
		{
			get { return _history; }
			set
			{
				_history = value;
				OnPropertyChanged("History");
			}
		}

		/// <summary>
		/// Gets the NewRestClientCommand
		/// </summary>
		public RelayCommand NewRestClientCommand
		{
			get
			{
				if (this.newRestClientCommand == null)
				{
					this.newRestClientCommand = new RelayCommand(command => this.ExecuteNewRestClient());
				}

				return this.newRestClientCommand;
			}
		}

		/// <summary>
		/// Gets or sets the RestClients
		/// </summary>
		public ObservableCollection<RestClientViewModel> RestClients
		{
			get
			{
				return this.restClients;
			}

			set
			{
				this.restClients = value;
				this.OnPropertyChanged("RestClients");
			}
		}

		/// <summary>
		/// Gets the SaveAsRequestCommand
		/// </summary>
		public RelayCommand<RestClientViewModel> SaveAsRequestCommand
		{
			get
			{
				if (this.saveAsRequestCommand == null)
				{
					this.saveAsRequestCommand = new RelayCommand<RestClientViewModel>(command => this.ExecuteSaveAsRequest(command), can => this.CanSaveAsRequestExecute(can));
				}

				return this.saveAsRequestCommand;
			}
		}

		/// <summary>
		/// Gets or sets the SaveRequest
		/// </summary>
		public SaveRequestViewModel SaveRequest
		{
			get { return _saveRequest; }
			set
			{
				_saveRequest = value;
				OnPropertyChanged("SaveRequest");
			}
		}

		/// <summary>
		/// Gets or sets the SaveRequestCommand
		/// </summary>
		public RelayCommand<RestClientViewModel> SaveRequestCommand
		{
			get
			{
				if (_saveRequestCommand == null)
				{
					_saveRequestCommand = new RelayCommand<RestClientViewModel>(command => SaveRequestClicked(command), can => IsRestClientDataComplete(can));
				}
				return _saveRequestCommand;
			}
			set { _saveRequestCommand = value; }
		}

		/// <summary>
		/// Gets or sets the SelectedEnvironment
		/// </summary>
		public EnvironmentViewModel SelectedEnvironment
		{
			get
			{
				return this.selectedEnvironment;
			}

			set
			{
				if (value != null)
				{
					this.selectedEnvironment = value;
					this.OnPropertyChanged("SelectedEnvironment");
				}
				this.SetViewEnvData();
			}
		}

		/// <summary>
		/// Gets or sets the SelectedMainTab
		/// </summary>
		public int SelectedMainTab
		{
			get { return _selectedMainTab; }
			set
			{
				_selectedMainTab = value;
				OnPropertyChanged("SelectedMainTab");
			}
		}

		/// <summary>
		/// Gets or sets the SelectedRestClient
		/// </summary>
		public RestClientViewModel SelectedRestClient
		{
			get
			{
				return this.selectedRestClient;
			}

			set
			{
				this.selectedRestClient = value;
				this.OnPropertyChanged("SelectedRestClient");
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

		/// <summary>
		/// Gets or sets the SendEmailCommand
		/// </summary>
		public RelayCommand<RestClientViewModel> SendEmailCommand
		{
			get
			{
				if (_sendEmailCommand == null)
				{
					_sendEmailCommand = new RelayCommand<RestClientViewModel>(command => this.SendEmail(command), can => IsRestClientDataComplete(can));
				}
				return _sendEmailCommand;
			}
			set { _sendEmailCommand = value; }
		}

		/// <summary>
		/// Gets or sets the SetMenus
		/// </summary>
		public ObservableCollection<CustomContextMenuViewModel> SetMenus
		{
			get
			{
				return this.setMenus;
			}

			set
			{
				this.setMenus = value;
				this.OnPropertyChanged("SetMenus");
			}
		}

		/// <summary>
		/// Gets or sets the Settings
		/// </summary>
		public SettingsViewModel Settings
		{
			get { return _settings; }
			set
			{
				_settings = value;
				OnPropertyChanged("Settings");
			}
		}

		/// <summary>
		/// Gets or sets the TabChangedCommand
		/// </summary>
		public RelayCommand TabChangedCommand
		{
			get
			{
				if (_tabChangedCommand == null)
				{
					_tabChangedCommand = new RelayCommand(command => TabChanged());
				}
				return _tabChangedCommand;
			}
			set { _tabChangedCommand = value; }
		}

		/// <summary>
		/// Gets the TestCommand
		/// </summary>
		public RelayCommand<string> TestCommand
		{
			get
			{
				if (this.testCommand == null)
				{
					this.testCommand = new RelayCommand<string>(command => this.ExecuteTest(command), can => this.CanTest());
				}

				return this.testCommand;
			}
		}

		/// <summary>
		/// Gets or sets the UseVariableMenus
		/// </summary>
		public ObservableCollection<CustomContextMenuViewModel> UseVariableMenus
		{
			get
			{
				return this.useVariableMenus;
			}

			set
			{
				this.useVariableMenus = value;
				this.OnPropertyChanged("UseVariableMenus");
			}
		}

		/// <summary>
		/// Gets the Variables
		/// </summary>
		public List<KeyValueViewModel> Variables
		{
			get
			{
				return this.variables;
			}
		}

		/// <summary>
		/// Gets the ViewAppSettingsCommand
		/// </summary>
		public RelayCommand ViewAppSettingsCommand
		{
			get
			{
				if (this.viewAppSettingsCommand == null)
				{
					this.viewAppSettingsCommand = new RelayCommand(command => this.ExecuteViewAppSettings());
				}

				return this.viewAppSettingsCommand;
			}
		}

		/// <summary>
		/// Gets the ViewEnvironmentSettingsCommand
		/// </summary>
		public RelayCommand ViewEnvironmentSettingsCommand
		{
			get
			{
				if (this.viewEnvironmentSettingsCommand == null)
				{
					this.viewEnvironmentSettingsCommand = new RelayCommand(command => this.ExecuteViewEnvironmentSettings());
				}

				return this.viewEnvironmentSettingsCommand;
			}
		}

		/// <summary>
		/// Gets the ViewTestRunnerCommand
		/// </summary>
		public RelayCommand ViewTestRunnerCommand
		{
			get
			{
				if (this.viewTestRunnerCommand == null)
				{
					this.viewTestRunnerCommand = new RelayCommand(command => this.ExecuteViewTestRunner());
				}

				return this.viewTestRunnerCommand;
			}
		}

		/// <summary>
		/// Gets the EnvironmentChangedCommand
		/// </summary>
		public RelayCommand<string> WorkspaceChangedCommand
		{
			get
			{
				if (this.workspaceChangedCommand == null)
				{
					this.workspaceChangedCommand = new RelayCommand<string>(command => this.ExecuteWorkspaceChanged(command));
				}

				return this.workspaceChangedCommand;
			}
		}

		/// <summary>
		/// The BringRestClientToFocus
		/// </summary>
		public void BringRestClientToFocus()
		{
			MasterEventHandler.RaiseResponseReceivedEvent();
		}

		/// <summary>
		/// The EmailAsAttachment
		/// </summary>
		/// <param name="transaction">The <see cref="TransactionViewModel"/></param>
		public void EmailAsAttachment(TransactionViewModel transaction)
		{
			try
			{
				string tempPath = System.IO.Path.GetTempPath();
				string fileName = string.Format("Request&Response-{0}.txt", DateTime.Now.ToString("yyyyMMddhhmmssfff"));

				string content = this.SelectedRestClient.GetRequestResponseText(transaction);
				File.WriteAllText(tempPath + fileName, content);

				this.SendEmail(null, tempPath + fileName);
			}
			catch (Exception ex)
			{
				this.LogException(ex);
			}
		}

		/// <summary>
		/// The EmailInBody
		/// </summary>
		/// <param name="transaction">The <see cref="TransactionViewModel"/></param>
		public void EmailInBody(TransactionViewModel transaction)
		{
			try
			{
				string text = this.SelectedRestClient.GetRequestResponseText(transaction);
				this.SendEmail(text);
			}
			catch (Exception ex)
			{
				this.LogException(ex);
			}
		}

		/// <summary>
		/// The ReloadAfterImport
		/// </summary>
		public void ReloadAfterImport()
		{
			SaveRequest.LoadData(AppDataHelper.LoadSavedRequestData());

			var tempEnv = this.SelectedEnvironment;
			this.LoadEnvironmentData();
			this.SelectedEnvironment = this.Environments.FirstOrDefault(x => x.Guid == tempEnv?.Guid);

			var sessionData = AppDataHelper.LoadSessionHistoryData();
			sessionData = sessionData.Where(x => x.Time > DateTime.Now.AddDays(-1 * Settings.MaxHistoryDays)).ToList();
			var basicData = sessionData != null ? sessionData.Select(x => new BasicHistoryViewModel(x)) : null;
			History.LoadData(basicData != null ? basicData : null);
			if (History.SessionHistory != null)
			{
				var listUrl = History.SessionHistory
					.Select(s => new { Time = s.Time, Url = s.PreUrl ?? s.Url })
					.GroupBy(g => g.Url)
					.Select(x => new { Url = x.Key, Time = x.Max(a => a.Time) })
					.OrderByDescending(z => z.Time)
					.Select(u => new TextType { Text = u.Url });

				SelectedRestClient.UrlBase.Urls = new ObservableCollection<TextType>(listUrl);
			}
		}

		/// <summary>
		/// The RequestSelected
		/// </summary>
		/// <param name="req">The <see cref="TransactionViewModel"/></param>
		public void RequestSelected(TransactionViewModel req)
		{
			SessionHistoryViewModel session = new SessionHistoryViewModel(req);
			SessionSelected(session, null);
		}

		/// <summary>
		/// The SaveRequestClicked
		/// </summary>
		public async void SaveRequestClicked(RestClientViewModel restClient)
		{
			try
			{
				TransactionViewModel transaction = restClient.BuildRequest(restClient.IsSavedRequest ? restClient.Title : null);
				await this.SaveRequestClicked(transaction, restClient);
			}
			catch (Exception ex)
			{
				this.LogException(ex);
			}
		}

		/// <summary>
		/// The SaveRequestClicked
		/// </summary>
		/// <param name="transaction">The <see cref="TransactionViewModel"/></param>
		public async Task SaveRequestClicked(TransactionViewModel transaction, RestClientViewModel restClient = null)
		{
			bool? saved = null;
			if (string.IsNullOrWhiteSpace(transaction.Name))
			{
				SaveRequest.LoadData();
				SaveRequest.Transaction = transaction;
				saved = _view.OpenSaveRequestWindow(SaveRequest);
			}
			else
			{
				await AppDataHelper.SaveRequestData(transaction);
			}

			var newData = AppDataHelper.LoadSavedRequestData();


			var saveReq = newData?.Categories?.FirstOrDefault(x => x.Requests != null && x.Requests.Any(y => y.Guid == transaction.Guid))?.Requests?.FirstOrDefault(y => y.Guid == transaction.Guid);
			if (saveReq != null)
			{
				restClient.Title = saveReq.Name;
				restClient.IsSavedRequest = true;
				restClient.GUID = saveReq.Guid;
			}

			this.SaveRequest.LoadData(newData);
		}

		/// <summary>
		/// The SaveRequestToFile
		/// </summary>
		/// <param name="transaction">The <see cref="TransactionViewModel"/></param>
		public void SaveRequestToFile(TransactionViewModel transaction)
		{
			try
			{
				string text = this.SelectedRestClient.GetRequestResponseText(transaction);

				string filePath = this._view.SaveFile();

				if (filePath != null)
				{
					File.WriteAllText(filePath, text);
					this._view.MessageShow("Export", "Data exported successfully!!!");
				}
			}
			catch (Exception ex)
			{
				this.LogException(ex);
			}
		}

		/// <summary>
		/// The SessionSelected
		/// </summary>
		/// <param name="session">The <see cref="SessionHistoryViewModel"/></param>
		/// <param name="type">The <see cref="string"/></param>
		public void SessionSelected(SessionHistoryViewModel session, string type)
		{
			var existingTab = this.RestClients?.FirstOrDefault(x => x.GUID == session.Guid);

			if (existingTab != null)
			{
				this.SelectedRestClient = existingTab;
			}
			else
			{
				var restClient = this.AddNewRestClient();
				restClient.LoadSession(session, type);
				this.SelectedRestClient = restClient;
			}

			SelectedMainTab = (int)Constants.MainTab.RestClient;
		}

		/// <summary>
		/// The UpdateEnvironmentData
		/// </summary>
		public void UpdateEnvironmentData()
		{
			var selectedEnv = this.SelectedEnvironment;
			this.BuildVariables(this.GlobalVariables);
		}

		/// <summary>
		/// The AddNewRestClient
		/// </summary>
		/// <returns>The <see cref="RestClientViewModel"/></returns>
		private RestClientViewModel AddNewRestClient()
		{
			var blankRC = new RestClientViewModel() { Title = "Request " + ++counter };
			blankRC.ParentViewModel = this;
			blankRC.IsDefaultRestClient = true;
			blankRC.SelectCertificate(this.SelectedEnvironment?.DefaultCertificate ?? this.Settings.DefaultCertificate?.Name);
			if (this.History.SessionHistory != null)
			{
				var listUrl = this.History.SessionHistory
					.Select(s => new { Time = s.Time, Url = s.PreUrl ?? s.Url })
					.GroupBy(g => g.Url)
					.Select(x => new { Url = x.Key, Time = x.Max(a => a.Time) })
					.OrderByDescending(z => z.Time)
					.Select(u => new TextType { Text = u.Url });

				blankRC.UrlBase.Urls = new ObservableCollection<TextType>(listUrl);
			}

			if (this.RestClients == null)
			{
				this.RestClients = new ObservableCollection<RestClientViewModel>();
			}

			this.RestClients.Add(blankRC);

			return blankRC;
		}

		/// <summary>
		/// The AppCommandManager_SetVariableValue
		/// </summary>
		/// <param name="key">The <see cref="string"/></param>
		/// <param name="text">The <see cref="string"/></param>
		private void AppCommandManager_SetVariableValue(string key, string text)
		{
			if (string.IsNullOrEmpty(key) == false)
			{
				var parts = key.Split('|');
				if (parts.Length == 3)
				{
					var env = parts[0];
					var variable = parts[1];
					var action = parts[2];
					EnvironmentViewModel environmentToBeShown = null;
					if (env == "Global")
					{
						var matchVariables = this.GlobalVariables.Variables?.Where(x => x.Key.Equals(variable, StringComparison.CurrentCultureIgnoreCase));

						if (matchVariables != null && matchVariables.Any())
						{
							if (action == Constants.SetAction)
							{
								matchVariables.First().Value = text;

								AppDataHelper.SaveGlobalVariablesData(this.GlobalVariables.ToModel());
							}
						}
						else
						{
							if (this.GlobalVariables.Variables == null)
							{
								this.GlobalVariables.Variables = new ObservableCollection<KeyValueViewModel>();
							}

							var newVariable = new KeyValueViewModel { Value = text };
							this.GlobalVariables.Variables.Add(newVariable);
							AppDataHelper.SaveGlobalVariablesData(this.GlobalVariables.ToModel());
						}
					}
					else
					{
						if (this.GlobalVariables.Environments != null)
						{
							var matchEnvs = this.GlobalVariables.Environments.Where(x => x.Name.Equals(env, StringComparison.CurrentCultureIgnoreCase));

							if (matchEnvs.Any())
							{
								environmentToBeShown = matchEnvs.First();
								var matchVariables = matchEnvs.First().Variables?.Where(x => x.Key.Equals(variable, StringComparison.CurrentCultureIgnoreCase));

								if (matchVariables != null && matchVariables.Any())
								{
									if (action == Constants.SetAction)
									{
										matchVariables.First().Value = text;

										AppDataHelper.SaveGlobalVariablesData(this.GlobalVariables.ToModel());
									}
								}
								else
								{
									if (matchEnvs.First().Variables == null)
									{
										matchEnvs.First().Variables = new ObservableCollection<KeyValueViewModel>();
									}

									var newVariable = new KeyValueViewModel { Value = text };
									matchEnvs.First().Variables.Add(newVariable);
									AppDataHelper.SaveGlobalVariablesData(this.GlobalVariables.ToModel());
								}
							}
						}
					}

				}

			}
		}

		/// <summary>
		/// The BuildMenus
		/// </summary>
		/// <param name="action">The <see cref="string"/></param>
		/// <param name="command">The <see cref="ICommand"/></param>
		/// <returns>The <see cref="List{CustomContextMenuViewModel}"/></returns>
		private List<CustomContextMenuViewModel> BuildMenus(string action, ICommand command)
		{
			var menuList = new List<CustomContextMenuViewModel>();

			if (this.GlobalVariables.Variables != null)
			{
				var globVarMenu = new CustomContextMenuViewModel(action + ": Global", null, null);
				foreach (var variable in this.GlobalVariables.Variables)
				{
					if (string.IsNullOrEmpty(variable.Key) == false)
					{

						var globVarOptions1Menu = new CustomContextMenuViewModel(variable.Key, command, "Global|" + variable.Key + "|" + action);

						if (globVarMenu.Children == null)
						{
							globVarMenu.Children = new ObservableCollection<CustomContextMenuViewModel>();
						}
						globVarMenu.Children.Add(globVarOptions1Menu);
					}
				}

				menuList.Add(globVarMenu);
			}

			if (this.EnvironmentsViewData != null && this.EnvironmentsViewData.Environment != null)
			{
				var env = this.EnvironmentsViewData.Environment;
				if (env.Variables != null)
				{
					var childEnvMenu = new CustomContextMenuViewModel($"{action}: {env.Workspace}-{env.Name}", null, null);
					foreach (var variable in env.Variables)
					{
						if (string.IsNullOrEmpty(variable.Key) == false)
						{
							var envVarOptionsMenu = new CustomContextMenuViewModel(variable.Key, command, env.Name + "|" + variable.Key + "|" + action);

							if (childEnvMenu.Children == null)
							{
								childEnvMenu.Children = new ObservableCollection<CustomContextMenuViewModel>();
							}

							childEnvMenu.Children.Add(envVarOptionsMenu);
						}
					}

					menuList.Add(childEnvMenu);
				}

			}

			return menuList;
		}

		/// <summary>
		/// The BuildVariableMenus
		/// </summary>
		private void BuildVariableMenus()
		{
			var useVariableMenus = this.BuildMenus(Constants.UseAction, Common.AppCommandManager.UseVariableCommand);
			this.UseVariableMenus = new ObservableCollection<CustomContextMenuViewModel>(useVariableMenus);

			var setValueMenus = this.BuildMenus(Constants.SetAction, Common.AppCommandManager.SetVariableValueCommand);
			this.SetMenus = new ObservableCollection<CustomContextMenuViewModel>(setValueMenus);
		}

		/// <summary>
		/// The BuildVariables
		/// </summary>
		/// <param name="global">The <see cref="GlobalVariableModel"/></param>
		private void BuildVariables(GlobalSetupViewModel global)
		{
			List<KeyValueViewModel> variables = new List<KeyValueViewModel>();
			List<CertificateViewModel> certs = new List<CertificateViewModel>();

			EnvironmentViewModel environment = null;
			if (global.allEnvironments != null)
			{
				environment = global.allEnvironments.FirstOrDefault(x => this.SelectedEnvironment != null && x.Name == this.SelectedEnvironment.Name);
			}

			if (environment != null)
			{
				if (environment.Variables != null)
				{
					variables.AddRange(environment.Variables);
				}

				//if (environment.Certificates != null)
				//{
				//    certs.AddRange(environment.Certificates);
				//}
			}

			if (global.Variables != null)
			{
				variables.AddRange(global.Variables.Where(x => variables.Any(y => y.Key.Equals(x.Key, StringComparison.CurrentCultureIgnoreCase)) == false));
			}

			//if (this.Settings.Certificates != null)
			//{
			//	certs.AddRange(this.Settings.Certificates.Select(x => x.ToModel()));
			//}

			this.variables = variables;
			//this.Certificates = this.Settings.Certificates;

			this.BuildVariableMenus();
		}

		/// <summary>
		/// Determines whether CloseRestClient can be executed or not
		/// </summary>
		private bool CanCloseRestClientExecute(RestClientViewModel input)
		{
			return input != null && input.SendButtonText == "Send";
		}

		/// <summary>
		/// Determines whether SaveAsRequest can be executed or not
		/// </summary>
		private bool CanSaveAsRequestExecute(RestClientViewModel input)
		{
			return input != null ? input.HasCompleteRequestData() : false;
		}

		/// <summary>
		/// The CanTest
		/// </summary>
		/// <returns>The <see cref="bool"/></returns>
		private bool CanTest()
		{
			return true;
		}

		/// <summary>
		/// Executes CloseRestClient
		/// </summary>
		private void ExecuteCloseRestClient(RestClientViewModel input)
		{
			this.RestClients.Remove(input);
		}

		/// <summary>
		/// Executes EnvironmentChanged
		/// </summary>
		private void ExecuteEnvironmentChanged(EnvironmentViewModel selection)
		{
			//this.SelectedEnvironment = selection;
			this.BuildVariables(this.GlobalVariables);
			foreach (var item in this.RestClients)
			{
				item.SelectCertificate(this.SelectedEnvironment?.DefaultCertificate ?? this.Settings.DefaultCertificate?.Name);
			}
		}

		/// <summary>
		/// Executes EnvironmentManage
		/// </summary>
		private void ExecuteEnvironmentManage()
		{
			if (this.SelectedEnvironment != null && this.SelectedEnvironment.Name != Constants.Select)
			{
				this.ViewEnvironments(this.SelectedEnvironment.ToModel());
			}
			else
			{
				this.ViewEnvironments(null);
			}
		}

		/// <summary>
		/// Executes NewRestClient
		/// </summary>
		private void ExecuteNewRestClient()
		{
			var blank = this.AddNewRestClient();
			this.SelectedRestClient = blank;
		}

		/// <summary>
		/// Executes SaveAsRequest
		/// </summary>
		private async void ExecuteSaveAsRequest(RestClientViewModel input)
		{
			try
			{
				TransactionViewModel transaction = input.BuildRequest(null);
				transaction.Guid = Guid.NewGuid().ToString();
				await this.SaveRequestClicked(transaction, input);
			}
			catch (Exception ex)
			{
				this.LogException(ex);
			}
		}

		/// <summary>
		/// Executes Test
		/// </summary>
		private void ExecuteTest(string text)
		{
			this.testCommand = null;
			this.BuildVariableMenus();
		}

		/// <summary>
		/// Executes ViewAppSettings
		/// </summary>
		private void ExecuteViewAppSettings()
		{
			this._view.ViewSettingsWindow(this.Settings);
		}

		/// <summary>
		/// Executes ViewEnvironmentSettings
		/// </summary>
		private void ExecuteViewEnvironmentSettings()
		{
			EnvironmentViewModel env = null;
			if (this.SelectedEnvironment.Name != Constants.AddNew && this.SelectedEnvironment.Name != Constants.Select)
			{
				env = this.SelectedEnvironment;
			}

			this.ViewEnvironments(env?.ToModel());
		}

		/// <summary>
		/// Executes ViewTestRunner
		/// </summary>
		private void ExecuteViewTestRunner()
		{
			this._view.OpenTestRunner();
		}

		private void ExecuteWorkspaceChanged(string command)
		{
			var finalEnvs = this.GlobalVariables.allEnvironments.Where(x => x.Workspace == this.SelectedWorkspace).ToList();
			finalEnvs.Insert(0, new EnvironmentViewModel() { Name = Constants.Select });

			var oldEnvGuid = this.SelectedEnvironment?.Guid;
			this.Environments = new ObservableCollection<EnvironmentViewModel>(finalEnvs);
			this.SelectedEnvironment = this.Environments.FirstOrDefault(x => x.Guid == oldEnvGuid) ?? this.Environments.FirstOrDefault();
			this.BuildVariables(this.GlobalVariables);
		}

		/// <summary>
		/// The IsRestClientDataComplete
		/// </summary>
		/// <returns>The <see cref="bool"/></returns>
		private bool IsRestClientDataComplete(RestClientViewModel input)
		{
			return input != null ? input.HasCompleteRequestData() : false;
		}

		/// <summary>
		/// The LoadData
		/// </summary>
		private void LoadData()
		{
			SaveRequest = new SaveRequestViewModel();
			SaveRequest.ParentViewModel = this;
			SaveRequest.LoadData(AppDataHelper.LoadSavedRequestData());

			Settings = AppDataHelper.LoadSettingsData();
			Settings.ParentViewModel = this;

			this.GlobalVariables = GlobalSetupViewModel.Parse(AppDataHelper.LoadGlobalData());

			this.LoadEnvironmentData();
			this.SelectedEnvironment = this.Environments.First();

			History = new HistoryViewModel(_view.HistoryView);
			History.ParentViewModel = this;
			var sessionData = AppDataHelper.LoadSessionHistoryData();
			sessionData = sessionData.Where(x => x.Time > DateTime.Now.AddDays(-1 * Settings.MaxHistoryDays)).ToList();
			var basicData = sessionData != null ? sessionData.Select(x => new BasicHistoryViewModel(x)) : null;
			History.LoadData(basicData != null ? basicData : null);

			var appStatus = AppDataHelper.LoadAppState();
			this.RestClients = new ObservableCollection<RestClientViewModel>(appStatus.RestClients ?? new ObservableCollection<RestClientViewModel>());
			if (!string.IsNullOrEmpty(appStatus.SelectedRestClient))
			{
				this.SelectedRestClient = this.RestClients.FirstOrDefault(x => x.GUID == appStatus.SelectedRestClient);
			}

			if (this.RestClients.Count == 0)
			{
				var blank = this.AddNewRestClient();
				this.SelectedRestClient = blank;
			}
			else
			{
				foreach (var rc in this.RestClients)
				{
					rc.ParentViewModel = this;
				}
			}

			if (!string.IsNullOrEmpty(appStatus.SelectedWorkspace))
			{
				this.SelectedWorkspace = this.GlobalVariables.Workspaces.FirstOrDefault(x => x == appStatus.SelectedWorkspace);
				var finalEnvs = this.GlobalVariables.allEnvironments.Where(x => x.Workspace == this.SelectedWorkspace).ToList();
				finalEnvs.Insert(0, new EnvironmentViewModel() { Name = Constants.Select });

				this.Environments = new ObservableCollection<EnvironmentViewModel>(finalEnvs);
			}

			if (!string.IsNullOrEmpty(appStatus.SelectedEnvironment))
			{
				this.SelectedEnvironment = this.Environments.FirstOrDefault(x => x.Name == appStatus.SelectedEnvironment);
				if (this.SelectedEnvironment != null)
				{
					ExecuteEnvironmentChanged(this.SelectedEnvironment);
				}
			}
		}

		/// <summary>
		/// The LoadEnvironmentData
		/// </summary>
		private void LoadEnvironmentData()
		{
			this.LoadFromGlobalData();
		}

		/// <summary>
		/// The LoadFromGlobalData
		/// </summary>
		private void LoadFromGlobalData()
		{
			//List<EnvironmentViewModel> envs = new List<EnvironmentViewModel>();

			//envs.Add(new EnvironmentViewModel() { Guid = Guid.NewGuid().ToString(), Name = Constants.Select });

			//if (GlobalVariables.Environments != null)
			//{
			//	foreach (var env in this.GlobalVariables.Environments)
			//	{
			//		envs.Add(EnvironmentViewModel.Parse(env));
			//	}
			//}

			var oldSelectedEnv = this.SelectedEnvironment?.Guid;
			if (this.GlobalVariables.Workspaces != null)
			{
				var oldSelectedWorkspace = this.SelectedWorkspace;
				if (oldSelectedWorkspace != null)
				{
					this.SelectedWorkspace = this.GlobalVariables.Workspaces.FirstOrDefault(x => x == oldSelectedWorkspace);
				}
				else
				{
					this.SelectedWorkspace = this.GlobalVariables.Workspaces.FirstOrDefault();
				}
			}

			//this.allEnvironments = envs;
			var finalEnvs = this.GlobalVariables.allEnvironments.Where(x => x.Workspace == this.SelectedWorkspace).ToList();
			finalEnvs.Insert(0, new EnvironmentViewModel() { Name = Constants.Select });

			this.Environments = new ObservableCollection<EnvironmentViewModel>(finalEnvs);
			if (oldSelectedEnv != null)
			{
				this.SelectedEnvironment = this.Environments.FirstOrDefault(x => x.Guid == oldSelectedEnv);
				if (this.SelectedEnvironment == null)
				{
					this.SelectedEnvironment = this.Environments.FirstOrDefault();
				}
			}

			this.BuildVariables(this.GlobalVariables);
		}

		private void MasterEventHandler_AppClosing()
		{
			var appStatus = new AppStatus()
			{
				RestClients = RestClients,
				SelectedRestClient = this.SelectedRestClient?.GUID,
				SelectedWorkspace = this.SelectedWorkspace,
				SelectedEnvironment = this.SelectedEnvironment?.Name
			};

			AppDataHelper.SaveAppState(appStatus);
		}

		/// <summary>
		/// The MasterEventHandler_ReloadSavedRequest
		/// </summary>
		private void MasterEventHandler_ReloadSavedRequest()
		{
			SaveRequest.LoadData(AppDataHelper.LoadSavedRequestData());
		}

		/// <summary>
		/// The MasterEventHandler_RemoveCategory
		/// </summary>
		/// <param name="ctg">The <see cref="CategoryViewModel"/></param>
		private void MasterEventHandler_RemoveCategory(CategoryViewModel ctg)
		{

			if (SaveRequest.Data.Categories != null)
			{
				var currCat = SaveRequest.Data.Categories.FirstOrDefault(x => x.Name == ctg.Name);
				if (currCat != null)
				{
					SaveRequest.Data.Categories.Remove(currCat);
				}
			}
			AppDataHelper.SaveRequestData(SaveRequest.Data);
			SaveRequest.Data = AppDataHelper.LoadSavedRequestData();
		}

		/// <summary>
		/// The MasterEventHandler_RemoveSession
		/// </summary>
		/// <param name="session">The <see cref="SessionHistoryViewModel"/></param>
		private void MasterEventHandler_RemoveSession(SessionHistoryViewModel session)
		{
			AppDataHelper.RemoveSession(session);
			this.History.RemoveSession(session);
		}

		/// <summary>
		/// The MasterEventHandler_SaveSession
		/// </summary>
		/// <param name="session">The <see cref="TransactionViewModel"/></param>
		private void MasterEventHandler_SaveSession(TransactionViewModel session)
		{
			List<SessionHistoryViewModel> sessionsVM = AppDataHelper.LoadSessionHistoryData();
			try
			{
				int index = sessionsVM.FindIndex(x => x.Guid == session.Guid);
				var transaction = new SessionHistoryViewModel(session);
				if (index > -1 && sessionsVM.Count > index)
				{
					sessionsVM[index] = transaction;
				}
				else
				{
					sessionsVM.Insert(0, transaction);
				}
				AppDataHelper.SaveSessionHistoryData(sessionsVM);

				this.History.AddSession(session);
			}
			catch (Exception ex)
			{
				ErrorLog.LogException(ex);
			}
		}

		/// <summary>
		/// The SendEmail
		/// </summary>
		private void SendEmail(RestClientViewModel restClient)
		{
			string body = restClient.GetRequestResponseText(this.SelectedRestClient.BuildRequestWithResponse());
			this.SendEmail(body);
		}

		/// <summary>
		/// The SendEmail
		/// </summary>
		/// <param name="body">The <see cref="string"/></param>
		/// <param name="attachmentPath">The <see cref="string"/></param>
		private void SendEmail(string body, string attachmentPath = null)
		{
			try
			{
				Task.Run(() =>
				{
					//Outlook.Application outlookApp = new Outlook.Application();
					//Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

					//mailItem.Subject = "Rest Client";

					//if (attachmentPath != null && File.Exists(attachmentPath))
					//{
					//    mailItem.Attachments.Add(attachmentPath);
					//}

					//mailItem.Body = body;
					//mailItem.Display(false);
				});
			}
			catch (Exception ex)
			{
				this.LogException(ex);
			}
		}

		/// <summary>
		/// The SetViewEnvData
		/// </summary>
		private void SetViewEnvData()
		{
			this.EnvironmentsViewData = new ViewEnvironmentsViewModel();
			this.EnvironmentsViewData.Variables = this.GlobalVariables?.Variables?.ToList();
			if (this.SelectedEnvironment != null)
			{
				this.EnvironmentsViewData.Environment = this.GlobalVariables?.Environments?.FirstOrDefault(x => x.Guid == this.SelectedEnvironment.Guid);
			}
		}

		/// <summary>
		/// The TabChanged
		/// </summary>
		private void TabChanged()
		{
			try
			{
				if (SelectedMainTab == (int)Constants.MainTab.History)
				{
					this.History.FilteredSessionData();
				}
			}
			catch (Exception ex)
			{
				this.LogException(ex);
			}
		}

		/// <summary>
		/// The ViewEnvironments
		/// </summary>
		/// <param name="env">The <see cref="EnvironmentModel"/></param>
		private void ViewEnvironments(EnvironmentModel env)
		{
			var newEnvironment = _view.ViewEnvironmentWindow(this.GlobalVariables, env, this.SelectedWorkspace);
			this.LoadEnvironmentData();

			if (newEnvironment != null)
			{
				this.SelectedEnvironment = this.Environments.FirstOrDefault(x => x.Guid == newEnvironment.Guid);
			}
			else if (this.SelectedEnvironment != null)
			{
				this.SelectedEnvironment = this.Environments.FirstOrDefault(x => x.Guid == this.SelectedEnvironment.Guid);
			}
			else
			{
				this.SelectedEnvironment = this.Environments.FirstOrDefault();
			}

			this.BuildVariables(this.GlobalVariables);
		}
	}
}
