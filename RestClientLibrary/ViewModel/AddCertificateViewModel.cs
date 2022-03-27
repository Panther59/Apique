﻿// <copyright file="AddCertificateViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>05-09-2017</date>

namespace RestClientLibrary.ViewModel
{
    using DataLibrary;
    using RestClientLibrary.Common;
    using RestClientLibrary.View;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Defines the <see cref="AddCertificateViewModel" />
    /// </summary>
    public class AddCertificateViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// Defines the _certificateName
        /// </summary>
        private string _certificateName;

        /// <summary>
        /// Defines the _password
        /// </summary>
        private string _password;

        /// <summary>
        /// Defines the _path
        /// </summary>
        private string _path;

        /// <summary>
        /// The certificates field
        /// </summary>
        private List<X509Certificate2> certificates;

        /// <summary>
        /// The searchBy field
        /// </summary>
        private SearchCertificateBy searchBy;

        /// <summary>
        /// The searchText field
        /// </summary>
        private string searchText;

        /// <summary>
        /// The selectedCertificate field
        /// </summary>
        private X509Certificate2 selectedCertificate;

        /// <summary>
        /// Defines the view
        /// </summary>
        private IAddCertificateView view;

        #region Commands

        /// <summary>
        /// The closeWindowCommand field
        /// </summary>
        private RelayCommand closeWindowCommand;

        /// <summary>
        /// The saveCertificateCommand field
        /// </summary>
        private RelayCommand saveCertificateCommand;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCertificateViewModel"/> class.
        /// </summary>
        /// <param name="view">The <see cref="IAddCertificateView"/></param>
        public AddCertificateViewModel(IAddCertificateView view)
        {
            this.view = view;
            this.LoadData();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the CertificateName
        /// </summary>
        public string CertificateName
        {
            get { return _certificateName; }
            set
            {
                _certificateName = value;
                OnPropertyChanged("CertificateName");
            }
        }

        /// <summary>
        /// Gets or sets the Certificates
        /// </summary>
        public List<X509Certificate2> Certificates
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
        /// Gets or sets the FinalCertificate
        /// </summary>
        public CertificateViewModel FinalCertificate { get; private set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        /// <summary>
        /// Gets or sets the Path
        /// </summary>
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }

        /// <summary>
        /// Gets or sets the SearchBy
        /// </summary>
        public SearchCertificateBy SearchBy
        {
            get
            {
                return this.searchBy;
            }

            set
            {
                this.searchBy = value;
                this.OnPropertyChanged("SearchBy");
            }
        }

        /// <summary>
        /// Gets or sets the SearchText
        /// </summary>
        public string SearchText
        {
            get
            {
                return this.searchText;
            }

            set
            {
                this.searchText = value;
                this.OnPropertyChanged("SearchText");
            }
        }

        /// <summary>
        /// Gets or sets the SelectedCertificate
        /// </summary>
        public X509Certificate2 SelectedCertificate
        {
            get
            {
                return this.selectedCertificate;
            }

            set
            {
                this.selectedCertificate = value;
                this.OnPropertyChanged("SelectedCertificate");
            }
        }

        #region Commands

        /// <summary>
        /// Gets the CloseWindowCommand
        /// </summary>
        public RelayCommand CloseWindowCommand
        {
            get
            {
                if (this.closeWindowCommand == null)
                {
                    this.closeWindowCommand = new RelayCommand(command => this.ExecuteCloseWindow());
                }

                return this.closeWindowCommand;
            }
        }

        /// <summary>
        /// Gets the SaveCertificateCommand
        /// </summary>
        public RelayCommand SaveCertificateCommand
        {
            get
            {
                if (this.saveCertificateCommand == null)
                {
                    this.saveCertificateCommand = new RelayCommand(command => this.ExecuteSaveCertificate(), can => this.CanSaveCertificateExecute());
                }

                return this.saveCertificateCommand;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The CloseWindow
        /// </summary>
        public void CloseWindow()
        {
            this.view.CloseParentWindow();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Determines whether SaveCertificate can be executed or not
        /// </summary>
        private bool CanSaveCertificateExecute()
        {
            return this.SelectedCertificate != null && string.IsNullOrWhiteSpace(this.CertificateName) == false;
        }

        /// <summary>
        /// Executes CloseWindow
        /// </summary>
        private void ExecuteCloseWindow()
        {
            this.CloseWindow();
        }

        /// <summary>
        /// Executes SaveCertificate
        /// </summary>
        private void ExecuteSaveCertificate()
        {
            this.FinalCertificate = CertificateViewModel.Parse(this.SelectedCertificate, this.CertificateName);
            this.CloseWindow();
        }

        /// <summary>
        /// The LoadData
        /// </summary>
        private void LoadData()
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            List<X509Certificate2> certs = new List<X509Certificate2>();
            foreach (X509Certificate2 mCert in store.Certificates)
            {
                certs.Add(mCert);
            }

            this.Certificates = certs;
        }

        #endregion

        #endregion
    }
}
