// <copyright file="EnvironmentViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>12-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using DataLibrary;
    using RestClientLibrary.Model;
    using RestClientLibrary.View;

    /// <summary>
    /// Defines the <see cref = "EnvironmentViewModel"/>
    /// </summary>
    public class EnvironmentViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// The certificates field
        /// </summary>
        private ObservableCollection<CertificateViewModel> certificates;

        /// <summary>
        /// The certificateSupport field
        /// </summary>
        private bool certificateSupport;

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

        /// <summary>
        /// Defines the view
        /// </summary>
        private IEnvironmentView view;

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
            if (input.Variables != null)
            {
                output.Variables = new ObservableCollection<KeyValueViewModel>(input.Variables.Select(x => KeyValueViewModel.Parse(x)));
            }

            if (input.Certificates != null)
            {
                output.Certificates = new ObservableCollection<CertificateViewModel>(input.Certificates.Select(x => CertificateViewModel.Parse(x)));
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
            this.CheckForCertificateSupport();
        }

        /// <summary>
        /// The IsValid
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool IsValid()
        {
            return (string.IsNullOrEmpty(this.Name) == false);
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
                Certificates = this.Certificates?.Select(x => x.ToModel()).ToList(),
                Variables = this.Variables?.Select(x => x.ToModel()).ToList()
            };
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The CheckForCertificateSupport
        /// </summary>
        private void CheckForCertificateSupport()
        {
            var settings = RestClientLibrary.Common.AppDataHelper.LoadSettingsData();
            this.CertificateSupport = settings.CertificateSupport;
        }

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
