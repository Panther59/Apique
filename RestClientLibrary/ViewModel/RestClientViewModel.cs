// <copyright file="RestClientViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Security.Cryptography.X509Certificates;
	using System.Text;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Threading.Tasks;
	using DataLibrary;
	using Newtonsoft.Json;
	using RestClientLibrary.Common;
	using RestClientLibrary.Model;
	using RestClientLibrary.View;
	using RestClientLibrary.ViewModel.Automations;

	/// <summary>
	/// Defines the <see cref = "RestClientViewModel"/>
	/// </summary>
	public class RestClientViewModel : BaseViewModel
	{
		#region Fields

		/// <summary>
		/// Defines the _headersBase
		/// </summary>
		private HeadersViewModel _headersBase;

		/// <summary>
		/// Defines the _isCallSuccessFul
		/// </summary>
		private bool _isCallSuccessFul;

		/// <summary>
		/// Defines the _isJsonResponse
		/// </summary>
		private bool _isJsonResponse;

		/// <summary>
		/// Defines the _isRequestContentVisible
		/// </summary>
		private bool _isRequestContentVisible;

		/// <summary>
		/// Defines the _isResponseVisible
		/// </summary>
		private bool _isResponseVisible;

		/// <summary>
		/// Defines the _isXmlResponse
		/// </summary>
		private bool _isXmlResponse;

		/// <summary>
		/// Defines the _jsonResponseContent
		/// </summary>
		private string _jsonResponseContent;

		/// <summary>
		/// Defines the _operations
		/// </summary>
		private List<TextType> _operations;

		/// <summary>
		/// Defines the _rawResponseContent
		/// </summary>
		private string _rawResponseContent;

		/// <summary>
		/// Defines the _requestContent
		/// </summary>
		private string _requestContent;

		/// <summary>
		/// Defines the _responseHeaders
		/// </summary>
		private string _responseHeaders;

		/// <summary>
		/// Defines the _responseMessage
		/// </summary>
		private string _responseMessage;

		/// <summary>
		/// Defines the _selectedOperation
		/// </summary>
		private TextType _selectedOperation;

		/// <summary>
		/// Defines the _selectedResponseTab
		/// </summary>
		private int _selectedResponseTab;

		/// <summary>
		/// Defines the _sendButtonText
		/// </summary>
		private string _sendButtonText = "Send";

		/// <summary>
		/// Defines the _urlBase
		/// </summary>
		private UriViewModel _urlBase;

		/// <summary>
		/// Defines the _view
		/// </summary>
		private View.IRestClientView _view;

		/// <summary>
		/// Defines the _xmlResponseContent
		/// </summary>
		private string _xmlResponseContent;

		/// <summary>
		/// The certificate field
		/// </summary>
		private CertificateViewModel certificate;

		/// <summary>
		/// The collapseAllFoldings field
		/// </summary>
		private Action collapseAllFoldings;

		/// <summary>
		/// The expandAllFoldings field
		/// </summary>
		private Action expandAllFoldings;

		/// <summary>
		/// The htmlResponseContent field
		/// </summary>
		private string htmlResponseContent;

		/// <summary>
		/// The isHtmlResponse field
		/// </summary>
		private bool isHtmlResponse;

		/// <summary>
		/// The isRawContentType field
		/// </summary>
		private bool isRawContentType;

		/// <summary>
		/// The isRenaming field
		/// </summary>
		private bool isRenaming;

		/// <summary>
		/// The isSavedRequest field
		/// </summary>
		private bool isSavedRequest;

		/// <summary>
		/// The isValidationSuccessful field
		/// </summary>
		private bool? isValidationSuccessful;

		/// <summary>
		/// The postScript field
		/// </summary>
		private RuntimeCodeViewModel postScript;

		/// <summary>
		/// The preScript field
		/// </summary>
		private RuntimeCodeViewModel preScript;

		/// <summary>
		/// The rawContentTypes field
		/// </summary>
		private List<TextType> rawContentTypes;

		/// <summary>
		/// The requestContentCategories field
		/// </summary>
		private List<RequestContentTypeCategoryViewModel> requestContentCategories;

		/// <summary>
		/// The requestHighlightType field
		/// </summary>
		private string requestHighlightType = "Json";

		/// <summary>
		/// The requestParameters field
		/// </summary>
		private KeyValuePairsViewModel requestParameters;

		/// <summary>
		/// Defines the requestResponseDataThreadCancellationSource
		/// </summary>
		private CancellationTokenSource requestResponseDataThreadCancellationSource;

		/// <summary>
		/// Defines the requestResponseHeaderThreadCancellationToken
		/// </summary>
		private CancellationTokenSource requestResponseHeaderThreadCancellationSource;

		/// <summary>
		/// Defines the requestThreadCancellationToken
		/// </summary>
		private CancellationTokenSource requestThreadCancellationSource;

		/// <summary>
		/// The selectedRawContentType field
		/// </summary>
		private TextType selectedRawContentType;

		/// <summary>
		/// The selectedRequestContentCategory field
		/// </summary>
		private RequestContentTypeCategoryViewModel selectedRequestContentCategory;

		/// <summary>
		/// The selectedText field
		/// </summary>
		private string selectedText;

		/// <summary>
		/// The summaryText field
		/// </summary>
		private string summaryText;

		/// <summary>
		/// The title field
		/// </summary>
		private string title;

		/// <summary>
		/// The validations field
		/// </summary>
		private List<ValidationViewModel> validations;

		/// <summary>
		/// The validationsScript field
		/// </summary>
		private RuntimeCodeViewModel validationsScript;

		#region Commands

		/// <summary>
		/// Defines the _certificateChangedCommand
		/// </summary>
		private RelayCommand _certificateChangedCommand;

		/// <summary>
		/// Defines the _clearButtonClickedCommand
		/// </summary>
		private RelayCommand _clearButtonClickedCommand;

		/// <summary>
		/// Defines the _collapseAllJsonNodeCommand
		/// </summary>
		private RelayCommand _collapseAllJsonNodeCommand;

		/// <summary>
		/// Defines the _copyJsonDataToClipboardCommand
		/// </summary>
		private RelayCommand _copyJsonDataToClipboardCommand;

		/// <summary>
		/// Defines the _expandAllJsonNodeCommand
		/// </summary>
		private RelayCommand _expandAllJsonNodeCommand;

		/// <summary>
		/// Defines the _operationChangedCommand
		/// </summary>
		private RelayCommand _operationChangedCommand;

		/// <summary>
		/// Defines the _sendButtonClickedCommand
		/// </summary>
		private RelayCommand _sendButtonClickedCommand;

		/// <summary>
		/// The closeRestClientCommand field
		/// </summary>
		private RelayCommand closeRestClientCommand;

		/// <summary>
		/// The expandCollapseAllCommand field
		/// </summary>
		private RelayCommand<bool> expandCollapseAllCommand;

		/// <summary>
		/// The renameTitleCommand field
		/// </summary>
		private RelayCommand<bool> renameTitleCommand;

		/// <summary>
		/// The requestContentCategoriesChangedCommand field
		/// </summary>
		private RelayCommand requestContentCategoriesChangedCommand;

		/// <summary>
		/// The requestContentTypeChangedCommand field
		/// </summary>
		private RelayCommand requestContentTypeChangedCommand;

		#endregion

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref = "RestClientViewModel"/> class.
		/// </summary>
		/// <param name = "view">The <see cref = "View.IRestClientView"/></param>
		public RestClientViewModel()
		{
			LoadData();
		}

		#endregion

		#region Delegates

		/// <summary>
		/// The OnResposeReceivedHandler
		/// </summary>
		/// <param name="restresponse">The <see cref="RestResponse"/></param>
		public delegate void OnResposeReceivedHandler(RestResponse restresponse);

		#endregion

		#region Events

		/// <summary>
		/// Defines the ResposeReceived
		/// </summary>
		public event OnResposeReceivedHandler ResposeReceived;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the Certificate
		/// </summary>
		public CertificateViewModel Certificate
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
		/// Gets or sets the CollapseAllFoldings
		/// </summary>
		public Action CollapseAllFoldings
		{
			get
			{
				return this.collapseAllFoldings;
			}

			set
			{
				this.collapseAllFoldings = value;
				this.OnPropertyChanged("CollapseAllFoldings");
			}
		}

		/// <summary>
		/// Gets or sets the ExpandAllFoldings
		/// </summary>
		public Action ExpandAllFoldings
		{
			get
			{
				return this.expandAllFoldings;
			}

			set
			{
				this.expandAllFoldings = value;
				this.OnPropertyChanged("ExpandAllFoldings");
			}
		}

		/// <summary>
		/// Gets or sets GUID
		/// </summary>
		public string GUID { get; set; }

		/// <summary>
		/// Gets or sets the HeadersBase
		/// </summary>
		public HeadersViewModel HeadersBase
		{
			get
			{
				return _headersBase;
			}

			set
			{
				_headersBase = value;
				OnPropertyChanged("HeadersBase");
			}
		}

		/// <summary>
		/// Gets or sets the HtmlResponseContent
		/// </summary>
		public string HtmlResponseContent
		{
			get
			{
				return this.htmlResponseContent;
			}

			set
			{
				this.htmlResponseContent = value;
				this.OnPropertyChanged("HtmlResponseContent");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether IsCallSuccessFul
		/// </summary>
		public bool IsCallSuccessFul
		{
			get
			{
				return _isCallSuccessFul;
			}

			set
			{
				_isCallSuccessFul = value;
				OnPropertyChanged("IsCallSuccessFul");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether IsDefaultRestClient
		/// </summary>
		public bool IsDefaultRestClient { get; set; }

		/// <summary>
		/// Gets or sets the IsHtmlResponse
		/// </summary>
		public bool IsHtmlResponse
		{
			get
			{
				return this.isHtmlResponse;
			}

			set
			{
				this.isHtmlResponse = value;
				this.OnPropertyChanged("IsHtmlResponse");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether IsJsonResponse
		/// </summary>
		public bool IsJsonResponse
		{
			get
			{
				return _isJsonResponse;
			}

			set
			{
				_isJsonResponse = value;
				OnPropertyChanged("IsJsonResponse");
			}
		}

		/// <summary>
		/// Gets or sets the IsRawContentType
		/// </summary>
		public bool IsRawContentType
		{
			get
			{
				return this.isRawContentType;
			}

			set
			{
				this.isRawContentType = value;
				this.OnPropertyChanged("IsRawContentType");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether IsRenaming
		/// </summary>
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
		/// Gets or sets a value indicating whether IsRequestContentVisible
		/// </summary>
		public bool IsRequestContentVisible
		{
			get
			{
				return _isRequestContentVisible;
			}

			set
			{
				_isRequestContentVisible = value;
				OnPropertyChanged("IsRequestContentVisible");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether IsResponseVisible
		/// </summary>
		public bool IsResponseVisible
		{
			get
			{
				return _isResponseVisible;
			}

			set
			{
				_isResponseVisible = value;
				OnPropertyChanged("IsResponseVisible");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether IsSavedRequest
		/// </summary>
		public bool IsSavedRequest
		{
			get
			{
				return this.isSavedRequest;
			}

			set
			{
				this.isSavedRequest = value;
				this.OnPropertyChanged("IsSavedRequest");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether IsValidationSuccessful
		/// </summary>
		public bool? IsValidationSuccessful
		{
			get
			{
				return this.isValidationSuccessful;
			}

			set
			{
				this.isValidationSuccessful = value;
				this.OnPropertyChanged("IsValidationSuccessful");
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether IsXmlResponse
		/// </summary>
		public bool IsXmlResponse
		{
			get
			{
				return _isXmlResponse;
			}

			set
			{
				_isXmlResponse = value;
				OnPropertyChanged("IsXmlResponse");
			}
		}

		/// <summary>
		/// Gets or sets the JsonResponseContent
		/// </summary>
		public string JsonResponseContent
		{
			get
			{
				return _jsonResponseContent;
			}

			set
			{
				_jsonResponseContent = value;
				OnPropertyChanged("JsonResponseContent");
			}
		}

		/// <summary>
		/// Gets or sets the Operations
		/// </summary>
		public List<TextType> Operations
		{
			get
			{
				return _operations;
			}

			set
			{
				_operations = value;
				OnPropertyChanged("Operations");
			}
		}

		/// <summary>
		/// Gets or sets the ParentViewModel
		/// </summary>
		public WorkspaceViewModel ParentViewModel { get; set; }

		/// <summary>
		/// Gets or sets the PostScript
		/// </summary>
		public RuntimeCodeViewModel PostScript
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
		/// Gets or sets the PreScript
		/// </summary>
		public RuntimeCodeViewModel PreScript
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
		/// Gets or sets the RawContentTypes
		/// </summary>
		public List<TextType> RawContentTypes
		{
			get
			{
				return this.rawContentTypes;
			}

			set
			{
				this.rawContentTypes = value;
				this.OnPropertyChanged("RawContentTypes");
			}
		}

		/// <summary>
		/// Gets or sets the RawResponseContent
		/// </summary>
		public string RawResponseContent
		{
			get
			{
				return _rawResponseContent;
			}

			set
			{
				_rawResponseContent = value;
				OnPropertyChanged("RawResponseContent");
			}
		}

		/// <summary>
		/// Gets or sets the RequestContent
		/// </summary>
		public string RequestContent
		{
			get
			{
				return _requestContent;
			}

			set
			{
				_requestContent = value;
				OnPropertyChanged("RequestContent");
			}
		}

		/// <summary>
		/// Gets or sets the RequestContentCategories
		/// </summary>
		public List<RequestContentTypeCategoryViewModel> RequestContentCategories
		{
			get
			{
				return this.requestContentCategories;
			}

			set
			{
				this.requestContentCategories = value;
				this.OnPropertyChanged("RequestContentCategories");
			}
		}

		/// <summary>
		/// Gets or sets the request content type
		/// </summary>
		public string RequestContentType
		{
			get
			{
				if (SelectedRequestContentCategory != null)
				{
					if (SelectedRequestContentCategory.Name == "Raw")
					{
						return SelectedRawContentType.Text;
					}
					else
					{
						return SelectedRequestContentCategory.Value;
					}
				}
				else
				{
					return "application/json";
				}
			}

			set
			{
				if (this.RequestContentCategories.Any(x => x.Value.Equals(value, StringComparison.CurrentCultureIgnoreCase)))
				{
					this.SelectedRequestContentCategory = this.RequestContentCategories.First(x => x.Value.Equals(value, StringComparison.CurrentCultureIgnoreCase));
				}
				else
				{
					this.SelectedRequestContentCategory = this.RequestContentCategories.First(x => x.Value == "Raw");
					if (this.RawContentTypes.Any(x => x.Text.Equals(value, StringComparison.CurrentCultureIgnoreCase)))
					{
						this.SelectedRawContentType = this.RawContentTypes.First(x => x.Text.Equals(value, StringComparison.CurrentCultureIgnoreCase));
					}
					else
					{
						this.SelectedRawContentType = this.RawContentTypes.First(x => x.Text.Equals("application/json", StringComparison.CurrentCultureIgnoreCase));
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the RequestHighlightType
		/// </summary>
		public string RequestHighlightType
		{
			get
			{
				return this.requestHighlightType;
			}

			set
			{
				this.requestHighlightType = value;
				this.OnPropertyChanged("RequestHighlightType");
			}
		}

		/// <summary>
		/// Gets or sets the RequestParameters
		/// </summary>
		public KeyValuePairsViewModel RequestParameters
		{
			get
			{
				return this.requestParameters;
			}

			set
			{
				this.requestParameters = value;
				this.OnPropertyChanged("RequestParameters");
			}
		}

		/// <summary>
		/// Gets or sets the ResponseHeaders
		/// </summary>
		public string ResponseHeaders
		{
			get
			{
				return _responseHeaders;
			}

			set
			{
				_responseHeaders = value;
				OnPropertyChanged("ResponseHeaders");
			}
		}

		/// <summary>
		/// Gets or sets the ResponseInterval
		/// </summary>
		public int? ResponseInterval { get; set; }

		/// <summary>
		/// Gets or sets the ResponseMessage
		/// </summary>
		public string ResponseMessage
		{
			get
			{
				return _responseMessage;
			}

			set
			{
				_responseMessage = value;
				OnPropertyChanged("ResponseMessage");
			}
		}

		/// <summary>
		/// Gets or sets the ResponseStatusCode
		/// </summary>
		public int? ResponseStatusCode { get; set; }

		/// <summary>
		/// Gets or sets the ResponseStatusDescription
		/// </summary>
		public string ResponseStatusDescription { get; set; }

		/// <summary>
		/// Gets or sets the SelectedOperation
		/// </summary>
		public TextType SelectedOperation
		{
			get
			{
				return _selectedOperation;
			}

			set
			{
				_selectedOperation = value;
				OnPropertyChanged("SelectedOperation");
			}
		}

		/// <summary>
		/// Gets or sets the SelectedRawContentType
		/// </summary>
		public TextType SelectedRawContentType
		{
			get
			{
				return this.selectedRawContentType;
			}

			set
			{
				this.selectedRawContentType = value;
				this.OnPropertyChanged("SelectedRawContentType");
			}
		}

		/// <summary>
		/// Gets or sets the SelectedRequestContentCategory
		/// </summary>
		public RequestContentTypeCategoryViewModel SelectedRequestContentCategory
		{
			get
			{
				return this.selectedRequestContentCategory;
			}

			set
			{
				this.selectedRequestContentCategory = value;
				this.OnPropertyChanged("SelectedRequestContentCategory");
			}
		}

		/// <summary>
		/// Gets or sets the SelectedResponseTab
		/// </summary>
		public int SelectedResponseTab
		{
			get
			{
				return _selectedResponseTab;
			}

			set
			{
				_selectedResponseTab = value;
				OnPropertyChanged("SelectedResponseTab");
			}
		}

		/// <summary>
		/// Gets or sets the SelectedText
		/// </summary>
		public string SelectedText
		{
			get
			{
				return this.selectedText;
			}

			set
			{
				this.selectedText = value;
				this.OnPropertyChanged("SelectedText");
			}
		}

		/// <summary>
		/// Gets or sets the SendButtonText
		/// </summary>
		public string SendButtonText
		{
			get
			{
				return _sendButtonText;
			}

			set
			{
				_sendButtonText = value;
				OnPropertyChanged("SendButtonText");
			}
		}

		/// <summary>
		/// Gets or sets the SummaryText
		/// </summary>
		public string SummaryText
		{
			get
			{
				return this.summaryText;
			}

			set
			{
				this.summaryText = value;
				this.OnPropertyChanged("SummaryText");
			}
		}

		/// <summary>
		/// Gets or sets the Title
		/// </summary>
		public string Title
		{
			get
			{
				return this.title;
			}

			set
			{
				this.title = value;
				this.OnPropertyChanged("Title");
			}
		}

		/// <summary>
		/// Gets or sets the UrlBase
		/// </summary>
		public UriViewModel UrlBase
		{
			get
			{
				return _urlBase;
			}

			set
			{
				_urlBase = value;
				OnPropertyChanged("UrlBase");
			}
		}

		/// <summary>
		/// Gets or sets the Validations
		/// </summary>
		public List<ValidationViewModel> Validations
		{
			get
			{
				return this.validations;
			}

			set
			{
				this.validations = value;
				this.OnPropertyChanged("Validations");
			}
		}

		/// <summary>
		/// Gets or sets the ValidationsScript
		/// </summary>
		public RuntimeCodeViewModel ValidationsScript
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

		/// <summary>
		/// Gets or sets the XmlResponseContent
		/// </summary>
		public string XmlResponseContent
		{
			get
			{
				return _xmlResponseContent;
			}

			set
			{
				_xmlResponseContent = value;
				OnPropertyChanged("XmlResponseContent");
			}
		}

		#region Commands

		/// <summary>
		/// Gets or sets the ClearButtonClickedCommand
		/// </summary>
		public RelayCommand ClearButtonClickedCommand
		{
			get
			{
				if (_clearButtonClickedCommand == null)
				{
					_clearButtonClickedCommand = new RelayCommand(command => ClearButtonClicked());
				}

				return _clearButtonClickedCommand;
			}

			set
			{
				_clearButtonClickedCommand = value;
			}
		}

		/// <summary>
		/// Gets or sets the CollapseAllJsonNodeCommand
		/// </summary>
		public RelayCommand CollapseAllJsonNodeCommand
		{
			get
			{
				if (_collapseAllJsonNodeCommand == null)
				{
					_collapseAllJsonNodeCommand = new RelayCommand(command => CollapseAllJsonNode());
				}

				return _collapseAllJsonNodeCommand;
			}

			set
			{
				_collapseAllJsonNodeCommand = value;
			}
		}

		/// <summary>
		/// Gets or sets the CopyJsonDataToClipboardCommand
		/// </summary>
		public RelayCommand CopyJsonDataToClipboardCommand
		{
			get
			{
				if (_copyJsonDataToClipboardCommand == null)
				{
					_copyJsonDataToClipboardCommand = new RelayCommand(command => CopyJsonDataToClipboard());
				}

				return _copyJsonDataToClipboardCommand;
			}

			set
			{
				_copyJsonDataToClipboardCommand = value;
			}
		}

		/// <summary>
		/// Gets or sets the ExpandAllJsonNodeCommand
		/// </summary>
		public RelayCommand ExpandAllJsonNodeCommand
		{
			get
			{
				if (_expandAllJsonNodeCommand == null)
				{
					_expandAllJsonNodeCommand = new RelayCommand(command => ExpandAllJsonNode());
				}

				return _expandAllJsonNodeCommand;
			}

			set
			{
				_expandAllJsonNodeCommand = value;
			}
		}

		/// <summary>
		/// Gets the ExpandCollapseAllCommand
		/// </summary>
		public RelayCommand<bool> ExpandCollapseAllCommand
		{
			get
			{
				if (this.expandCollapseAllCommand == null)
				{
					this.expandCollapseAllCommand = new RelayCommand<bool>(command => this.ExecuteExpandCollapseAll(command), can => this.CanExpandCollapseAllExecute());
				}

				return this.expandCollapseAllCommand;
			}
		}

		/// <summary>
		/// Gets or sets the OperationChangedCommand
		/// </summary>
		public RelayCommand OperationChangedCommand
		{
			get
			{
				if (_operationChangedCommand == null)
				{
					_operationChangedCommand = new RelayCommand(command => OperationChanged());
				}

				return _operationChangedCommand;
			}

			set
			{
				_operationChangedCommand = value;
			}
		}

		/// <summary>
		/// Gets the RenameTitleCommand
		/// </summary>
		public RelayCommand<bool> RenameTitleCommand
		{
			get
			{
				if (this.renameTitleCommand == null)
				{
					this.renameTitleCommand = new RelayCommand<bool>(command => this.ExecuteRenameTitle(command));
				}

				return this.renameTitleCommand;
			}
		}

		/// <summary>
		/// Gets the RequestContentCategoriesChangedCommand
		/// </summary>
		public RelayCommand RequestContentCategoriesChangedCommand
		{
			get
			{
				if (this.requestContentCategoriesChangedCommand == null)
				{
					this.requestContentCategoriesChangedCommand = new RelayCommand(command => this.ExecuteRequestContentCategoriesChanged());
				}

				return this.requestContentCategoriesChangedCommand;
			}
		}

		/// <summary>
		/// Gets the RequestContentTypeChangedCommand
		/// </summary>
		public RelayCommand RequestContentTypeChangedCommand
		{
			get
			{
				if (this.requestContentTypeChangedCommand == null)
				{
					this.requestContentTypeChangedCommand = new RelayCommand(command => this.ExecuteRequestContentTypeChanged());
				}

				return this.requestContentTypeChangedCommand;
			}
		}

		/// <summary>
		/// Gets or sets the SendButtonClickedCommand
		/// </summary>
		public RelayCommand SendButtonClickedCommand
		{
			get
			{
				if (_sendButtonClickedCommand == null)
				{
					_sendButtonClickedCommand = new RelayCommand(command => SendButtonClicked(), can => CanSaveButtonClicked());
				}

				return _sendButtonClickedCommand;
			}

			set
			{
				_sendButtonClickedCommand = value;
			}
		}

		public void SelectCertificate(string certName)
		{
			this.Certificate = this.ParentViewModel.Settings.Certificates.FirstOrDefault(x => x.Name == certName);
		}

		#endregion

		#endregion

		#region Methods

		#region Public Methods

		/// <summary>
		/// The AttachView
		/// </summary>
		/// <param name="view">The <see cref="IRestClientView"/></param>
		public void AttachView(IRestClientView view)
		{
			this._view = view;
		}

		/// <summary>
		/// The BuildRequest
		/// </summary>
		/// <param name = "variables">The <see cref = "List{KeyValueModel}"/></param>
		/// <param name = "name">The <see cref = "string "/></param>
		/// <returns>The <see cref = "TransactionViewModel"/></returns>
		public TransactionViewModel BuildRequest(string name = null)
		{
			Uri testUri;
			bool suc = Uri.TryCreate(UrlBase.Url, UriKind.Absolute, out testUri);
			return new TransactionViewModel()
			{
				ParentViewModel = this.ParentViewModel,
				Name = name,
				Guid = this.GUID,
				Certificate = this.Certificate?.Name,
				RequestContentType = this.RequestContentType,
				PreRequestHeaders = HeadersBase.GetHeaderContent(),
				Operation = SelectedOperation.Text,
				PreRequestContent = RequestContent,
				Time = DateTime.Now,
				PreUrl = UrlBase.Url,
				RequestParameters = this.RequestParameters.GetItems(),
				UrlScheme = suc ? testUri.Scheme : null,
				UrlHost = suc ? testUri.Host : null,
				UrlPath = suc ? testUri.PathAndQuery : null,
				PreScript = this.PreScript.EditableCode,
				PostScript = this.PostScript.EditableCode,
				ValidationsScript = this.ValidationsScript.EditableCode,
			};
		}

		/// <summary>
		/// The BuildRequestWithResponse for sending mail
		/// </summary>
		/// <param name = "name">The <see cref = "string "/></param>
		/// <returns>The <see cref = "TransactionViewModel"/></returns>
		public TransactionViewModel BuildRequestWithResponse(string name = null)
		{
			Uri testUri;
			bool suc = Uri.TryCreate(UrlBase.Url, UriKind.Absolute, out testUri);
			return new TransactionViewModel()
			{
				ParentViewModel = this.ParentViewModel,
				Name = name,
				Guid = Guid.NewGuid().ToString(),
				Headers = HeadersBase.GetHeaderContent(),
				Operation = SelectedOperation.Text,
				RequestContent = RequestContent,
				Time = DateTime.Now,
				Url = UrlBase.Url,
				UrlScheme = suc ? testUri.Scheme : null,
				UrlHost = suc ? testUri.Host : null,
				UrlPath = suc ? testUri.PathAndQuery : null,
				ResponseContent = this.RawResponseContent,
				ResponseHeaders = this.ResponseHeaders,
				ResponseTime = this.ResponseInterval,
				StatusCode = this.ResponseStatusCode,
				StatusDescription = this.ResponseStatusDescription
			};
		}

		/// <summary>
		/// The ClearResposeReceivedEvent
		/// </summary>
		public void ClearResposeReceivedEvent()
		{
			if (ResposeReceived != null)
			{
				foreach (Delegate d in ResposeReceived.GetInvocationList())
				{
					ResposeReceived -= (OnResposeReceivedHandler)d;
				}
			}
		}

		/// <summary>
		/// The ConvertToRawFormat
		/// </summary>
		/// <param name = "input">The <see cref = "string "/></param>
		/// <returns>The <see cref = "string "/></returns>
		public string ConvertToRawFormat(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return Constants.NoResponseContent;
			}
			else
			{
				return input;
			}
		}

		/// <summary>
		/// The GetRequestResponseText
		/// </summary>
		/// <param name = "transaction">The <see cref = "TransactionViewModel"/></param>
		/// <returns>The <see cref = "string "/></returns>
		public string GetRequestResponseText(TransactionViewModel transaction)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(string.Format("{0} {1}", transaction.Operation, transaction.Url));
			if (string.IsNullOrEmpty(transaction.Headers) == false)
			{
				sb.AppendLine(transaction.Headers);
			}

			if (string.IsNullOrEmpty(transaction.RequestContent) == false)
			{
				sb.AppendLine();
				sb.AppendLine(transaction.RequestContent);
			}

			if (transaction.StatusCode.HasValue)
			{
				sb.AppendLine();
				sb.AppendLine(string.Format("Result - {0} {1}", transaction.StatusCode.Value, transaction.StatusDescription));
				if (string.IsNullOrEmpty(transaction.ResponseHeaders) == false)
				{
					sb.AppendLine(transaction.ResponseHeaders.Trim());
				}

				sb.AppendLine();
				sb.AppendLine(transaction.ResponseContent);
			}

			return sb.ToString();
		}

		/// <summary>
		/// The GetVariablesForSuggestion
		/// </summary>
		/// <returns>The <see cref="IEnumerable{MyCompletionData}"/></returns>
		public IEnumerable<MyCompletionData> GetVariablesForSuggestion()
		{
			var variables = RaiseGetRelatedVariables();
			var list = variables?.Select(x => new MyCompletionData(x.Key) { Description = x.Value });

			return list;
		}

		/// <summary>
		/// The HasCompleteRequestData
		/// </summary>
		/// <returns>The <see cref = "bool "/></returns>
		public bool HasCompleteRequestData()
		{
			if (string.IsNullOrEmpty(UrlBase.Url) == false && (SelectedOperation.Text == "GET" || (SelectedOperation.Text != "GET" && string.IsNullOrEmpty(RequestContent) == false)))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// The LoadData
		/// </summary>
		public void LoadData()
		{
			this.GUID = Guid.NewGuid().ToString();

			this.UrlBase = new UriViewModel();
			this.HeadersBase = new HeadersViewModel();
			this.HeadersBase.LoadData();
			this.RequestParameters = new KeyValuePairsViewModel();
			List<TextType> ops = new List<TextType>();
			ops.Add(new TextType()
			{ Text = Constants.GET });
			ops.Add(new TextType()
			{ Text = Constants.POST });
			ops.Add(new TextType()
			{ Text = Constants.PUT });
			ops.Add(new TextType()
			{ Text = Constants.DELETE });
			Operations = ops;
			SelectedOperation = ops[0];
			List<RequestContentTypeCategoryViewModel> requestContentCategories = new List<RequestContentTypeCategoryViewModel>()
			{
				new RequestContentTypeCategoryViewModel
				{
					Name = "Raw",
					Value = "Raw"
				},
				new RequestContentTypeCategoryViewModel
				{
					Name = "form-data",
					Value = "multipart/form-data"
				},
				new RequestContentTypeCategoryViewModel
				{
					Name = "x-www-form-urlencoded",
					Value = "application/x-www-form-urlencoded"
				},
			};
			this.RequestContentCategories = requestContentCategories;
			this.SelectedRequestContentCategory = requestContentCategories.First();
			List<TextType> rawContentTypes = new List<TextType>()
			{
				new TextType{Text = "application/json"},
				new TextType{Text = "application/xml"},
				new TextType{Text = "text/plain"},
				new TextType{Text = "application/javascript"},
				new TextType{Text = "text/xml"},
				new TextType{Text = "text/html"},
			};
			this.RawContentTypes = rawContentTypes;
			this.SelectedRawContentType = this.RawContentTypes.First();
			this.IsRawContentType = true;

			this.PreScript = new RuntimeCodeViewModel();
			this.PreScript.LoadCode(Constants.PreAutomationScriptStartCode, Constants.AutomationScriptIndentation, Constants.AutomationScriptEndCode);

			this.PostScript = new RuntimeCodeViewModel();
			this.PostScript.LoadCode(Constants.PostAutomationScriptStartCode, Constants.AutomationScriptIndentation, Constants.AutomationScriptEndCode);

			this.ValidationsScript = new RuntimeCodeViewModel();
			this.ValidationsScript.LoadCode(Constants.ValidationScriptStartCode, Constants.AutomationScriptIndentation, Constants.AutomationScriptEndCode);

			List<SnippetViewModel> preSnippets = new List<SnippetViewModel>();
			preSnippets.Add(new SnippetViewModel { Name = "Clear Global Variable", Code = "restClient.ClearGlobalVariable(\"variableName\");" });
			preSnippets.Add(new SnippetViewModel { Name = "Set Global Variable", Code = "restClient.SetGlobalVariable(\"variableName\", \"variableValue\");" });
			preSnippets.Add(new SnippetViewModel { Name = "Clear Environment Variable", Code = "restClient.ClearEnvironmentVariable(\"variableName\");" });
			preSnippets.Add(new SnippetViewModel { Name = "Set Environment Variable", Code = "restClient.SetEnvironmentVariable(\"variableName\", \"variableValue\");" });
			preSnippets.Add(new SnippetViewModel { Name = "Get Variables List", Code = "List<KeyValuePair<string, string>> variables = restClient.GetVariablesList();" });
			preSnippets.Add(new SnippetViewModel { Name = "Get Variable Value", Code = "string variableValue = restClient.GetVariableValue(\"variableName\");" });
			this.PreScript.Snippets = preSnippets;

			List<SnippetViewModel> postSnippets = new List<SnippetViewModel>();
			postSnippets.Add(new SnippetViewModel { Name = "Get Response Status Code", Code = "int statusCode = restClient.GetResponseStatusCode();" });
			postSnippets.Add(new SnippetViewModel { Name = "Get Response Status Description", Code = "string statusDesc = restClient.GetResponseStatusDescription();" });
			postSnippets.Add(new SnippetViewModel { Name = "Get Response Property Value", Code = "string value = restClient.GetResponsePropertyValue(\"propertyPath\");" });
			postSnippets.Add(new SnippetViewModel { Name = "Check if service call successful", Code = "bool isSuccess = restClient.IsCallSuccessful();" });
			postSnippets.Add(new SnippetViewModel { Name = "GetResponseContent", Code = "string content = restClient.GetResponseContent();" });
			postSnippets.Add(new SnippetViewModel { Name = "Get Resonse Headers", Code = "List<KeyValuePair<string, string>> headers = restClient.GetResonseHeaders();" });
			postSnippets.Add(new SnippetViewModel { Name = "Get Response Time", Code = "int time = restClient.GetResponseTime();" });
			postSnippets.Add(new SnippetViewModel { Name = "Get Response Size", Code = "long? size = restClient.GetResponseSize();" });
			postSnippets.AddRange(preSnippets);
			this.PostScript.Snippets = postSnippets;

			List<SnippetViewModel> validationSnippets = new List<SnippetViewModel>();
			validationSnippets.Add(new SnippetViewModel { Name = "Validate", Code = "bool result = restClient.Validate(\"Test Name\", \"expectedValue\", \"actualValue\", CompareMode.Contains);" });
			validationSnippets.Add(new SnippetViewModel { Name = "Validate Conditions", Code = "bool result = restClient.ValidateCondition(\"Test Name\", <condition>);" });
			validationSnippets.Add(new SnippetViewModel { Name = "Validate Header", Code = "bool result = restClient.ValidateHeader(\"Test Name\", \"headerKey\", \"headerValue\", CompareMode.Contains);" });
			validationSnippets.Add(new SnippetViewModel { Name = "Validate Response Content", Code = "bool result = restClient.ValidateResponseContent(\"Test Name\", \"expectedContent\");" });
			validationSnippets.Add(new SnippetViewModel { Name = "Validate Response Field", Code = "bool result = restClient.ValidateResponseField(\"Test Name\", \"fieldPath\", \"expectedValue\", CompareMode.Contains);" });
			validationSnippets.Add(new SnippetViewModel { Name = "Validate Status Code", Code = "bool result = restClient.ValidateStatusCode(\"Test Name\", 200);" });
			validationSnippets.AddRange(postSnippets);
			this.ValidationsScript.Snippets = validationSnippets;
		}

		/// <summary>
		/// Load session data
		/// </summary>
		/// <param name = "session">Complete session data</param>
		/// <param name = "section">This is used to pass as NULL if complete data needs to load
		/// but if we need to load only part of session then pass different values like URL, Headers....</param>
		public void LoadSession(SessionHistoryViewModel session, string section)
		{
			Thread sessionLoadThread = new Thread(() =>
			{
				if (session == null)
					return;

				IsBusy = true;
				this.GUID = session.Guid;
				if (string.IsNullOrWhiteSpace(session.Name) == false)
				{
					this.Title = session.Name;
					this.IsSavedRequest = true;
				}

				if (section == null)
				{
					SelectedOperation = Operations.FirstOrDefault(x => x.Text == session.Operation);
					this.OperationChanged();
				}

				if (section == null || section.GetString().ToUpper() == "CERTIFICATE")
				{
					this.Certificate = this.ParentViewModel?.Settings.Certificates?.FirstOrDefault(x => x.Name == session.Certificate);
				}

				if (section == null || section.GetString().ToUpper() == "URL")
				{
					UrlBase.Url = session.PreUrl ?? session.Url;
				}

				if (section == null || section.GetString().ToUpper() == "HEADERS")
				{
					HeadersBase.SetHeaders(session.PreRequestHeaders ?? session.Headers);
				}

				if (section == null || section.GetString().ToUpper() == "REQUEST")
				{
					RequestContent = session.PreRequestContent ?? session.RequestContent;
				}

				if (section == null || section.GetString().ToUpper() == "SCRIPT")
				{
					this.PreScript.EditableCode = session.PreScript;
					this.PostScript.EditableCode = session.PostScript;
				}

				if (section == null || section.GetString().ToUpper() == "VALIDATIONS")
				{
					this.ValidationsScript.EditableCode = session.ValidationsScript;
				}


				if (session.StatusCode.HasValue && session.ResponseTime.HasValue)
				{
					this.IsResponseVisible = true;
					this.ResponseStatusCode = session.StatusCode.Value;
					this.ResponseStatusDescription = session.StatusDescription;
					this.ResponseInterval = session.ResponseTime;
					this.IsValidationSuccessful = session.IsValidationSuccessFul;
					this.IsCallSuccessFul = session.IsCallSessessFul.HasValue ? session.IsCallSessessFul.Value : this.IdentifyWhetherCallwasSuccess(session.StatusCode);
					// This means response was received
					if (section == null || section.GetString().ToUpper() == "STATUS")
					{
						this.ResponseMessage = this.GetResponseMessage(new RestResponse()
						{ StatusCode = session.StatusCode.Value, StatusDescription = session.StatusDescription, ResponseSize = session.ResponseSize, Interval = session.ResponseTime.Value });
					}

					if (section == null || section.GetString().ToUpper() == "RESPONSE")
					{
						this.requestResponseDataThreadCancellationSource = new CancellationTokenSource();
						Task.Run(() => this.SetOutputResponseContent(session.ResponseContent, session.ResponseContentType), this.requestResponseDataThreadCancellationSource.Token);
						this.ResponseHeaders = session.ResponseHeaders;
					}

					this.BuildSummaryText(session);

					this.LoadValidations(session.Validations);
				}
				else
				{
					this.ResponseMessage = string.Empty;
					this.IsResponseVisible = false;
				}

				this.IsBusy = false;
			});
			sessionLoadThread.Start();
		}

		/// <summary>
		/// The LoadValidations
		/// </summary>
		/// <param name="validations">The <see cref="List{ValidationResultModel}"/></param>
		/// <returns>The <see cref="bool?"/></returns>
		public bool? LoadValidations(List<ValidationResultModel> validations)
		{
			bool? result = null;
			this.Validations = null;

			if (validations != null && validations.Count > 0)
			{
				var outValidations = validations.Select(x => ValidationViewModel.Parse(x));
				this.Validations = new List<ValidationViewModel>(outValidations);
				result = this.Validations.Any(x => x.IsSuccess == false) ? false : true;
			}

			this.IsValidationSuccessful = result;
			return result;
		}

		/// <summary>
		/// The SelectOperation
		/// </summary>
		/// <param name = "operation">The <see cref = "string "/></param>
		public void SelectOperation(string operation)
		{
			this.SelectedOperation = this.Operations.FirstOrDefault(x => x.Text == operation);
		}

		/// <summary>
		/// The SendButtonClicked
		/// </summary>
		public void SendButtonClicked()
		{
			try
			{
				if (SendButtonText == "Send")
				{
					TransactionViewModel request = BuildRequest();

					SendButtonText = "Cancel";
					IsBusy = true;
					if (this.requestResponseDataThreadCancellationSource != null && this.requestResponseDataThreadCancellationSource.Token.CanBeCanceled)
					{
						this.requestResponseDataThreadCancellationSource.Cancel();
					}

					if (this.requestResponseHeaderThreadCancellationSource != null && this.requestResponseHeaderThreadCancellationSource.Token.CanBeCanceled)
					{
						this.requestResponseHeaderThreadCancellationSource.Cancel();
					}

					this.requestThreadCancellationSource = new CancellationTokenSource();
					Task.Run(() => InitiateSendingRequest(request), this.requestThreadCancellationSource.Token);
				}
				else
				{
					SendButtonText = "Send";
					IsBusy = false;

					if (this.requestThreadCancellationSource != null && this.requestThreadCancellationSource.Token.CanBeCanceled)
					{
						this.requestThreadCancellationSource.Cancel();
					}

					if (this.requestResponseDataThreadCancellationSource != null && this.requestResponseDataThreadCancellationSource.Token.CanBeCanceled)
					{
						this.requestResponseDataThreadCancellationSource.Cancel();
					}

					if (this.requestResponseHeaderThreadCancellationSource != null && this.requestResponseHeaderThreadCancellationSource.Token.CanBeCanceled)
					{
						this.requestResponseHeaderThreadCancellationSource.Cancel();
					}
				}
			}
			catch (Exception ex)
			{
				LogException(ex);
				IsBusy = false;
				SendButtonText = "Send";
				_view.MessageShow("Error", ex.Message);
			}
		}

		/// <summary>
		/// The SubscribeResposeReceivedEvent
		/// </summary>
		/// <param name = "handler">The <see cref = "OnResposeReceivedHandler"/></param>
		public void SubscribeResposeReceivedEvent(OnResposeReceivedHandler handler)
		{
			ClearResposeReceivedEvent();
			ResposeReceived += handler;
		}

		#endregion

		#region Private Methods

		///// <summary>
		///// The ApplyVariables
		///// </summary>
		///// <param name = "request">The <see cref = "TransactionViewModel"/></param>
		///// <param name = "variables">The <see cref = "List{KeyValueModel}"/></param>
		///// <returns>The <see cref = "TransactionViewModel"/></returns>
		//private TransactionViewModel ApplyVariables(TransactionViewModel request, List<KeyValueModel> variables)
		//{
		//    request.Url = this.ReplaceVariables(request.PreUrl, variables);
		//    request.RequestContent = this.ReplaceVariables(request.PreRequestContent, variables);
		//    request.Headers = this.ReplaceVariables(request.PreRequestHeaders, variables);
		//    return request;
		//}

		/// <summary>
		/// The BuildParameters
		/// </summary>
		/// <param name="list">The <see cref="List{KeyValuePair{string, string}}"/></param>
		/// <param name="variables">The <see cref="List{KeyValueModel}"/></param>
		/// <returns>The <see cref="List{KeyValuePair{string, string}}"/></returns>
		private List<KeyValuePair<string, string>> BuildParameters(List<KeyValuePair<string, string>> list, List<KeyValueModel> variables)
		{
			if (list == null)
			{
				return null;
			}

			return list.Select(x => new KeyValuePair<string, string>(this.ReplaceVariables(x.Key, variables), this.ReplaceVariables(x.Value, variables))).ToList();
		}

		/// <summary>
		/// The CanCollapseAllJsonNode
		/// </summary>
		/// <returns>The <see cref = "bool "/></returns>
		private bool CanCollapseAllJsonNode()
		{
			return !string.IsNullOrEmpty(JsonResponseContent);
		}

		/// <summary>
		/// The CanExpandAllJsonNode
		/// </summary>
		/// <returns>The <see cref = "bool "/></returns>
		private bool CanExpandAllJsonNode()
		{
			return !string.IsNullOrEmpty(JsonResponseContent);
		}

		/// <summary>
		/// Determines whether ExpandCollapseAll can be executed or not
		/// </summary>
		private bool CanExpandCollapseAllExecute()
		{
			return (this.IsJsonResponse && string.IsNullOrWhiteSpace(this.JsonResponseContent) == false) ||
				(this.IsXmlResponse && string.IsNullOrWhiteSpace(this.XmlResponseContent) == false);
		}

		/// <summary>
		/// The CanSaveButtonClicked
		/// </summary>
		/// <returns>The <see cref = "bool "/></returns>
		private bool CanSaveButtonClicked()
		{
			return true;
		}

		/// <summary>
		/// The ClearButtonClicked
		/// </summary>
		private void ClearButtonClicked()
		{
			try
			{
				this.UrlBase.Url = null;
				this.SelectedOperation = Operations[0];
				this.IsResponseVisible = false;
				this.RequestContent = null;
				this.HeadersBase.SetHeaders(null);
				this.ResponseMessage = null;
				this.ResponseHeaders = null;
				this.RawResponseContent = null;
				this.JsonResponseContent = string.Empty;
				this.PreScript.EditableCode = string.Empty;
				this.PreScript.StatusMessage = string.Empty;
				this.PostScript.EditableCode = string.Empty;
				this.PostScript.StatusMessage = string.Empty;
				this.Validations = null;
				this.IsValidationSuccessful = null;
				this.IsBusy = false;
			}
			catch (Exception ex)
			{
				LogException(ex);
			}
		}

		/// <summary>
		/// The CollapseAllJsonNode
		/// </summary>
		private void CollapseAllJsonNode()
		{
			try
			{
				////if (JsonData != null)
				////{
				////    foreach (JsonTreeDataViewModel node in JsonData)
				////    {
				////        SetExpansion(node, false);
				////    }
				////}
			}
			catch (Exception ex)
			{
				LogException(ex);
			}
		}

		/// <summary>
		/// The ConvertToJsonFormat
		/// </summary>
		/// <param name = "raw">The <see cref = "string "/></param>
		/// <returns>The <see cref = "string "/></returns>
		private string ConvertToJsonFormat(string raw)
		{
			//this.JsonData = new ObservableCollection<JsonTreeDataViewModel>();
			string formattatedText = string.Empty;
			try
			{
				if (raw != null)
				{
					object obj = JsonConvert.DeserializeObject(raw);
					formattatedText = JsonConvert.SerializeObject(obj, Formatting.Indented);
					//List<JsonTreeDataViewModel> treeData = JsonTreeDataViewModel.JsonToTreeview(raw);
					//JsonData = new ObservableCollection<JsonTreeDataViewModel>(treeData);
				}
			}
			catch (Exception ex)
			{
				LogException(ex);
			}

			return formattatedText;
		}

		/// <summary>
		/// The ConvertToXmlFormat
		/// </summary>
		/// <param name = "raw">The <see cref = "string "/></param>
		/// <returns>The <see cref = "string "/></returns>
		private string ConvertToXmlFormat(string raw)
		{
			return XMLHelper.FormatXml(raw);
		}

		/// <summary>
		/// The CopyJsonDataToClipboard
		/// </summary>
		private void CopyJsonDataToClipboard()
		{
			try
			{
				System.Windows.Clipboard.SetText(this.JsonResponseContent);
			}
			catch (Exception ex)
			{
				LogException(ex);
			}
		}

		/// <summary>
		/// Executes ExpandCollapseAll
		/// </summary>
		private void ExecuteExpandCollapseAll(bool expand)
		{
			if (this.IsJsonResponse)
			{
				this._view.ExpandCollapseFoldings(expand, true);
			}
			else if (this.IsXmlResponse)
			{
				this._view.ExpandCollapseFoldings(expand, false);
			}
		}

		/// <summary>
		/// Executes RenameTitle
		/// </summary>
		private void ExecuteRenameTitle(bool input)
		{
			this.IsRenaming = input;
		}

		/// <summary>
		/// Executes RequestContentCategoriesChanged
		/// </summary>
		private void ExecuteRequestContentCategoriesChanged()
		{
			if (SelectedRequestContentCategory != null && SelectedRequestContentCategory.Name == "Raw")
			{
				this.IsRawContentType = true;
			}
			else
			{
				this.IsRawContentType = false;
			}
		}

		/// <summary>
		/// Executes RequestContentTypeChanged
		/// </summary>
		private void ExecuteRequestContentTypeChanged()
		{
			var type = this.SelectedRawContentType?.Text;

			if (type.GetString().ToLower().Contains("json"))
			{
				this.RequestHighlightType = "Json";
			}
			else if (type.GetString().ToLower().Contains("xml"))
			{
				this.RequestHighlightType = "XML";
			}
			else
			{
				this.RequestHighlightType = "Variables";
			}
		}

		/// <summary>
		/// The ExpandAllJsonNode
		/// </summary>
		private void ExpandAllJsonNode()
		{
			try
			{
				//if (JsonData != null)
				//{
				//    foreach (JsonTreeDataViewModel node in JsonData)
				//    {
				//        SetExpansion(node, true);
				//    }
				//}
			}
			catch (Exception ex)
			{
				LogException(ex);
			}
		}

		/// <summary>
		/// The ExtractVariablesUsed
		/// </summary>
		/// <param name = "usedVariables">The <see cref = "List{string}"/></param>
		/// <param name = "text">The <see cref = "string "/></param>
		/// <returns>The <see cref = "List{string}"/></returns>
		private List<string> ExtractVariablesUsed(List<string> usedVariables, string text)
		{
			if (string.IsNullOrEmpty(text) == false)
			{
				string regexPattern = @"\{{([^}]*)\}}";
				var result = Regex.Matches(text, regexPattern);
				foreach (Match item in result)
				{
					usedVariables.Add(item.Groups[1].Value);
				}
			}

			return usedVariables;
		}

		/// <summary>
		/// The GetCertificateFromThumprint
		/// </summary>
		/// <param name="thumbprint">The <see cref="string"/></param>
		/// <returns>The <see cref="X509Certificate2"/></returns>
		private X509Certificate2 GetCertificateFromThumprint(string thumbprint)
		{
			if (string.IsNullOrEmpty(thumbprint))
			{
				return null;
			}
			
			List<X509Certificate2> certs = new List<X509Certificate2>();

			var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
			store.Open(OpenFlags.ReadOnly);

			if (store.Certificates != null)
			{
				foreach (X509Certificate2 mCert in store.Certificates)
				{
					if (mCert.Thumbprint.Equals(thumbprint, StringComparison.CurrentCultureIgnoreCase))
					{
						return mCert;
					}
				}
			}

			certs = new List<X509Certificate2>();

			var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			store.Open(OpenFlags.ReadOnly);

			if (store.Certificates != null)
			{
				foreach (X509Certificate2 mCert in store.Certificates)
				{
					if (mCert.Thumbprint.Equals(thumbprint, StringComparison.CurrentCultureIgnoreCase))
					{
						return mCert;
					}
				}
			}

			return null;
		}

		private X509Certificate2 GetCertificateFromFile(string file, string password)
		{
			if (string.IsNullOrEmpty(file) || string.IsNullOrEmpty(password))
			{
				return null;
			}

			try
			{
				return new X509Certificate2(file, password);
			}
			catch (Exception ex)
			{
				this.LogException(ex);
				return null;
			}
		}

		/// <summary>
		/// The GetHeaders
		/// </summary>
		/// <param name = "headerText">The <see cref = "string "/></param>
		/// <returns>The <see cref = "List{KeyValuePair{string, string}}"/></returns>
		private List<KeyValuePair<string, string>> GetHeaders(string headerText)
		{
			List<KeyValuePair<string, string>> output = null;
			if (headerText != null)
			{
				string[] multiheaders = headerText.Replace(Environment.NewLine, "\n").Split('\n');
				foreach (string header in multiheaders)
				{
					if (header.GetString().Trim() != string.Empty)
					{
						string[] headerParts = header.Split(':');
						if (headerParts.Length == 2)
						{
							KeyValuePair<string, string> obj = new KeyValuePair<string, string>(headerParts[0].GetString().Trim(), headerParts[1].GetString().Trim());
							if (output == null)
							{
								output = new List<KeyValuePair<string, string>>();
							}

							output.Add(obj);
						}
						else
						{
							throw new InvalidCastException("Invalid header content");
						}
					}
				}
			}

			return output;
		}

		/// <summary>
		/// The GetResponseMessage
		/// </summary>
		/// <param name = "response">The <see cref = "RestResponse"/></param>
		/// <returns>The <see cref = "string "/></returns>
		private string GetResponseMessage(RestResponse response)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(string.Format("{0}({1})", response.StatusCode, response.StatusDescription));
			sb.Append(string.Format("  Time: {0} ms", response.Interval));
			if (response.ResponseSize.HasValue)
			{
				sb.Append(string.Format("  Size: {0} ", response.ResponseSize.Value.ToPrettySize(2)));
			}

			return sb.ToString();
		}

		/// <summary>
		/// The IdentifyWhetherCallwasSuccess
		/// </summary>
		/// <param name = "statusCode">The <see cref = "int  ?"/></param>
		/// <returns>The <see cref = "bool "/></returns>
		private bool IdentifyWhetherCallwasSuccess(int? statusCode)
		{
			if (statusCode.HasValue)
			{
				if (statusCode.Value / 100 >= 2 && statusCode.Value / 100 < 3)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private void BuildSummaryText(TransactionViewModel transaction)
		{
			this.SummaryText = this.GetRequestResponseText(transaction);
		}

		/// <summary>
		/// The InitiateSendingRequest
		/// </summary>
		/// <param name = "transaction">The <see cref = "TransactionViewModel"/></param>
		private void InitiateSendingRequest(TransactionViewModel transaction)
		{
			try
			{
				var variables = RaiseGetRelatedVariables();
				if (Validation(transaction, variables) == false)
				{
					this.SendButtonText = "Send";
					return;
				}

				this.Validations = null;
				this.summaryText = null;

				//List<KeyValuePair<string, string>> headers = GetHeaders(HeadersBase.Headers.GetString().Trim());
				if (IsDefaultRestClient)
				{
					/// if this is not a saved request then we need to change its GUID in order to add into history
					/// if we dont change the GUID then it will not add new history instead it will update the existing request
					var newGuid = Guid.NewGuid().ToString();
					if (this.IsSavedRequest == false)
					{
						this.GUID = newGuid;
					}

					transaction.Guid = newGuid;
					MasterEventHandler.RaiseSaveSessionEvent(transaction);
				}

				ExecuteScriptViewModel runtimeAutomation = new ExecuteScriptViewModel();

				if (this.PreScript.IsCodePresent)
				{
					RestClientPreExecutionAutomation preAutomation = new RestClientPreExecutionAutomation(this.ParentViewModel.GlobalVariables, this.ParentViewModel.SelectedEnvironment?.Name);
					runtimeAutomation.ExecutePreCode(this.PreScript.Code, preAutomation);
					this.ParentViewModel.GlobalVariables = preAutomation.GlobalVariableData;
					AppDataHelper.SaveGlobalVariablesData(this.ParentViewModel.GlobalVariables);
				}

				// Get variabled again since there is a chance that the variable values might have changed in PreScript
				variables = RaiseGetRelatedVariables();

				transaction.ApplyVariables(variables);
				List<KeyValuePair<string, string>> headers = GetHeaders(transaction.Headers);

				IsXmlResponse = false;
				IsJsonResponse = false;
				RestResponse restresponse = null;
				X509Certificate2 clientCert = this.GetCertificate();
				ServiceClient client = null;

				if (clientCert != null)
				{
					client = new ServiceClient(transaction.RequestContentType, clientCert);
				}
				else
				{
					client = new ServiceClient(transaction.RequestContentType);
				}

				IsResponseVisible = false;
				if (this.IsRawRequest(transaction.RequestContentType))
				{
					switch (SelectedOperation.Text)
					{
						case Constants.GET:
							restresponse = client.GET(transaction.Url, headers);
							break;
						case Constants.POST:
							restresponse = client.POST(transaction.Url, headers, transaction.RequestContent);
							break;
						case Constants.PUT:
							restresponse = client.PUT(transaction.Url, headers, transaction.RequestContent);
							break;
						case Constants.DELETE:
							restresponse = client.DELETE(transaction.Url, headers, transaction.RequestContent);
							break;
						default:
							break;
					}
				}
				else
				{
					switch (SelectedOperation.Text)
					{
						case Constants.GET:
							restresponse = client.GET(transaction.Url, headers);
							break;
						case Constants.POST:
							restresponse = client.POST(transaction.Url, headers, transaction.RequestParameters);
							break;
						case Constants.PUT:
							restresponse = client.PUT(transaction.Url, headers, transaction.RequestParameters);
							break;
						case Constants.DELETE:
							restresponse = client.DELETE(transaction.Url, headers, transaction.RequestParameters);
							break;
						default:
							break;
					}
				}

				if (this.PostScript.IsCodePresent)
				{
					RestClientPostExecutionAutomation postAutomation = new RestClientPostExecutionAutomation(this.ParentViewModel.GlobalVariables, this.ParentViewModel.SelectedEnvironment?.Name, restresponse);
					runtimeAutomation.ExecutePostCode(this.PostScript.Code, postAutomation);
					this.ParentViewModel.GlobalVariables = postAutomation.GlobalVariableData;
					AppDataHelper.SaveGlobalVariablesData(this.ParentViewModel.GlobalVariables);
				}

				bool? validationSuccess = null;
				if (this.ValidationsScript.IsCodePresent)
				{
					RestClientValidationsAutomation validations = new RestClientValidationsAutomation(this.ParentViewModel.GlobalVariables, this.ParentViewModel.SelectedEnvironment?.Name, restresponse);
					runtimeAutomation.ExecuteValidations(this.ValidationsScript.Code, validations);
					this.ParentViewModel.GlobalVariables = validations.GlobalVariableData;
					AppDataHelper.SaveGlobalVariablesData(this.ParentViewModel.GlobalVariables);

					validationSuccess = this.LoadValidations(validations.Results);
					transaction.Validations = validations.Results;
				}

				this.RaiseResposeReceivedEvent(restresponse);
				if (restresponse != null)
				{
					IsCallSuccessFul = restresponse.IsSuccess;
					IsResponseVisible = true;
					this.ResponseStatusCode = restresponse.StatusCode;
					this.ResponseStatusDescription = restresponse.StatusDescription;
					this.ResponseInterval = restresponse.Interval;
					this.ResponseMessage = GetResponseMessage(restresponse);
					transaction.ResponseHeaders = restresponse.GetResponseHeaders();
					transaction.ResponseContentType = restresponse.ContentType;
					this.requestResponseDataThreadCancellationSource = new CancellationTokenSource();
					Task.Run(() => this.ResponseHeaders = transaction.ResponseHeaders, this.requestResponseDataThreadCancellationSource.Token);
					this.requestResponseHeaderThreadCancellationSource = new CancellationTokenSource();
					Task.Run(() => this.SetOutputResponseContent(restresponse.OutputContent, restresponse.ContentType), this.requestResponseHeaderThreadCancellationSource.Token);

					transaction.StatusCode = restresponse.StatusCode;
					transaction.StatusDescription = restresponse.StatusDescription;
					transaction.ResponseContent = restresponse.OutputContent;
					transaction.ResponseTime = restresponse.Interval;
					transaction.ResponseSize = restresponse.ResponseSize;
					transaction.IsCallSessessFul = restresponse.IsSuccess;
					transaction.IsValidationSuccessFul = validationSuccess;
					//SessionHistory = new ObservableCollection<SessionHistoryViewModel>(AppDataHelper.SaveSessionInHistory(transaction, SessionHistory));
					this.BuildSummaryText(transaction);

					if (IsDefaultRestClient)
					{
						MasterEventHandler.RaiseSaveSessionEvent(transaction);
					}
				}


				if (IsDefaultRestClient)
				{
					this.ParentViewModel?.UpdateEnvironmentData();
					this._view.Dispatcher.BeginInvoke(new Action(() =>
					{
						this.ParentViewModel.BringRestClientToFocus();
					}));
				}
			}
			catch (ThreadAbortException)
			{
				//Do nothing as thread was cancelled by user.
			}
			catch (Exception ex)
			{
				this._view.Dispatcher.BeginInvoke(new Action(() =>
				{
					_view.MessageShow("Error", ex.Message);
				}));
			}
			finally
			{
				this._view.Dispatcher.BeginInvoke(new Action(() =>
				{
					this.IsBusy = false;
					this.SendButtonText = "Send";
				}));
			}
		}

		private X509Certificate2 GetCertificate()
		{
			X509Certificate2 clientCert = null;
			if (this.Certificate != null)
			{
				var cert = this.ParentViewModel?.Settings.Certificates.LastOrDefault(x => x.Name.Equals(this.Certificate.Name.Trim(), StringComparison.CurrentCultureIgnoreCase));
				if (!string.IsNullOrEmpty(cert.Thumbprint))
				{
					clientCert = this.GetCertificateFromThumprint(cert?.Thumbprint);
				}
				else if (!string.IsNullOrEmpty(cert.FilePath))
				{
					clientCert = this.GetCertificateFromFile(cert?.FilePath, cert.FilePassword);
				}
			}

			return clientCert;
		}

		/// <summary>
		/// The IsRawRequest
		/// </summary>
		/// <param name="requestContentType">The <see cref="string"/></param>
		/// <returns>The <see cref="bool"/></returns>
		private bool IsRawRequest(string requestContentType)
		{
			if (this.RequestContentCategories.Any(x => x.Value.Equals(requestContentType, StringComparison.CurrentCultureIgnoreCase)))
			{
				return false;
			}
			else
			{
				if (this.RawContentTypes.Any(x => x.Text.Equals(requestContentType, StringComparison.CurrentCultureIgnoreCase)))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// The OperationChanged
		/// </summary>
		private void OperationChanged()
		{
			try
			{
				if (SelectedOperation != null && SelectedOperation.Text == Constants.GET)
				{
					IsRequestContentVisible = false;
					return;
				}

				IsRequestContentVisible = true;
			}
			catch (Exception ex)
			{
				LogException(ex);
			}
		}

		/// <summary>
		/// The RaiseGetRelatedVariables
		/// </summary>
		/// <returns>The <see cref = "List{KeyValueModel}"/></returns>
		private List<KeyValueModel> RaiseGetRelatedVariables()
		{
			return this.ParentViewModel?.Variables;
		}

		/// <summary>
		/// The RaiseResposeReceivedEvent
		/// </summary>
		/// <param name = "restresponse">The <see cref = "RestResponse"/></param>
		private void RaiseResposeReceivedEvent(RestResponse restresponse)
		{
			if (ResposeReceived != null)
			{
				ResposeReceived(restresponse);
			}
		}

		/// <summary>
		/// The ReplaceVariables
		/// </summary>
		/// <param name = "input">The <see cref = "string "/></param>
		/// <param name = "variables">The <see cref = "List{KeyValueModel}"/></param>
		/// <returns>The <see cref = "string "/></returns>
		private string ReplaceVariables(string input, List<KeyValueModel> variables)
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
		/// The SetExpansion
		/// </summary>
		/// <param name = "node">The <see cref = "JsonTreeDataViewModel"/></param>
		/// <param name = "isExpanded">The <see cref = "bool "/></param>
		private void SetExpansion(JsonTreeDataViewModel node, bool isExpanded)
		{
			if (node != null)
			{
				node.IsExpanded = isExpanded;
				if (node.Nodes != null)
				{
					foreach (JsonTreeDataViewModel childnode in node.Nodes)
					{
						SetExpansion(childnode, isExpanded);
					}
				}
			}
		}

		/// <summary>
		/// The SetOutputResponseContent
		/// </summary>
		/// <param name = "response">The <see cref = "string "/></param>
		private void SetOutputResponseContent(string response, string contentType)
		{
			this.JsonResponseContent = string.Empty;
			this.XmlResponseContent = null;
			this.RawResponseContent = null;
			this.IsJsonResponse = false;
			this.IsHtmlResponse = false;
			this.IsXmlResponse = false;
			RawResponseContent = ConvertToRawFormat(response);
			if ((contentType != null && contentType.Contains("json")) || response.IsJson())
			{
				IsJsonResponse = true;
				SelectedResponseTab = (int)Constants.ResponseTab.Raw;
				JsonResponseContent = this.ConvertToJsonFormat(response);
				SelectedResponseTab = (int)Constants.ResponseTab.Json;
			}
			else if ((contentType != null && contentType.Contains("html")) || response.IsXml())
			{
				this.SelectedResponseTab = (int)Constants.ResponseTab.Html;
				this.HtmlResponseContent = response;
				this.IsHtmlResponse = true;
			}
			else if ((contentType != null && contentType.Contains("xml")) || response.IsXml())
			{
				this.SelectedResponseTab = (int)Constants.ResponseTab.Raw;
				this.XmlResponseContent = this.ConvertToXmlFormat(response);
				if (this.XmlResponseContent != null)
				{
					this.IsXmlResponse = true;
					this.SelectedResponseTab = (int)Constants.ResponseTab.Xml;
				}
			}
			else if (response.GetString() != null)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(ResponseHeaders);
				sb.AppendLine();
				sb.AppendLine(response);
				ResponseHeaders = sb.ToString();
				SelectedResponseTab = (int)Constants.ResponseTab.Raw;
			}
		}

		/// <summary>
		/// The Validation
		/// </summary>
		/// <param name = "request">The <see cref = "TransactionViewModel"/></param>
		/// <param name = "variable">The <see cref = "List{KeyValueModel}"/></param>
		/// <returns>The <see cref = "bool "/></returns>
		private bool Validation(TransactionViewModel request, List<KeyValueModel> variable)
		{
			try
			{
				List<string> usedVariables = new List<string>();
				usedVariables = ExtractVariablesUsed(usedVariables, request.PreUrl);
				usedVariables = ExtractVariablesUsed(usedVariables, request.PreRequestContent);
				usedVariables = ExtractVariablesUsed(usedVariables, request.PreRequestHeaders);
				usedVariables = usedVariables.Distinct().ToList();
				var unavailableVariables = usedVariables.Where(x => variable != null && variable.Any(y => y.Key == x) == false);
				if (unavailableVariables.Any())
				{
					StringBuilder sb = new StringBuilder();
					sb.AppendLine("Unable to find following variables in the Global or selected environment variables.");
					sb.AppendLine();
					foreach (var item in unavailableVariables)
					{
						sb.AppendLine(item);
					}

					this._view.MessageShow("Variables not found", sb.ToString());
					return false;
				}

				Uri temp = new Uri(this.ReplaceVariables(request.PreUrl, variable), UriKind.Absolute);

				bool successPreScript = this.PreScript.CompileCode();
				if (successPreScript == false)
				{
					this._view.MessageShow("Pre Execution Error", "There are some build errors in the pre execution code.");
					return false;
				}

				bool successPostScript = this.PostScript.CompileCode();
				if (successPostScript == false)
				{
					this._view.MessageShow("Post Execution Error", "There are some build errors in the post execution code.");
					return false;
				}

				bool successValidationsScript = this.ValidationsScript.CompileCode();
				if (successValidationsScript == false)
				{
					this._view.MessageShow("Validations Error", "There are some build errors in the validations code.");
					return false;
				}

				if (this.Certificate != null)
				{
					var cert = this.GetCertificate();
					if (cert == null)
					{
						this._view.MessageShow("Validations Error", "Unable to find '" + this.Certificate.Name + "' certificate in the Global or selected environment certificates.");
						return false;
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				_view.MessageShow("Error", ex.Message);
				this.LogException(ex);
				return false;
			}
		}

		#endregion

		#endregion
	}
}
