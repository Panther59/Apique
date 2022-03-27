namespace RestClientLibrary.ViewModel
{
    using DataLibrary;

    /// <summary>
    /// Defines the <see cref = "RequestContentTypeCategoryViewModel"/>
    /// </summary>
    public class RequestContentTypeCategoryViewModel : BaseViewModel
    {
        /// <summary>
        /// The name field
        /// </summary>
        private string name;

        /// <summary>
        /// The value field
        /// </summary>
        private string value;

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
        /// Gets or sets the Value
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
                this.OnPropertyChanged("Value");
            }
        }
    }
}
