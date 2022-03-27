namespace RestClientLibrary.ViewModel
{
    using System;
    using DataLibrary;

    /// <summary>
    /// Defines the <see cref="QueryStringViewModel" />
    /// </summary>
    public class QueryStringViewModel : BaseViewModel
    {
        /// <summary>
        /// Defines the _decodeClickedCommand
        /// </summary>
        private RelayCommand _decodeClickedCommand;

        /// <summary>
        /// Defines the _encodeClickedCommand
        /// </summary>
        private RelayCommand _encodeClickedCommand;

        /// <summary>
        /// Defines the _key
        /// </summary>
        private string _key;

        /// <summary>
        /// Defines the _removeClickedCommand
        /// </summary>
        private RelayCommand _removeClickedCommand;

        /// <summary>
        /// Defines the _value
        /// </summary>
        private string _value;

        /// <summary>
        /// Defines the Remove
        /// </summary>
        public event OnRemoveHandler Remove;

        /// <summary>
        /// Gets or sets the DecodeClickedCommand
        /// </summary>
        public RelayCommand DecodeClickedCommand
        {
            get
            {
                if (_decodeClickedCommand == null)
                {
                    _decodeClickedCommand = new RelayCommand(command => DecodeClicked());
                }

                return _decodeClickedCommand;
            }

            set
            {
                _decodeClickedCommand = value;
            }
        }

        /// <summary>
        /// Gets or sets the EncodeClickedCommand
        /// </summary>
        public RelayCommand EncodeClickedCommand
        {
            get
            {
                if (_encodeClickedCommand == null)
                {
                    _encodeClickedCommand = new RelayCommand(command => EncodeClicked());
                }

                return _encodeClickedCommand;
            }

            set
            {
                _encodeClickedCommand = value;
            }
        }

        /// <summary>
        /// Gets or sets the Key
        /// </summary>
        public string Key
        {
            get
            {
                return _key;
            }

            set
            {
                _key = value;
                OnPropertyChanged("Key");
            }
        }

        /// <summary>
        /// Gets or sets the RemoveClickedCommand
        /// </summary>
        public RelayCommand RemoveClickedCommand
        {
            get
            {
                if (_removeClickedCommand == null)
                {
                    _removeClickedCommand = new RelayCommand(command => RemoveClicked());
                }

                return _removeClickedCommand;
            }

            set
            {
                _removeClickedCommand = value;
            }
        }

        /// <summary>
        /// Gets or sets the Value
        /// </summary>
        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        /// <summary>
        /// The RaiseRemoveEvent
        /// </summary>
        /// <param name="qs">The <see cref="QueryStringViewModel"/></param>
        public void RaiseRemoveEvent(QueryStringViewModel qs)
        {
            if (Remove != null)
            {
                Remove(qs);
            }
        }

        /// <summary>
        /// The DecodeClicked
        /// </summary>
        private void DecodeClicked()
        {
            try
            {
                Value = System.Net.WebUtility.UrlDecode(Value);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// The EncodeClicked
        /// </summary>
        private void EncodeClicked()
        {
            try
            {
                Value = System.Net.WebUtility.UrlEncode(Value);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// The RemoveClicked
        /// </summary>
        private void RemoveClicked()
        {
            RaiseRemoveEvent(this);
        }

        public delegate void OnRemoveHandler(QueryStringViewModel qs);
    }
}
