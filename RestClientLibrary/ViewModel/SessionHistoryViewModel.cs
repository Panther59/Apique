using DataLibrary;
using RestClientLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DataLibrary;
using System.Windows;
using Newtonsoft.Json;

namespace RestClientLibrary.ViewModel
{
    public class SessionHistoryViewModel : TransactionViewModel
    {
        public SessionHistoryViewModel()
        {

        }

        public SessionHistoryViewModel(TransactionViewModel s)
            : this()
        {
            if (s == null)
                return;

            this.Name = s.Name;
            this.Guid = s.Guid;
            this.Time = s.Time;
            this.Operation = s.Operation;
            this.Certificate = s.Certificate;
            this.Url = s.Url;
            this.Headers = s.Headers;
            this.RequestContent = s.RequestContent;
            this.StatusCode = s.StatusCode;
            this.StatusDescription = s.StatusDescription;
            this.ResponseSize = s.ResponseSize;
            this.ResponseTime = s.ResponseTime;
            this.ResponseContent = s.ResponseContent;
            this.ResponseHeaders = s.ResponseHeaders;
            this.PreUrl = s.PreUrl;
            this.ValidationsScript = s.ValidationsScript;
            this.PreScript = s.PreScript;
            this.PostScript = s.PostScript;
            this.PreUrl = s.PreUrl;
            this.PreRequestContent = s.PreRequestContent;
            this.PreRequestHeaders = s.PreRequestHeaders;
            this.RequestContentType = s.RequestContentType;
            this.ResponseContentType = s.ResponseContentType;
            this.RequestParameters = s.RequestParameters;
            this.Validations = s.Validations;
            this.IsValidationSuccessFul = s.IsValidationSuccessFul;
        }

        public TransactionViewModel GetTransactionObject(bool includeResponse = true)
        {
            TransactionViewModel trans = new TransactionViewModel();
            trans.Name = this.Name;
            trans.Guid = this.Guid;
            trans.Certificate = this.Certificate;
            trans.Time = this.Time;
            trans.Operation = this.Operation;
            trans.Url = this.Url;
            trans.Headers = this.Headers;
            trans.ResponseSize = this.ResponseSize;
            trans.RequestContent = this.RequestContent;
            trans.StatusCode = this.StatusCode;
            trans.StatusDescription = this.StatusDescription;
            if (includeResponse)
            {
                trans.ResponseTime = this.ResponseTime;
                trans.ResponseContent = this.ResponseContent;
                trans.ResponseHeaders = this.ResponseHeaders;
            }
            trans.PreUrl = this.PreUrl;
            trans.ValidationsScript = this.ValidationsScript;
            trans.PreScript = this.PreScript;
            trans.PostScript = this.PostScript;
            trans.PreRequestContent = this.PreRequestContent;
            trans.PreRequestHeaders = this.PreRequestHeaders;
            trans.RequestParameters = this.RequestParameters;
            trans.RequestContentType = this.RequestContentType;
            trans.ResponseContentType = this.ResponseContentType;
            trans.Validations = this.Validations;
            trans.IsValidationSuccessFul = this.IsValidationSuccessFul;
            return trans;
        }

        private RelayCommand _sessionSelectedCommand;
        [JsonIgnore]
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

        private RelayCommand<string> _copySessionCommand;
        [JsonIgnore]
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

        private RelayCommand _removeSessionCommand;
        [JsonIgnore]
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

        private RelayCommand _saveRequestToMasterCommand;
        [JsonIgnore]
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

        private RelayCommand _saveRequestToFileCommand;
        [JsonIgnore]
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

        private RelayCommand<bool> _emailInBodyCommand;
        [JsonIgnore]
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

        private RelayCommand<bool> _emailAsAttachmentCommand;
        [JsonIgnore]
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

        [JsonIgnore]
        public WorkspaceViewModel MainViewModel { get; set; }

        private void RemoveSession()
        {
            MasterEventHandler.RaiseRemoveSessionEvent(this);
        }


        private void OnSessionSelected(object type)
        {

            if (MainViewModel != null)
            {
                MainViewModel.SessionSelected(this, type != null ? type.ToString() : null);
            }
        }

        private void SaveRequestToMaster()
        {
            this.MainViewModel.SaveRequestClicked(this.GetTransactionObject(false));
            MasterEventHandler.RaiseReloadSavedRequestEvent();
        }

        private void SaveRequestToFile()
        {
            this.MainViewModel.SaveRequestToFile(this.GetTransactionObject(false));
        }

        private void EmailInBody(bool includeResponse)
        {
            this.MainViewModel.EmailInBody(this.GetTransactionObject(includeResponse));
        }

        private void EmailAsAttachment(bool includeResponse)
        {
            this.MainViewModel.EmailAsAttachment(this.GetTransactionObject(false));
        }

        private void CopySession(string type)
        {
            try
            {
                switch (type.GetString().ToLower())
                {
                    case "url":
                        Clipboard.SetText(this.Url);
                        break;
                    case "request":
                        Clipboard.SetText(this.RequestContent);
                        break;
                    case "headers":
                        Clipboard.SetText(this.Headers);
                        break;
                    case "responseheaders":
                        Clipboard.SetText(this.ResponseHeaders);
                        break;
                    case "response":
                        Clipboard.SetText(this.ResponseContent);
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
    }
}
