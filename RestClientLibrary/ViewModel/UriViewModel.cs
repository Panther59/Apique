// <copyright file="UriViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>13-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using DataLibrary;
	using Newtonsoft.Json;

	/// <summary>
	/// Defines the <see cref="UriViewModel" />
	/// </summary>
	public class UriViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// Defines the 
        /// </summary>
        private string _enteredUrl;

        /// <summary>
        /// Defines the 
        /// </summary>
        private string _hash;

        /// <summary>
        /// Defines the 
        /// </summary>
        private string _host;

        /// <summary>
        /// Defines the 
        /// </summary>
        private bool _isExpanded;

        /// <summary>
        /// Defines the 
        /// </summary>
        private string _path;

        /// <summary>
        /// Defines the 
        /// </summary>
        private ObservableCollection<QueryStringViewModel> _queryString;

        /// <summary>
        /// Defines the 
        /// </summary>
        private string _scheme;

        /// <summary>
        /// Defines the 
        /// </summary>
        private ObservableCollection<TextType> _urls;

        #region Commands

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand _addNewQueryStringClickedCommand;

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand _urlExpandedCommand;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UriViewModel"/> class.
        /// </summary>
        public UriViewModel()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the EnteredUrl
        /// </summary>
        public string EnteredUrl
        {
            get { return _enteredUrl; }
            set
            {
                _enteredUrl = value;
                OnPropertyChanged("EnteredUrl");
            }
        }

        /// <summary>
        /// Gets or sets the Hash
        /// </summary>
        public string Hash
        {
            get { return _hash; }
            set
            {
                _hash = value;
                OnPropertyChanged("Hash");
            }
        }

        /// <summary>
        /// Gets or sets the Host
        /// </summary>
        public string Host
        {
            get { return _host; }
            set
            {
                _host = value;
                OnPropertyChanged("Host");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsExpanded
        /// </summary>
		[JsonIgnore]
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        /// <summary>
        /// Gets or sets the Path
        /// </summary>
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }

        /// <summary>
        /// Gets or sets the QueryStrings
        /// </summary>
        public ObservableCollection<QueryStringViewModel> QueryStrings
        {
            get { return _queryString; }
            set
            {
                _queryString = value;
                OnPropertyChanged("QueryStrings");
            }
        }

        /// <summary>
        /// Gets or sets the Scheme
        /// </summary>
        public string Scheme
        {
            get { return _scheme; }
            set
            {
                _scheme = value;
                OnPropertyChanged("Scheme");
            }
        }

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url
        {
            get
            {
                if (IsExpanded)
                {
                    return BuildUrlFromForm();
                }
                else
                {
                    return EnteredUrl;
                }
            }
            set
            {
                EnteredUrl = value ?? string.Empty;
                if (IsExpanded)
                {
                    LoadUrlDetails();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Urls
        /// </summary>
        public ObservableCollection<TextType> Urls
        {
            get { return _urls; }
            set
            {
                _urls = value;
                OnPropertyChanged("Urls");
            }
        }

        #region Commands

        /// <summary>
        /// Gets or sets the AddNewQueryStringClickedCommand
        /// </summary>
		[JsonIgnore]
        public RelayCommand AddNewQueryStringClickedCommand
        {
            get
            {
                if (_addNewQueryStringClickedCommand == null)
                {
                    _addNewQueryStringClickedCommand = new RelayCommand(command => AddNewQueryStringClicked());
                }
                return _addNewQueryStringClickedCommand;
            }
            set { _addNewQueryStringClickedCommand = value; }
        }

        /// <summary>
        /// Gets or sets the UrlExpandedCommand
        /// </summary>
		[JsonIgnore]
        public RelayCommand UrlExpandedCommand
        {
            get
            {
                if (_urlExpandedCommand == null)
                {
                    _urlExpandedCommand = new RelayCommand(command => UrlExpanded());
                }
                return _urlExpandedCommand;
            }
            set { _urlExpandedCommand = value; }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// The queryString_Remove
        /// </summary>
        /// <param name="qs">The <see cref="QueryStringViewModel"/></param>
        internal void queryString_Remove(QueryStringViewModel qs)
        {
            QueryStrings.Remove(qs);

            if (QueryStrings.Count == 0)
            {
                AddNewQueryStringClicked();
            }
        }

        #region Private Methods

        /// <summary>
        /// The AddNewQueryStringClicked
        /// </summary>
        private void AddNewQueryStringClicked()
        {
            try
            {
                QueryStringViewModel queryString = new QueryStringViewModel();
                queryString.Remove += queryString_Remove;
                QueryStrings.Add(queryString);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// The AddUrlToDropDown
        /// </summary>
        private void AddUrlToDropDown()
        {
            string actualUrl = EnteredUrl;
            ObservableCollection<TextType> urls = new ObservableCollection<TextType>(_urls);
            if (urls == null)
            {
                urls = new ObservableCollection<TextType>();
            }
            TextType url = new TextType() { Text = actualUrl };
            urls.Remove(url);
            urls.Insert(0, url);
            Urls = urls;
        }

        /// <summary>
        /// The BuildUrlFromForm
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        private string BuildUrlFromForm()
        {
            UriBuilder ub = new UriBuilder();
            try
            {
                if (string.IsNullOrEmpty(Host) == false &&
                    string.IsNullOrEmpty(Scheme))
                {
                    ub.Scheme = Scheme;
                    if (Host.GetString().Contains(":") && Host.GetString().Split(':').Length > 1)
                    {
                        ub.Host = Host.Split(':').First();
                        ub.Port = Convert.ToInt16(Host.Split(':').Last());
                    }
                    else
                    {
                        ub.Host = Host;
                    }
                    ub.Path = Path;
                    ub.Query = GetQueryString(QueryStrings);
                    ub.Fragment = Hash;
                    return ub.Uri.ToString();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            return string.Empty;
        }

        /// <summary>
        /// The GetQueryString
        /// </summary>
        /// <param name="queryStrings">The <see cref="ObservableCollection{QueryStringViewModel}"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string GetQueryString(ObservableCollection<QueryStringViewModel> queryStrings)
        {
            StringBuilder sb = new StringBuilder();
            if (queryStrings != null)
            {
                for (int i = 0; i < queryStrings.Count; i++)
                {

                    if (queryStrings[i].Key.GetString().Trim() != string.Empty &&
                        queryStrings[i].Value.GetString().Trim() != string.Empty)
                    {
                        sb.Append(queryStrings[i].Key);
                        sb.Append("=");
                        sb.Append(queryStrings[i].Value);
                        if (i != queryStrings.Count - 1)
                        {
                            sb.Append("&");
                        }
                    }
                }
            }
            return sb.ToString().Trim();
        }

        /// <summary>
        /// The LoadQueryStrings
        /// </summary>
        /// <param name="querystring">The <see cref="string"/></param>
        private void LoadQueryStrings(string querystring)
        {
            List<QueryStringViewModel> lst = new List<QueryStringViewModel>();
            if (querystring != null && querystring.StartsWith("?"))
            {
                string[] strs = querystring.Remove(0, 1).Split('&');
                foreach (string str in strs)
                {
                    string[] parts = str.Split('=');
                    QueryStringViewModel queryString = new QueryStringViewModel()
                    {
                        Key = parts[0],
                        Value = parts[1]
                    };
                    queryString.Remove += queryString_Remove;
                    lst.Add(queryString);
                }
            }
            if (lst.Count == 0)
            {
                lst.Add(new QueryStringViewModel());
            }
            QueryStrings = new ObservableCollection<QueryStringViewModel>(lst);
        }

        /// <summary>
        /// The LoadUrlDetails
        /// </summary>
        private void LoadUrlDetails()
        {
            try
            {
                if (IsExpanded)
                {
                    if (EnteredUrl.GetString().Trim() != string.Empty)
                    {
                        Uri uri1 = new Uri(EnteredUrl);
                        Scheme = uri1.Scheme;
                        Host = uri1.Authority;
                        Path = uri1.AbsolutePath;
                        LoadQueryStrings(uri1.Query);
                        Hash = uri1.Fragment.GetString().Length > 1 ? uri1.Fragment.Remove(0, 1) : null;
                    }
                    else
                    {
                        Scheme = null;
                        Path = null;
                        LoadQueryStrings(null);
                        Hash = null;
                    }
                }
                else
                {
                    EnteredUrl = BuildUrlFromForm();

                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// The UrlExpanded
        /// </summary>
        private void UrlExpanded()
        {
            try
            {
                LoadUrlDetails();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        #endregion

        #endregion
    }
}
