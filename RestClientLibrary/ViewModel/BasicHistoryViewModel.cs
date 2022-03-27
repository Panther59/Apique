// <copyright file="BasicHistoryViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using System;
    using System.Windows;
    using System.Xml.Serialization;
    using DataLibrary;
    using RestClientLibrary.Common;

    /// <summary>
    /// Defines the <see cref="BasicHistoryViewModel" />
    /// </summary>
    public class BasicHistoryViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// Defines the 
        /// </summary>
        private string _guid;

        /// <summary>
        /// Defines the 
        /// </summary>
        private string _operation;

        /// <summary>
        /// Defines the 
        /// </summary>
        private string _requestContent;

        /// <summary>
        /// Defines the 
        /// </summary>
        private int? _statusCode;

        /// <summary>
        /// Defines the 
        /// </summary>
        private string _statusDescription;

        /// <summary>
        /// Defines the 
        /// </summary>
        private DateTime _time;

        /// <summary>
        /// Defines the 
        /// </summary>
        private string _url;

        /// <summary>
        /// The isSuccess field
        /// </summary>
        private bool? isSuccess;

        /// <summary>
        /// The isValidationSuccess field
        /// </summary>
        private bool? isValidationSuccess;

        /// <summary>
        /// The preRequestContent field
        /// </summary>
        private string preRequestContent;

        /// <summary>
        /// The preUrl field
        /// </summary>
        private string preUrl;

        #region Commands

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand<string> _copySessionCommand;

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand<bool> _emailAsAttachmentCommand;

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand<bool> _emailInBodyCommand;

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand _removeSessionCommand;

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand _saveRequestToFileCommand;

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand _saveRequestToMasterCommand;

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand _sessionSelectedCommand;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicHistoryViewModel"/> class.
        /// </summary>
        public BasicHistoryViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicHistoryViewModel"/> class.
        /// </summary>
        /// <param name="s">The <see cref="TransactionViewModel"/></param>
        public BasicHistoryViewModel(TransactionViewModel s)
            : this()
        {
            if (s == null)
                return;

            this.Guid = s.Guid;
            this.IsValidationSuccess = s.IsValidationSuccessFul;
            this.IsSuccess = s.IsCallSessessFul.HasValue ? s.IsCallSessessFul.Value : (bool?)(s.StatusCode.HasValue ? (bool?)(s.StatusCode.Value >= 200 && s.StatusCode.Value < 300) : null);
            this.Time = s.Time;
            this.Operation = s.Operation;
            this.Url = s.Url;
            this.PreUrl = s.PreUrl;
            this.PreRequestContent = s.PreRequestContent;
            this.RequestContent = s.RequestContent;
            this.StatusCode = s.StatusCode;
            this.StatusDescription = s.StatusDescription;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicHistoryViewModel"/> class.
        /// </summary>
        /// <param name="s">The <see cref="SessionHistoryViewModel"/></param>
        public BasicHistoryViewModel(SessionHistoryViewModel s)
            : this()
        {
            if (s == null)
                return;

            this.Guid = s.Guid;
            this.IsValidationSuccess = s.IsValidationSuccessFul;
            this.IsSuccess = s.IsCallSessessFul.HasValue ? s.IsCallSessessFul.Value : (bool?)(s.StatusCode.HasValue ? (bool?)(s.StatusCode.Value >= 200 && s.StatusCode.Value < 300) : null);
            this.Time = s.Time;
            this.Operation = s.Operation;
            this.Url = s.Url;
            this.PreUrl = s.PreUrl;
            this.PreRequestContent = s.PreRequestContent;
            this.RequestContent = s.RequestContent;
            this.StatusCode = s.StatusCode;
            this.StatusDescription = s.StatusDescription;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Guid
        /// </summary>
        public string Guid
        {
            get { return this._guid; }
            set
            {
                this._guid = value;
                this.OnPropertyChanged("Guid");
            }
        }

        /// <summary>
        /// Gets or sets the Time
        /// </summary>
        public DateTime Time
        {
            get { return this._time; }
            set
            {
                this._time = value;
                this.OnPropertyChanged("Time");
            }
        }

        /// <summary>
        /// Gets or sets the IsSuccess
        /// </summary>
        public bool? IsSuccess
        {
            get
            {
                return this.isSuccess;
            }

            set
            {
                this.isSuccess = value;
                this.OnPropertyChanged("IsSuccess");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsValidationSuccess
        /// </summary>
        public bool? IsValidationSuccess
        {
            get
            {
                return this.isValidationSuccess;
            }

            set
            {
                this.isValidationSuccess = value;
                this.OnPropertyChanged("IsValidationSuccess");
            }
        }

        /// <summary>
        /// Gets or sets the Operation
        /// </summary>
        public string Operation
        {
            get { return this._operation; }
            set
            {
                this._operation = value;
                this.OnPropertyChanged("Operation");
            }
        }

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url
        {
            get { return this._url; }
            set
            {
                this._url = value;
                this.OnPropertyChanged("Url");
            }
        }

        /// <summary>
        /// Gets or sets the PreUrl
        /// </summary>
        public string PreUrl
        {
            get
            {
                return this.preUrl;
            }

            set
            {
                this.preUrl = value;
                this.OnPropertyChanged("PreUrl");
            }
        }

        /// <summary>
        /// Gets or sets the PreRequestContent
        /// </summary>
        public string PreRequestContent
        {
            get
            {
                return this.preRequestContent;
            }

            set
            {
                this.preRequestContent = value;
                this.OnPropertyChanged("PreRequestContent");
            }
        }

        /// <summary>
        /// Gets or sets the RequestContent
        /// </summary>
        public string RequestContent
        {
            get { return this._requestContent; }
            set
            {
                this._requestContent = value;
                this.OnPropertyChanged("RequestContent");
            }
        }

        /// <summary>
        /// Gets or sets the StatusCode
        /// </summary>
        public int? StatusCode
        {
            get { return this._statusCode; }
            set
            {
                this._statusCode = value;
                this.OnPropertyChanged("StatusCode");
            }
        }

        /// <summary>
        /// Gets or sets the StatusDescription
        /// </summary>
        public string StatusDescription
        {
            get { return this._statusDescription; }
            set
            {
                this._statusDescription = value;
                this.OnPropertyChanged("StatusDescription");
            }
        }

        /// <summary>
        /// Gets or sets the MainViewModel
        /// </summary>
        [XmlIgnore]
        public WorkspaceViewModel MainViewModel
        {
            get; set;
        }

        #region Commands

        /// <summary>
        /// Gets or sets the CopySessionCommand
        /// </summary>
        [XmlIgnore]
        public RelayCommand<string> CopySessionCommand
        {
            get
            {
                if (_copySessionCommand == null)
                {
                    _copySessionCommand = new RelayCommand<string>(command => CopySession(command));
                }
                return _copySessionCommand;
            }
            set { _copySessionCommand = value; }
        }

        /// <summary>
        /// Gets or sets the EmailAsAttachmentCommand
        /// </summary>
        [XmlIgnore]
        public RelayCommand<bool> EmailAsAttachmentCommand
        {
            get
            {
                if (_emailAsAttachmentCommand == null)
                {
                    _emailAsAttachmentCommand = new RelayCommand<bool>(command => EmailAsAttachment(command));
                }
                return _emailAsAttachmentCommand;
            }
            set { _emailAsAttachmentCommand = value; }
        }

        /// <summary>
        /// Gets or sets the EmailInBodyCommand
        /// </summary>
        [XmlIgnore]
        public RelayCommand<bool> EmailInBodyCommand
        {
            get
            {
                if (_emailInBodyCommand == null)
                {
                    _emailInBodyCommand = new RelayCommand<bool>(command => EmailInBody(command));
                }
                return _emailInBodyCommand;
            }
            set { _emailInBodyCommand = value; }
        }

        /// <summary>
        /// Gets or sets the RemoveSessionCommand
        /// </summary>
        [XmlIgnore]
        public RelayCommand RemoveSessionCommand
        {
            get
            {
                if (_removeSessionCommand == null)
                {
                    _removeSessionCommand = new RelayCommand(command => RemoveSession());
                }
                return _removeSessionCommand;
            }
            set { _removeSessionCommand = value; }
        }

        /// <summary>
        /// Gets or sets the SaveRequestToFileCommand
        /// </summary>
        [XmlIgnore]
        public RelayCommand SaveRequestToFileCommand
        {
            get
            {
                if (_saveRequestToFileCommand == null)
                {
                    _saveRequestToFileCommand = new RelayCommand(command => SaveRequestToFile());
                }
                return _saveRequestToFileCommand;
            }
            set { _saveRequestToFileCommand = value; }
        }

        /// <summary>
        /// Gets or sets the SaveRequestToMasterCommand
        /// </summary>
        [XmlIgnore]
        public RelayCommand SaveRequestToMasterCommand
        {
            get
            {
                if (_saveRequestToMasterCommand == null)
                {
                    _saveRequestToMasterCommand = new RelayCommand(command => SaveRequestToMaster());
                }
                return _saveRequestToMasterCommand;
            }
            set { _saveRequestToMasterCommand = value; }
        }

        /// <summary>
        /// Gets or sets the SessionSelectedCommand
        /// </summary>
        [XmlIgnore]
        public RelayCommand SessionSelectedCommand
        {
            get
            {
                if (_sessionSelectedCommand == null)
                {
                    _sessionSelectedCommand = new RelayCommand(command => OnSessionSelected(command));
                }
                return _sessionSelectedCommand;
            }
            set { _sessionSelectedCommand = value; }
        }

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The GetCurrentSession
        /// </summary>
        /// <returns>The <see cref="SessionHistoryViewModel"/></returns>
        public SessionHistoryViewModel GetCurrentSession()
        {
            SessionHistoryViewModel session = Common.AppDataHelper.GetSessionHistoryData(this.Guid);
            return session;
        }

        /// <summary>
        /// The GetTransactionObject
        /// </summary>
        /// <param name="includeResponse">The <see cref="bool"/></param>
        /// <returns>The <see cref="TransactionViewModel"/></returns>
        public TransactionViewModel GetTransactionObject(bool includeResponse = true)
        {
            SessionHistoryViewModel session = this.GetCurrentSession();
            TransactionViewModel trans = session.GetTransactionObject(includeResponse);

            return trans;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The CopySession
        /// </summary>
        /// <param name="type">The <see cref="string"/></param>
        private void CopySession(string type)
        {
            try
            {
                var session = this.GetCurrentSession();
                switch (type.GetString().ToLower())
                {
                    case "url":
                        Clipboard.SetText(this.Url);
                        break;
                    case "request":
                        Clipboard.SetText(this.RequestContent);
                        break;
                    case "headers":
                        Clipboard.SetText(session.Headers);
                        break;
                    case "responseheaders":
                        Clipboard.SetText(session.ResponseHeaders);
                        break;
                    case "response":
                        Clipboard.SetText(session.ResponseContent);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }
        }

        /// <summary>
        /// The EmailAsAttachment
        /// </summary>
        /// <param name="includeResponse">The <see cref="bool"/></param>
        private void EmailAsAttachment(bool includeResponse)
        {
            this.MainViewModel.EmailAsAttachment(this.GetTransactionObject(false));
        }

        /// <summary>
        /// The EmailInBody
        /// </summary>
        /// <param name="includeResponse">The <see cref="bool"/></param>
        private void EmailInBody(bool includeResponse)
        {
            this.MainViewModel.EmailInBody(this.GetTransactionObject(includeResponse));
        }

        /// <summary>
        /// The OnSessionSelected
        /// </summary>
        /// <param name="type">The <see cref="object"/></param>
        private void OnSessionSelected(object type)
        {

            if (MainViewModel != null)
            {
                MainViewModel.SessionSelected(this.GetCurrentSession(), type != null ? type.ToString() : null);
            }
        }

        /// <summary>
        /// The RemoveSession
        /// </summary>
        private void RemoveSession()
        {
            MasterEventHandler.RaiseRemoveSessionEvent(this.GetCurrentSession());
        }

        /// <summary>
        /// The SaveRequestToFile
        /// </summary>
        private void SaveRequestToFile()
        {
            this.MainViewModel.SaveRequestToFile(this.GetTransactionObject(false));
        }

        /// <summary>
        /// The SaveRequestToMaster
        /// </summary>
        private void SaveRequestToMaster()
        {
            this.MainViewModel.SaveRequestClicked(this.GetTransactionObject(false));
            MasterEventHandler.RaiseReloadSavedRequestEvent();
        }

        #endregion

        #endregion
    }
}
