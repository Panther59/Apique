namespace RestClientLibrary.ViewModel
{
    using DataLibrary;

    /// <summary>
    /// Defines the <see cref="CodeErrorViewModel" />
    /// </summary>
    public class CodeErrorViewModel : BaseViewModel
    {
        /// <summary>
        /// The code field
        /// </summary>
        private string code;

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
        /// The description field
        /// </summary>
        private string description;

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                this.description = value;
                this.OnPropertyChanged("Description");
            }
        }

        /// <summary>
        /// The line field
        /// </summary>
        private int line;

        /// <summary>
        /// Gets or sets the Line
        /// </summary>
        public int Line
        {
            get
            {
                return this.line;
            }

            set
            {
                this.line = value;
                this.OnPropertyChanged("Line");
            }
        }

        /// <summary>
        /// The severity field
        /// </summary>
        private string severity;

        /// <summary>
        /// Gets or sets the Severity
        /// </summary>
        public string Severity
        {
            get
            {
                return this.severity;
            }

            set
            {
                this.severity = value;
                this.OnPropertyChanged("Severity");
            }
        }
    }
}
