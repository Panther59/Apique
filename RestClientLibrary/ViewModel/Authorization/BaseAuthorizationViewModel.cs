using DataLibrary;
using Newtonsoft.Json;

namespace RestClientLibrary.ViewModel.Authorization
{
    /// <summary>
    /// Defines the <see cref="BaseAuthorizationViewModel" />
    /// </summary>
    public abstract class BaseAuthorizationViewModel : BaseViewModel
    {
        /// <summary>
        /// The name field
        /// </summary>
        private string name;

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

		public abstract KeyValueViewModel GetAuthorizationHeader();

        public abstract void SetAuthorizationHeader(KeyValueViewModel header);

        public abstract bool IsValidAuthorization(KeyValueViewModel header);
    }
}
