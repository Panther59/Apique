namespace RestClientLibrary.ViewModel
{
    using DataLibrary;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Defines the <see cref = "HeadersViewModel"/>
    /// </summary>
    public class HeadersViewModel : BaseViewModel
    {
        /// <summary>
        /// Defines the _addNewHeaderCommand
        /// </summary>
        private RelayCommand _addNewHeaderCommand;

        /// <summary>
        /// Defines the _formHeaders
        /// </summary>
        private ObservableCollection<KeyValueViewModel> _formHeaders;

        /// <summary>
        /// Defines the _headers
        /// </summary>
        private string _headers;

        /// <summary>
        /// Defines the _headerTabChangedCommand
        /// </summary>
        private RelayCommand _headerTabChangedCommand;

        /// <summary>
        /// Defines the _previousTab
        /// </summary>
        private int _previousTab;

        /// <summary>
        /// Defines the _selectedTab
        /// </summary>
        private int _selectedTab;

        /// <summary>
        /// The authorization field
        /// </summary>
        private AuthorizationViewModel authorization;

        /// <summary>
        /// Defines the isTabChangeInProgress
        /// </summary>
        private bool isTabChangeInProgress;

        /// <summary>
        /// The knownHeaders field
        /// </summary>
        private List<string> knownHeaders;

        /// <summary>
        /// Initializes a new instance of the <see cref = "HeadersViewModel"/> class.
        /// </summary>
        public HeadersViewModel()
        {
        }

        /// <summary>
        /// Gets or sets the AddNewHeaderCommand
        /// </summary>
        public RelayCommand AddNewHeaderCommand
        {
            get
            {
                if (_addNewHeaderCommand == null)
                {
                    _addNewHeaderCommand = new RelayCommand(command => AddNewHeader());
                }

                return _addNewHeaderCommand;
            }

            set
            {
                _addNewHeaderCommand = value;
            }
        }

        /// <summary>
        /// Gets or sets the Authorization
        /// </summary>
        public AuthorizationViewModel Authorization
        {
            get
            {
                return this.authorization;
            }

            set
            {
                this.authorization = value;
                this.OnPropertyChanged("Authorization");
            }
        }

        /// <summary>
        /// Gets or sets the FormHeaders
        /// </summary>
        public ObservableCollection<KeyValueViewModel> FormHeaders
        {
            get
            {
                return _formHeaders;
            }

            set
            {
                _formHeaders = value;
                OnPropertyChanged("FormHeaders");
            }
        }

        /// <summary>
        /// Gets or sets the Headers
        /// </summary>
        public string Headers
        {
            get
            {
                return _headers;
            }

            set
            {
                _headers = value;
                OnPropertyChanged("Headers");
            }
        }

        /// <summary>
        /// Gets or sets the HeaderTabChangedCommand
        /// </summary>
        public RelayCommand HeaderTabChangedCommand
        {
            get
            {
                if (_headerTabChangedCommand == null)
                {
                    _headerTabChangedCommand = new RelayCommand(command => HeaderTabChanged());
                }

                return _headerTabChangedCommand;
            }

            set
            {
                _headerTabChangedCommand = value;
            }
        }

        /// <summary>
        /// Gets or sets the KnownHeaders
        /// </summary>
        public List<string> KnownHeaders
        {
            get
            {
                return this.knownHeaders;
            }

            set
            {
                this.knownHeaders = value;
                this.OnPropertyChanged("KnownHeaders");
            }
        }

        /// <summary>
        /// Gets or sets the SelctedTab
        /// </summary>
        public int SelctedTab
        {
            get
            {
                return _selectedTab;
            }

            set
            {
                _selectedTab = value;
                OnPropertyChanged("SelctedTab");
            }
        }

        /// <summary>
        /// The GetHeaderContent
        /// </summary>
        /// <returns>The <see cref = "string "/></returns>
        public string GetHeaderContent()
        {
            if (this.SelctedTab != (int)Common.Constants.HeaderTab.Raw)
            {
                if (this.SelctedTab == (int)Common.Constants.HeaderTab.Authorization)
                {
                    LoadDataToAuthorization();
                }

                LoadDataToRaw();
            }

            return this.Headers;
        }

        /// <summary>
        /// The LoadData
        /// </summary>
        public void LoadData()
        {
            this.KnownHeaders = Enum.GetNames(typeof(System.Net.HttpRequestHeader)).ToList();

            this.Authorization = new AuthorizationViewModel();
            this.Authorization.LoadData();
        }

        /// <summary>
        /// The SetHeaders
        /// </summary>
        /// <param name = "header">The <see cref = "string "/></param>
        public void SetHeaders(string header)
        {
            this.Headers = header;
            if (this.SelctedTab != (int)Common.Constants.HeaderTab.Raw)
            {
                this.LoadFromRaw();
                this.LoadDataToAuthorization();
            }
        }

        /// <summary>
        /// The header_Remove
        /// </summary>
        /// <param name = "header">The <see cref = "KeyValueViewModel"/></param>
        internal void Header_Remove(KeyValueViewModel header)
        {
            FormHeaders.Remove(header);
            if (FormHeaders == null || FormHeaders.Count == 0)
            {
                AddNewHeader();
            }
        }

        /// <summary>
        /// The AddNewHeader
        /// </summary>
        private void AddNewHeader()
        {
            try
            {
                if (FormHeaders == null || FormHeaders.Count == 0)
                {
                    FormHeaders = new ObservableCollection<KeyValueViewModel>();
                }

                KeyValueViewModel header = new KeyValueViewModel();
                header.Remove += Header_Remove;
                FormHeaders.Add(header);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// The HeaderTabChanged
        /// </summary>
        private void HeaderTabChanged()
        {
            if (isTabChangeInProgress == true || _previousTab == this.SelctedTab)
            {
                return;
            }

            isTabChangeInProgress = true;
            try
            {
                if ((Common.Constants.HeaderTab)_previousTab == Common.Constants.HeaderTab.Authorization)
                {
                    LoadDataFromAuthorization();
                }
                else if ((Common.Constants.HeaderTab)_previousTab == Common.Constants.HeaderTab.Raw)
                {
                    LoadFromRaw();
                }

                if ((Common.Constants.HeaderTab)_selectedTab == Common.Constants.HeaderTab.Raw)
                {
                    LoadDataToRaw();
                }
                else if ((Common.Constants.HeaderTab)_selectedTab == Common.Constants.HeaderTab.Authorization)
                {
                    LoadDataToAuthorization();
                }

                _previousTab = this._selectedTab;
            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
            finally
            {
                isTabChangeInProgress = false;
            }
        }

        /// <summary>
        /// The LoadDataFromAuthorization
        /// </summary>
        private void LoadDataFromAuthorization()
        {
            var authHeader = this.Authorization.GetAuthorizationHeader();
            if (authHeader != null)
            {
                if (this.FormHeaders.Any(x => x.Key.GetString().Equals("Authorization")))
                {
                    this.FormHeaders.First(x => x.Key.GetString().Equals("Authorization")).Value = authHeader.Value;
                }
                else
                {
                    this.FormHeaders.Insert(0, authHeader);
                }
            }
            else
            {
                if (this.FormHeaders != null && this.FormHeaders.Any(x => x.Key.GetString().Equals("Authorization")))
                {
                    this.FormHeaders.Remove(this.FormHeaders.First(x => x.Key.GetString().Equals("Authorization")));
                    this.FormHeaders = new ObservableCollection<KeyValueViewModel>(this._formHeaders);
                }
            }
        }

        /// <summary>
        /// The LoadDataToAuthorization
        /// </summary>
        private void LoadDataToAuthorization()
        {
            if (FormHeaders != null && FormHeaders.Any(x => x.Key.GetString().Equals("Authorization")))
            {
                this.Authorization.LoadAuthorizationData(FormHeaders.First(x => x.Key.GetString().Equals("Authorization")));
            }
            else
            {
                this.Authorization.LoadAuthorizationData(null);
            }
        }

        /// <summary>
        /// The LoadDataToRaw
        /// </summary>
        private void LoadDataToRaw()
        {
            StringBuilder sb = new StringBuilder();
            if (FormHeaders != null)
            {
                foreach (KeyValueViewModel head in FormHeaders)
                {
                    if (head.Key.GetString().Trim() != string.Empty)
                    {
                        sb.Append(head.Key);
                        sb.Append(":");
                        sb.Append(head.Value);
                        sb.AppendLine();
                    }
                }
            }

            Headers = sb.ToString().Trim();
        }

        /// <summary>
        /// The LoadFromRaw
        /// </summary>
        private void LoadFromRaw()
        {
            List<KeyValueViewModel> output = new List<KeyValueViewModel>();
            string headerText = Headers.GetString().Trim();
            if (headerText != null)
            {
                string[] multiheaders = headerText.Replace(Environment.NewLine, "\n").Split('\n');
                foreach (string header in multiheaders)
                {
                    if (header.GetString().Trim() != string.Empty)
                    {
                        int pos = header.IndexOf(':');
                        if (pos > -1)
                        {
                            KeyValueViewModel obj = new KeyValueViewModel()
                            { Key = header.Substring(0, pos).GetString().Trim(), Value = header.Substring(pos + 1, header.Length - pos - 1).GetString().Trim() };
                            output.Add(obj);
                        }
                        else
                        {
                            KeyValueViewModel obj = new KeyValueViewModel()
                            { Key = header.GetString() };
                            output.Add(obj);
                        }
                    }
                }
            }

            FormHeaders = new ObservableCollection<KeyValueViewModel>(output);
            if (output.Count == 0)
            {
                AddNewHeader();
            }
        }
    }
}
