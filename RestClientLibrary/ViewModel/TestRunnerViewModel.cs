// <copyright file="TestRunnerViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>06-08-2017</date>

namespace RestClientLibrary.ViewModel
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Threading.Tasks;
	using DataLibrary;
	using Unity;
	using RestClientLibrary.Common;
	using RestClientLibrary.Model;

	/// <summary>
	/// Defines the <see cref="TestRunnerViewModel" />
	/// </summary>
	public class TestRunnerViewModel : BaseViewModel
	{
		#region Fields

		/// <summary>
		/// The categories field
		/// </summary>
		private List<CategoryViewModel> categories;

		/// <summary>
		/// The environments field
		/// </summary>
		private List<EnvironmentViewModel> environments;

		/// <summary>
		/// Defines the 
		/// </summary>
		private GlobalSetupViewModel globalData;

		/// <summary>
		/// Defines the 
		/// </summary>
		private IResolver resolver;

		/// <summary>
		/// The selectedCategory field
		/// </summary>
		private CategoryViewModel selectedCategory;

		/// <summary>
		/// The selectedEnvironment field
		/// </summary>
		private EnvironmentViewModel selectedEnvironment;

		/// <summary>
		/// The selectedTestRun field
		/// </summary>
		private TestRunViewModel selectedTestRun;

		/// <summary>
		/// The showNewTestRun field
		/// </summary>
		private bool showNewTestRun;

		/// <summary>
		/// The testRuns field
		/// </summary>
		private ObservableCollection<TestRunViewModel> testRuns;

		#region Commands

		/// <summary>
		/// The deleteTestRunCommand field
		/// </summary>
		private RelayCommand<TestRunViewModel> deleteTestRunCommand;

		/// <summary>
		/// The executeTestCommand field
		/// </summary>
		private RelayCommand executeTestCommand;

		/// <summary>
		/// The hideRunCommand field
		/// </summary>
		private RelayCommand hideRunCommand;

		/// <summary>
		/// The newTestRunCommand field
		/// </summary>
		private RelayCommand newTestRunCommand;

		/// <summary>
		/// The reRunCommand field
		/// </summary>
		private RelayCommand<TestRunViewModel> reRunCommand;

		/// <summary>
		/// The testRunSelectionChangedCommand field
		/// </summary>
		private RelayCommand testRunSelectionChangedCommand;

		#endregion

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TestRunnerViewModel"/> class.
		/// </summary>
		public TestRunnerViewModel()
		{
			this.LoadData();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the Categories
		/// </summary>
		public List<CategoryViewModel> Categories
		{
			get
			{
				return this.categories;
			}

			set
			{
				this.categories = value;
				this.OnPropertyChanged("Categories");
			}
		}

		/// <summary>
		/// Gets or sets the Environments
		/// </summary>
		public List<EnvironmentViewModel> Environments
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
		/// Sets the Resolver
		/// </summary>
		[Dependency]
		public IResolver Resolver
		{
			set
			{
				this.resolver = value;
			}
		}

		/// <summary>
		/// Gets or sets the SelectedCategory
		/// </summary>
		public CategoryViewModel SelectedCategory
		{
			get
			{
				return this.selectedCategory;
			}

			set
			{
				this.selectedCategory = value;
				this.OnPropertyChanged("SelectedCategory");
			}
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
				this.selectedEnvironment = value;
				this.OnPropertyChanged("SelectedEnvironment");
			}
		}

		/// <summary>
		/// Gets or sets the SelectedTestRun
		/// </summary>
		public TestRunViewModel SelectedTestRun
		{
			get
			{
				return this.selectedTestRun;
			}

			set
			{
				this.selectedTestRun = value;
				this.OnPropertyChanged("SelectedTestRun");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether ShowNewTestRun
		/// </summary>
		public bool ShowNewTestRun
		{
			get
			{
				return this.showNewTestRun;
			}

			set
			{
				this.showNewTestRun = value;
				this.OnPropertyChanged("ShowNewTestRun");
			}
		}

		/// <summary>
		/// Gets or sets the TestRuns
		/// </summary>
		public ObservableCollection<TestRunViewModel> TestRuns
		{
			get
			{
				return this.testRuns;
			}

			set
			{
				this.testRuns = value;
				this.OnPropertyChanged("TestRuns");
			}
		}

		#region Commands

		/// <summary>
		/// Gets the DeleteTestRunCommand
		/// </summary>
		public RelayCommand<TestRunViewModel> DeleteTestRunCommand
		{
			get
			{
				if (this.deleteTestRunCommand == null)
				{
					this.deleteTestRunCommand = new RelayCommand<TestRunViewModel>(command => this.ExecuteDeleteTestRun(command));
				}

				return this.deleteTestRunCommand;
			}
		}

		/// <summary>
		/// Gets the ExecuteTestCommand
		/// </summary>
		public RelayCommand ExecuteTestCommand
		{
			get
			{
				if (this.executeTestCommand == null)
				{
					this.executeTestCommand = new RelayCommand(command => this.ExecuteExecuteTest(), can => this.CanExecuteTestExecute());
				}

				return this.executeTestCommand;
			}
		}

		/// <summary>
		/// Gets the HideRunCommand
		/// </summary>
		public RelayCommand HideRunCommand
		{
			get
			{
				if (this.hideRunCommand == null)
				{
					this.hideRunCommand = new RelayCommand(command => this.ExecuteHideRun(), can => this.CanHideRunExecute());
				}

				return this.hideRunCommand;
			}
		}

		/// <summary>
		/// Gets the NewTestRunCommand
		/// </summary>
		public RelayCommand NewTestRunCommand
		{
			get
			{
				if (this.newTestRunCommand == null)
				{
					this.newTestRunCommand = new RelayCommand(command => this.ExecuteNewTestRun(), can => this.CanNewTestRunExecute());
				}

				return this.newTestRunCommand;
			}
		}

		/// <summary>
		/// Gets the ReRunCommand
		/// </summary>
		public RelayCommand<TestRunViewModel> ReRunCommand
		{
			get
			{
				if (this.reRunCommand == null)
				{
					this.reRunCommand = new RelayCommand<TestRunViewModel>(command => this.ExecuteReRun(command), can => this.CanExecuteReRun(can));
				}

				return this.reRunCommand;
			}
		}

		/// <summary>
		/// Gets the TestRunSelectionChangedCommand
		/// </summary>
		public RelayCommand TestRunSelectionChangedCommand
		{
			get
			{
				if (this.testRunSelectionChangedCommand == null)
				{
					this.testRunSelectionChangedCommand = new RelayCommand(command => this.ExecuteTestRunSelectionChanged());
				}

				return this.testRunSelectionChangedCommand;
			}
		}

		#endregion

		#endregion

		#region Methods

		#region Private Methods

		/// <summary>
		/// The CanExecuteReRun
		/// </summary>
		/// <param name="input">The <see cref="TestRunViewModel"/></param>
		/// <returns>The <see cref="bool"/></returns>
		private bool CanExecuteReRun(TestRunViewModel input)
		{
			var category = this.Categories.FirstOrDefault(x => x.Guid == input.CollectionId);
			var environment = this.Environments.FirstOrDefault(x => x.Guid == input.EnvironmentId);

			if (category != null && environment != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Determines whether ExecuteTest can be executed or not
		/// </summary>
		private bool CanExecuteTestExecute()
		{
			return this.SelectedCategory != null && this.SelectedEnvironment != null && this.SelectedEnvironment.Name != Constants.Select;
		}

		/// <summary>
		/// Determines whether HideRun can be executed or not
		/// </summary>
		private bool CanHideRunExecute()
		{
			return this.ShowNewTestRun;
		}

		/// <summary>
		/// Determines whether NewTestRun can be executed or not
		/// </summary>
		private bool CanNewTestRunExecute()
		{
			return this.ShowNewTestRun == false;
		}

		/// <summary>
		/// Executes DeleteTestRun
		/// </summary>
		private void ExecuteDeleteTestRun(TestRunViewModel input)
		{
			this.TestRuns.Remove(input);
			AppDataHelper.RemoveTestRun(input);
		}

		/// <summary>
		/// Executes ExecuteTest
		/// </summary>
		private void ExecuteExecuteTest()
		{
			var category = this.SelectedCategory;
			var newTestRun = this.resolver.Resolve<TestRunViewModel>();
			newTestRun.BeginRun(this.globalData, this.SelectedCategory, this.SelectedEnvironment.Name, this.SelectedEnvironment.Guid);

			if (this.TestRuns == null)
			{
				this.TestRuns = new ObservableCollection<TestRunViewModel>();
			}

			this.TestRuns.Insert(0, newTestRun);
			this.SelectedTestRun = newTestRun;
			this.ShowNewTestRun = false;
		}

		/// <summary>
		/// Executes HideRun
		/// </summary>
		private void ExecuteHideRun()
		{
			this.ShowNewTestRun = false;
		}

		/// <summary>
		/// Executes NewTestRun
		/// </summary>
		private void ExecuteNewTestRun()
		{
			this.ShowNewTestRun = true;
		}

		/// <summary>
		/// Executes ReRun
		/// </summary>
		private void ExecuteReRun(TestRunViewModel input)
		{
			var category = this.Categories.FirstOrDefault(x => x.Guid == input.CollectionId);
			var environment = this.Environments.FirstOrDefault(x => x.Guid == input.EnvironmentId);

			if (category != null && environment != null)
			{
				var newTestRun = this.resolver.Resolve<TestRunViewModel>();
				newTestRun.BeginRun(this.globalData, category, environment.Name, environment.Guid);

				if (this.TestRuns == null)
				{
					this.TestRuns = new ObservableCollection<TestRunViewModel>();
				}

				this.TestRuns.Insert(0, newTestRun);
				this.SelectedTestRun = newTestRun;
				this.ShowNewTestRun = false;
			}
		}

		/// <summary>
		/// Executes TestRunSelectionChanged
		/// </summary>
		private void ExecuteTestRunSelectionChanged()
		{
			this.ShowNewTestRun = false;
		}

		/// <summary>
		/// The GetHeaders
		/// </summary>
		/// <param name="inputHeaders">The <see cref="string"/></param>
		/// <returns>The <see cref="List{KeyValueViewModel}"/></returns>
		private List<KeyValueViewModel> GetHeaders(string inputHeaders)
		{
			List<KeyValueViewModel> output = new List<KeyValueViewModel>();
			string headerText = inputHeaders.GetString().Trim();
			if (headerText != null)
			{
				string[] multiheaders = headerText.Replace(Environment.NewLine, "\n").Split('\n');
				foreach (string header in multiheaders)
				{
					if (header.GetString().Trim() != string.Empty)
					{
						int pos = header.IndexOf(':');
						if (pos > -1)
						{
							KeyValueViewModel obj = new KeyValueViewModel()
							{
								Key = header.Substring(0, pos).GetString().Trim(),
								Value = header.Substring(pos + 1, header.Length - pos - 1).GetString().Trim()
							};
							output.Add(obj);
						}
						else
						{
							KeyValueViewModel obj = new KeyValueViewModel()
							{
								Key = header.GetString()
							};
							output.Add(obj);
						}
					}
				}
			}

			return output;
		}

		/// <summary>
		/// The deleteTestRunsCommand field
		/// </summary>
		private RelayCommand deleteTestRunsCommand;

		/// <summary>
		/// Gets the DeleteTestRunsCommand
		/// </summary>
		public RelayCommand DeleteTestRunsCommand
		{
			get
			{
				if (this.deleteTestRunsCommand == null)
				{
					this.deleteTestRunsCommand = new RelayCommand(command => this.ExecuteDeleteTestRuns(), can => this.CanDeleteTestRunsExecute());
				}

				return this.deleteTestRunsCommand;
			}
		}

		/// <summary>
		/// Executes DeleteTestRuns
		/// </summary>
		private void ExecuteDeleteTestRuns()
		{
			bool confirm = WindowServiceHandler.RaiseConfirmationBoxEvent("Delete Test Run(s)", "Are you sure you want to delete these test run(s)?");
			if (confirm)
			{
				this.TestRuns = new ObservableCollection<TestRunViewModel>(this.TestRuns.Where(x => x.IsSelected == false));
				AppDataHelper.SaveTestRunsData(this.TestRuns.ToList());
			}
		}

		/// <summary>
		/// Determines whether DeleteTestRuns can be executed or not
		/// </summary>
		private bool CanDeleteTestRunsExecute()
		{
			return this.TestRuns != null && this.TestRuns.Any(x => x.IsSelected);
		}

		/// <summary>
		/// The LoadData
		/// </summary>
		private void LoadData()
		{
			Task.Run(() =>
			{
				this.LoadEnvironmentData();
				var savedData = AppDataHelper.LoadSavedRequestData();
				this.Categories = savedData.Categories?.ToList();
				this.TestRuns = new ObservableCollection<TestRunViewModel>(AppDataHelper.LoadTestRunsData());
				foreach (var run in this.TestRuns)
				{
					var env = this.Environments?.FirstOrDefault(x => x.Guid != null && x.Guid == run.EnvironmentId);
					if (env == null)
					{
						env = this.Environments?.FirstOrDefault(x => x.Name == run.Environment);
					}

					if (env != null)
					{
						run.Environment = env.Name;
						run.EnvironmentId = env.Guid;
					}
				}
			});
		}

		/// <summary>
		/// The LoadEnvironmentData
		/// </summary>
		private void LoadEnvironmentData()
		{
			this.globalData = GlobalSetupViewModel.Parse(this.globalData, AppDataHelper.LoadGlobalData());

			List<EnvironmentViewModel> envs = new List<EnvironmentViewModel>();

			envs.Add(new EnvironmentViewModel() { Name = Constants.Select });

			if (globalData.Environments != null)
			{
				foreach (var env in this.globalData.Environments)
				{
					envs.Add(env);
				}
			}

			this.Environments = envs;
			this.SelectedEnvironment = envs.First();
		}

		#endregion

		#endregion
	}
}
