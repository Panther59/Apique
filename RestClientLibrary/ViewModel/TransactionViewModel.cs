// <copyright file="TransactionViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>13-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using DataLibrary;
	using Newtonsoft.Json;
	using RestClientLibrary.Common;
    using RestClientLibrary.Model;
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the <see cref = "TransactionViewModel"/>
    /// </summary>
    [Serializable]
    public class TransactionViewModel : BaseViewModel
    {
		public TransactionViewModel()
		{
            if (string.IsNullOrEmpty(this.Guid))
            {
                this.Guid = System.Guid.NewGuid().ToString();   
            }
		}

        #region Fields

        /// <summary>
        /// The certificate field
        /// </summary>
        private string certificate;

        /// <summary>
        /// The isRenaming field
        /// </summary>
        private bool isRenaming;

        /// <summary>
        /// The name field
        /// </summary>
        private string name;

        /// <summary>
        /// The postScript field
        /// </summary>
        private string postScript;

        /// <summary>
        /// The preScript field
        /// </summary>
        private string preScript;

        /// <summary>
        /// The validationsScript field
        /// </summary>
        private string validationsScript;

        #region Commands

        /// <summary>
        /// Defines the _requestSelectedCommand
        /// </summary>
        private RelayCommand _requestSelectedCommand;

        /// <summary>
        /// The renameRequestCommand field
        /// </summary>
        private RelayCommand<bool> renameRequestCommand;

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the CategoryName
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the Certificate
        /// </summary>
        public string Certificate
        {
            get
            {
                return this.certificate;
            }

            set
            {
                this.certificate = value;
                this.OnPropertyChanged("Certificate");
            }
        }

        /// <summary>
        /// Gets or sets the Guid
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets the Headers
        /// </summary>
        public string Headers { get; set; }

        /// <summary>
        /// Gets or sets the IsCallSessessFul
        /// </summary>
        public bool? IsCallSessessFul { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsRenaming
        /// </summary>
        [JsonIgnore]
        public bool IsRenaming
        {
            get
            {
                return this.isRenaming;
            }

            set
            {
                this.isRenaming = value;
                this.OnPropertyChanged("IsRenaming");
            }
        }

        /// <summary>
        /// Gets or sets the IsValidationSuccessFul
        /// </summary>
        public bool? IsValidationSuccessFul { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets the Operation
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// Gets or sets the ParentViewModel
        /// </summary>
        [JsonIgnore]
        public WorkspaceViewModel ParentViewModel { get; set; }

        /// <summary>
        /// Gets or sets the PostScript
        /// </summary>
        public string PostScript
        {
            get
            {
                return this.postScript;
            }

            set
            {
                this.postScript = value;
                this.OnPropertyChanged("PostScript");
            }
        }

        /// <summary>
        /// Gets or sets PreRequestContent
        /// </summary>
        public string PreRequestContent { get; set; }

        /// <summary>
        /// Gets or sets PreRequestHeaders
        /// </summary>
        public string PreRequestHeaders { get; set; }

        /// <summary>
        /// Gets or sets the PreScript
        /// </summary>
        public string PreScript
        {
            get
            {
                return this.preScript;
            }

            set
            {
                this.preScript = value;
                this.OnPropertyChanged("PreScript");
            }
        }

        /// <summary>
        /// Gets or sets PreUrl
        /// </summary>
        public string PreUrl { get; set; }

        /// <summary>
        /// Gets or sets the RequestContent
        /// </summary>
        public string RequestContent { get; set; }

        /// <summary>
        /// Gets or sets RequestContentType
        /// </summary>
        public string RequestContentType { get; set; }

        /// <summary>
        /// Gets or sets the RequestParameters 
        /// </summary>
        public List<KeyValuePair<string, string>> RequestParameters { get; set; }

        /// <summary>
        /// Gets or sets the ResponseContent
        /// </summary>
        public string ResponseContent { get; set; }

        /// <summary>
        /// Gets or sets ResponseContentType
        /// </summary>
        public string ResponseContentType { get; set; }

        /// <summary>
        /// Gets or sets the ResponseHeaders
        /// </summary>
        public string ResponseHeaders { get; set; }

        /// <summary>
        /// Gets or sets the ResponseSize
        /// </summary>
        public long? ResponseSize { get; set; }

        /// <summary>
        /// This is a response time in Mili second
        /// </summary>
        /// <summary>
        /// Gets or sets the ResponseTime
        /// </summary>
        public int? ResponseTime { get; set; }

        /// <summary>
        /// Gets or sets the StatusCode
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the StatusDescription
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the Time
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the UrlHost
        /// </summary>
        public string UrlHost { get; set; }

        /// <summary>
        /// Gets or sets the UrlPath
        /// </summary>
        public string UrlPath { get; set; }

        /// <summary>
        /// Gets or sets the UrlScheme
        /// </summary>
        public string UrlScheme { get; set; }

        /// <summary>
        /// Gets or sets Validations
        /// </summary>
        public List<ValidationResultModel> Validations { get; set; }

        /// <summary>
        /// Gets or sets the ValidationsScript
        /// </summary>
        public string ValidationsScript
        {
            get
            {
                return this.validationsScript;
            }

            set
            {
                this.validationsScript = value;
                this.OnPropertyChanged("ValidationsScript");
            }
        }

        #region Commands

        /// <summary>
        /// Gets the RenameRequestCommand
        /// </summary>
        [JsonIgnore]
        public RelayCommand<bool> RenameRequestCommand
        {
            get
            {
                if (this.renameRequestCommand == null)
                {
                    this.renameRequestCommand = new RelayCommand<bool>(command => this.ExecuteRenameRequest(command));
                }

                return this.renameRequestCommand;
            }
        }

        /// <summary>
        /// Gets or sets the RequestSelectedCommand
        /// </summary>
        [JsonIgnore]
        public RelayCommand RequestSelectedCommand
        {
            get
            {
                if (_requestSelectedCommand == null)
                {
                    _requestSelectedCommand = new RelayCommand(command => RequestSelected());
                }

                return _requestSelectedCommand;
            }

            set
            {
                _requestSelectedCommand = value;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The ApplyVariables
        /// </summary>
        /// <param name = "variables">The <see cref = "List{KeyValueModel}"/></param>
        /// <returns>The <see cref = "TransactionViewModel"/></returns>
        public void ApplyVariables(List<KeyValueViewModel> variables)
        {
            this.Url = this.ReplaceVariables(this.PreUrl, variables);
            this.RequestContent = this.ReplaceVariables(this.PreRequestContent, variables);
            this.Headers = this.ReplaceVariables(this.PreRequestHeaders, variables);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Executes RenameRequest
        /// </summary>
        private void ExecuteRenameRequest(bool isRenaming)
        {
            try
            {
                this.IsRenaming = isRenaming;
                if (isRenaming == false)
                {
                    AppDataHelper.SaveRequestData(this);
                }
            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
        }

        /// <summary>
        /// The ReplaceVariables
        /// </summary>
        /// <param name = "input">The <see cref = "string "/></param>
        /// <param name = "variables">The <see cref = "List{KeyValueModel}"/></param>
        /// <returns>The <see cref = "string "/></returns>
        private string ReplaceVariables(string input, List<KeyValueViewModel> variables)
        {
            if (variables != null)
            {
                foreach (var variable in variables)
                {
                    input = input.GetString().Replace(string.Format("{{{{{0}}}}}", variable.Key), variable.Value);
                }
            }

            return input;
        }

        /// <summary>
        /// The RequestSelected
        /// </summary>
        private void RequestSelected()
        {
            ParentViewModel.RequestSelected(this);
        }

        #endregion

        #endregion
    }
}
