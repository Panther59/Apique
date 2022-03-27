namespace RestClientLibrary.ViewModel.Automations
{
    using DataLibrary;

    /// <summary>
    /// Defines the <see cref = "SnippetViewModel"/>
    /// </summary>
    public class SnippetViewModel : BaseViewModel
    {
        /// <summary>
        /// The code field
        /// </summary>
        private string code;

        /// <summary>
        /// The name field
        /// </summary>
        private string name;

        /// <summary>
        /// Gets or sets the Code
        /// </summary>
        public string Code
        {
            get
            {
                return this.code;
            }

            set
            {
                this.code = value;
                this.OnPropertyChanged("Code");
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
    }
}
