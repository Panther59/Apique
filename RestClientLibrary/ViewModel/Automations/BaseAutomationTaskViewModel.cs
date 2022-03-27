namespace RestClientLibrary.ViewModel.Automations
{
    using DataLibrary;

    /// <summary>
    /// Defines the <see cref = "BaseAutomationTaskViewModel"/>
    /// </summary>
    public abstract class BaseAutomationTaskViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAutomationTaskViewModel"/> class.
        /// </summary>
        public BaseAutomationTaskViewModel()
        {
            this.Guid = System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets or sets the Guid
        /// </summary>
        public string Guid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Name
        /// </summary>
        public abstract string Name
        {
            get;
        }
    }
}
