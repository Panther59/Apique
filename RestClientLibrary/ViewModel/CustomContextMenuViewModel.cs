namespace RestClientLibrary.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    /// <summary>
    /// Defines the <see cref = "CustomContextMenuViewModel"/>
    /// </summary>
    public class CustomContextMenuViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "CustomContextMenuViewModel"/> class.
        /// </summary>
        /// <param name = "header">The <see cref = "string "/></param>
        /// <param name = "command">The <see cref = "RelayCommand"/></param>
        public CustomContextMenuViewModel(string header, ICommand command, string key)
        {
            this.Header = header;
            this.Command = command;
            this.Key = key;
        }

        /// <summary>
        /// Gets or sets the Children
        /// </summary>
        public ObservableCollection<CustomContextMenuViewModel> Children
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Command
        /// </summary>
        public ICommand Command
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Header
        /// </summary>
        public string Header
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Key
        /// </summary>
        public string Key
        {
            get;
            set;
        }
    }
}
