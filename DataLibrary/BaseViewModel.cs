namespace DataLibrary
{
	using Newtonsoft.Json;
	using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the <see cref = "BaseViewModel"/>
    /// </summary>
    [Serializable]
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Defines the isBusy
        /// </summary>
        private bool isBusy;

        /// <summary>
        /// Initializes a new instance of the <see cref = "BaseViewModel"/> class.
        /// </summary>
        public BaseViewModel()
        {
        }

        /// <summary>
        /// Defines the PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether IsBusy
        /// </summary>
        [JsonIgnore]
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                this.isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        /// <summary>
        /// The LogException
        /// </summary>
        /// <param name = "ex">The <see cref = "Exception"/></param>
        public void LogException(Exception ex)
        {
            ErrorLog.LogException(ex);
        }

        /// <summary>
        /// The OnPropertyChanged
        /// </summary>
        /// <param name = "propertyName">The <see cref = "string "/></param>
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
