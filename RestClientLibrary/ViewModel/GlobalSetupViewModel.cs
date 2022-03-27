// <copyright file="GlobalSetupViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>22-08-2017</date>

namespace RestClientLibrary.ViewModel
{
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
        private readonly IGlobalSetupView view;

        /// <summary>
        /// The certificates field
        /// </summary>
        private ObservableCollection<CertificateViewModel> certificates;

        /// <summary>
        /// The certificateSupport field
        /// </summary>
        private bool certificateSupport;

        /// <summary>
        /// The environments field
        /// </summary>
        private ObservableCollection<EnvironmentViewModel> environments;

        /// <summary>
        /// The selectedEnvironment field
        /// </summary>
        private EnvironmentViewModel selectedEnvironment;

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
        private RelayCommand cloneEnvironmentCommand;

        /// <summary>
        /// The deleteEnvironmentCommand field
        /// </summary>
        private RelayCommand deleteEnvironmentCommand;

        /// <summary>
        /// The environmentChangedCommand field
        /// </summary>
        private RelayCommand environmentChangedCommand;

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

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref = "GlobalSetupViewModel"/> class.
        /// </summary>
        /// <param name = "view">The <see cref = "IGlobalSetupView"/></param>
        public GlobalSetupViewModel(IGlobalSetupView view)
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
        /// Gets the CloneEnvironmentCommand
        /// </summary>
        public RelayCommand CloneEnvironmentCommand
        {
            get
            {
                if (this.cloneEnvironmentCommand == null)
                {
                    this.cloneEnvironmentCommand = new RelayCommand(command => this.ExecuteCloneEnvironment(), can => CanCloneEnvironmentExecuted());
                }

                return this.cloneEnvironmentCommand;
            }
        }

        /// <summary>
        /// Gets the DeleteEnvironmentCommand
        /// </summary>
        public RelayCommand DeleteEnvironmentCommand
        {
            get
            {
                if (this.deleteEnvironmentCommand == null)
                {
                    this.deleteEnvironmentCommand = new RelayCommand(command => this.ExecuteDeleteEnvironment(), can => this.CanDeleteEnvironmentExecute());
                }

                return this.deleteEnvironmentCommand;
            }
        }

        /// <summary>
        /// Gets the EnvironmentChangedCommand
        /// </summary>
        public RelayCommand EnvironmentChangedCommand
        {
            get
            {
                if (this.environmentChangedCommand == null)
                {
                    this.environmentChangedCommand = new RelayCommand(command => this.ExecuteEnvironmentChanged());
                }

                return this.environmentChangedCommand;
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
        public void LoadData(GlobalVariableModel globalData, EnvironmentModel selectedEnvironment)
        {
            var globalVariables = globalData ?? AppDataHelper.LoadGlobalData();
            var settings = AppDataHelper.LoadSettingsData();
            this.CertificateSupport = settings.CertificateSupport;

            if (globalVariables.Variables != null)
            {
                this.Variables = new ObservableCollection<KeyValueViewModel>(globalVariables.Variables.Select(x => KeyValueViewModel.Parse(x)));
            }

            if (globalVariables.Certificates != null)
            {
                this.Certificates = new ObservableCollection<CertificateViewModel>(globalVariables.Certificates.Select(x => CertificateViewModel.Parse(x)));
            }

            List<EnvironmentViewModel> envs = new List<EnvironmentViewModel>();
            envs.Add(new EnvironmentViewModel()
            { Name = Constants.Select });
            if (globalVariables.Environments != null)
            {
                foreach (var env in globalVariables.Environments)
                {
                    envs.Add(EnvironmentViewModel.Parse(env));
                }
            }

            envs.Add(new EnvironmentViewModel()
            { Name = Constants.AddNew });
            this.Environments = new ObservableCollection<EnvironmentViewModel>(envs);
            if (selectedEnvironment != null)
            {
                this.SelectedEnvironment = this.Environments.FirstOrDefault(x => x.Name == selectedEnvironment.Name);
            }
            else
            {
                this.SelectedEnvironment = this.Environments.First();
            }
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
            var environment = this.view.AddNewEnvironment(input);
            if (environment != null)
            {
                this.Environments.Insert(this.Environments.Count - 1, environment);
                this.SelectedEnvironment = environment;
            }
            else
            {
                this.SelectedEnvironment = this.Environments.First();
            }
        }

        /// <summary>
        /// The CanCloneEnvironmentExecuted
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        private bool CanCloneEnvironmentExecuted()
        {
            return this.SelectedEnvironment != null && this.SelectedEnvironment.Name != Constants.AddNew && this.SelectedEnvironment.Name != Constants.Select;
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
        private void ExecuteCloneEnvironment()
        {
            this.AddNewEnvironment(this.GetClonedObject(this.SelectedEnvironment));
        }

        /// <summary>
        /// Executes DeleteEnvironment
        /// </summary>
        private void ExecuteDeleteEnvironment()
        {
            this.Environments.Remove(this.SelectedEnvironment);
            this.SelectedEnvironment = this.Environments.FirstOrDefault();
        }

        /// <summary>
        /// Executes EnvironmentChanged
        /// </summary>
        private void ExecuteEnvironmentChanged()
        {
            if (this.SelectedEnvironment != null && this.SelectedEnvironment.Name == Constants.AddNew)
            {
                this.AddNewEnvironment(null);
            }
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
                Variables = input.Variables ?? new ObservableCollection<KeyValueViewModel>(input.Variables),
            };
        }

        /// <summary>
        /// The ToModel
        /// </summary>
        /// <returns>The <see cref = "GlobalVariableModel"/></returns>
        private GlobalVariableModel ToModel()
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
                var environmentsToSave = this.Environments.Where(x => x.Name != Constants.AddNew && x.Name != Constants.Select);
                output.Environments = environmentsToSave.Select(x => x.ToModel()).ToList();
            }

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

        #endregion

        #endregion
    }
}
