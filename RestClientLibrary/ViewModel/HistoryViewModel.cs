// <copyright file="HistoryViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>22-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using DataLibrary;
    using RestClientLibrary.Common;
    using RestClientLibrary.View;

    /// <summary>
    /// Defines the <see cref="HistoryViewModel" />
    /// </summary>
    public class HistoryViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// Defines the _view
        /// </summary>
        internal IHistoryView _view;

        /// <summary>
        /// Defines the _finalSessionHistory
        /// </summary>
        private ObservableCollection<BasicHistoryViewModel> _finalSessionHistory;

        /// <summary>
        /// Defines the _searchText
        /// </summary>
        private string _searchText;

        /// <summary>
        /// Defines the _sessionHistory
        /// </summary>
        private ObservableCollection<BasicHistoryViewModel> _sessionHistory;

        #region Commands

        /// <summary>
        /// Defines the _searchCommand
        /// </summary>
        private RelayCommand _searchCommand;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryViewModel"/> class.
        /// </summary>
        /// <param name="view">The <see cref="IHistoryView"/></param>
        public HistoryViewModel(IHistoryView view)
        {
            _view = view;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the FinalSessionHistory
        /// </summary>
        public ObservableCollection<BasicHistoryViewModel> FinalSessionHistory
        {
            get { return _finalSessionHistory; }
            private set
            {
                _finalSessionHistory = value;
                OnPropertyChanged("FinalSessionHistory");
            }
        }
        /// <summary>
        /// Gets or sets the ParentViewModel
        /// </summary>
        public MainViewModel ParentViewModel { get; set; }
        /// <summary>
        /// Gets or sets the SearchText
        /// </summary>
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }
        /// <summary>
        /// Gets or sets the SessionHistory
        /// </summary>
        public ObservableCollection<BasicHistoryViewModel> SessionHistory
        {
            get { return _sessionHistory; }
            set
            {
                _sessionHistory = value;
                this.FilteredSessionData();
            }
        }
        #region Commands

        /// <summary>
        /// Gets or sets the SearchCommand
        /// </summary>
        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(command => Search());
                }
                return _searchCommand;
            }
            set { _searchCommand = value; }
        }

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The AddSession
        /// </summary>
        /// <param name="session">The <see cref="TransactionViewModel"/></param>
        public void AddSession(TransactionViewModel session)
        {
            try
            {
                var sessionHistory = this.SessionHistory != null ? this.SessionHistory : new ObservableCollection<BasicHistoryViewModel>();
                var oldSession = sessionHistory.FirstOrDefault(x => x.Guid == session.Guid);
                var newSession = new BasicHistoryViewModel(session);
                newSession.MainViewModel = this.ParentViewModel;

                if (oldSession != null)
                {
                    var index = sessionHistory.IndexOf(oldSession);
                    sessionHistory[index] = newSession;
                }
                else
                {
                    sessionHistory.Insert(0, newSession);
                }

                this.SessionHistory = new ObservableCollection<BasicHistoryViewModel>(sessionHistory);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
        }

        /// <summary>
        /// The FilteredSessionData
        /// </summary>
        public void FilteredSessionData()
        {
            ObservableCollection<BasicHistoryViewModel> sessionHistory = _sessionHistory;

            bool filterViaRequest = true;
            bool filterViaHeader = true;
            bool filterViaStatus = true;

            if (this.ParentViewModel != null && this.ParentViewModel.Settings != null)
            {
                filterViaRequest = this.ParentViewModel.Settings.SearchInRequest;
                filterViaHeader = this.ParentViewModel.Settings.SearchInHeaders;
                filterViaStatus = this.ParentViewModel.Settings.SearchInStatus;
            }

            var searchText = SearchText.GetString().ToLower();
            var filData = _sessionHistory.Where(x =>
                string.IsNullOrEmpty(searchText) ||
                x.PreUrl.GetString().ToLower().Contains(searchText) ||
                x.Url.GetString().ToLower().Contains(searchText) ||
                (filterViaRequest == true && x.RequestContent.GetString().Contains(searchText)) ||
                (filterViaStatus == true && ((x.StatusCode.HasValue && x.StatusCode.ToString().Contains(searchText)) || x.StatusDescription.GetString().Contains(searchText))));

            this.FinalSessionHistory = new ObservableCollection<BasicHistoryViewModel>(filData);
        }

        /// <summary>
        /// The LoadData
        /// </summary>
        /// <param name="sessionData">The <see cref="IEnumerable{BasicHistoryViewModel}"/></param>
        public void LoadData(IEnumerable<BasicHistoryViewModel> sessionData)
        {
            this.SessionHistory = new ObservableCollection<BasicHistoryViewModel>(sessionData);

            foreach (var session in this.SessionHistory)
            {
                session.MainViewModel = ParentViewModel;
            }
        }

        /// <summary>
        /// The RemoveSession
        /// </summary>
        /// <param name="session">The <see cref="SessionHistoryViewModel"/></param>
        public void RemoveSession(SessionHistoryViewModel session)
        {
            try
            {
                var sessionHistory = this.SessionHistory != null ? this.SessionHistory : new ObservableCollection<BasicHistoryViewModel>();
                var oldSession = sessionHistory.FirstOrDefault(x => x.Guid == session.Guid);
                if (oldSession != null)
                {
                    sessionHistory.Remove(oldSession);
                    this.SessionHistory = new ObservableCollection<BasicHistoryViewModel>(sessionHistory);
                }

                AppDataHelper.RemoveSession(session);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The Search
        /// </summary>
        private void Search()
        {
            try
            {
                FilteredSessionData();
            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
        }

        /// <summary>
        /// The Session_SessionSelected
        /// </summary>
        /// <param name="session">The <see cref="SessionHistoryViewModel"/></param>
        /// <param name="type">The <see cref="string"/></param>
        private void Session_SessionSelected(SessionHistoryViewModel session, string type)
        {
            session.Guid = Guid.NewGuid().ToString();
            this.ParentViewModel.SessionSelected(session, type);
        }

        #endregion

        #endregion
    }
}
