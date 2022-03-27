namespace RestClientLibrary.ViewModel
{
    using System.Collections.ObjectModel;
    using DataLibrary;

    /// <summary>
    /// Defines the <see cref = "SaveRequestDataViewModel"/>
    /// </summary>
    public class SaveRequestDataViewModel : BaseViewModel
    {
        /// <summary>
        /// The categories field
        /// </summary>
        private ObservableCollection<CategoryViewModel> categories;

        /// <summary>
        /// The requests field
        /// </summary>
        private ObservableCollection<TransactionViewModel> requests;

        /// <summary>
        /// Gets or sets the Categories
        /// </summary>
        public ObservableCollection<CategoryViewModel> Categories
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
        /// Gets or sets the Requests
        /// </summary>
        public ObservableCollection<TransactionViewModel> Requests
        {
            get
            {
                return this.requests;
            }

            set
            {
                this.requests = value;
                this.OnPropertyChanged("Requests");
            }
        }
    }
}
