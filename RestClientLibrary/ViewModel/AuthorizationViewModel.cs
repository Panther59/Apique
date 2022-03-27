namespace RestClientLibrary.ViewModel
{
    using System.Collections.Generic;
    using System.Linq;
    using DataLibrary;
    using RestClientLibrary.ViewModel.Authorization;

    /// <summary>
    /// Defines the <see cref = "AuthorizationViewModel"/>
    /// </summary>
    public class AuthorizationViewModel : BaseViewModel
    {
        /// <summary>
        /// The authorizationTypeChangedCommand field
        /// </summary>
        private RelayCommand authorizationTypeChangedCommand;

        /// <summary>
        /// The authorizationTypes field
        /// </summary>
        private List<BaseAuthorizationViewModel> authorizationTypes;

        /// <summary>
        /// The selectedAuthorizationType field
        /// </summary>
        private BaseAuthorizationViewModel selectedAuthorizationType;

        /// <summary>
        /// Gets the AuthorizationTypeChangedCommand
        /// </summary>
        public RelayCommand AuthorizationTypeChangedCommand
        {
            get
            {
                if (this.authorizationTypeChangedCommand == null)
                {
                    this.authorizationTypeChangedCommand = new RelayCommand(command => this.ExecuteAuthorizationTypeChanged());
                }

                return this.authorizationTypeChangedCommand;
            }
        }

        /// <summary>
        /// Gets or sets the AuthorizationTypes
        /// </summary>
        public List<BaseAuthorizationViewModel> AuthorizationTypes
        {
            get
            {
                return this.authorizationTypes;
            }

            set
            {
                this.authorizationTypes = value;
                this.OnPropertyChanged("AuthorizationTypes");
            }
        }

        /// <summary>
        /// The authView field
        /// </summary>
        private BaseAuthorizationViewModel authView;

        /// <summary>
        /// Gets or sets the AuthView
        /// </summary>
        public BaseAuthorizationViewModel AuthView
        {
            get
            {
                return this.authView;
            }

            set
            {
                this.authView = value;
                this.OnPropertyChanged("AuthView");
            }
        }


        /// <summary>
        /// Gets or sets the SelectedAuthorizationType
        /// </summary>
        public BaseAuthorizationViewModel SelectedAuthorizationType
        {
            get
            {
                return this.selectedAuthorizationType;
            }

            set
            {
                this.selectedAuthorizationType = value;
                this.OnPropertyChanged("SelectedAuthorizationType");
            }
        }

        /// <summary>
        /// The GetAuthorizationHeader
        /// </summary>
        /// <returns>The <see cref="KeyValueViewModel"/></returns>
        public KeyValueViewModel GetAuthorizationHeader()
        {
            return this.AuthView?.GetAuthorizationHeader();
        }

        /// <summary>
        /// The LoadAuthorizationData
        /// </summary>
        /// <param name="keyValueViewModel">The <see cref="KeyValueViewModel"/></param>
        public void LoadAuthorizationData(KeyValueViewModel keyValueViewModel)
        {
            this.SelectedAuthorizationType = this.AuthorizationTypes.First();
            foreach (var authType in this.AuthorizationTypes)
            {
                if (authType.IsValidAuthorization(keyValueViewModel))
                {
                    this.SelectedAuthorizationType = authType;
                    this.SelectedAuthorizationType.SetAuthorizationHeader(keyValueViewModel);
                    break;
                }
            }

            this.AuthView = this.SelectedAuthorizationType;
        }

        /// <summary>
        /// The LoadData
        /// </summary>
        public void LoadData()
        {
            var types = new List<BaseAuthorizationViewModel>();
            types.Add(new NoAuthorizationViewModel()
            { Name = "No Auth" });
            types.Add(new BasicAuthorizationViewModel()
            { Name = "Basic" });
            this.AuthorizationTypes = types;
            this.SelectedAuthorizationType = this.AuthorizationTypes.First();
        }

        /// <summary>
        /// Executes AuthorizationTypeChanged
        /// </summary>
        private void ExecuteAuthorizationTypeChanged()
        {
            this.AuthView = SelectedAuthorizationType;
        }
    }
}
