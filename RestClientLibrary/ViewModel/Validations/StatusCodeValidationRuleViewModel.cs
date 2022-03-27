// <copyright file="StatusCodeValidationRuleViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel.Validations
{
    using System.Net;

    /// <summary>
    /// Defines the <see cref="StatusCodeValidationRuleViewModel" />
    /// </summary>
    public class StatusCodeValidationRuleViewModel : BaseValidationRuleViewModel
    {
        #region Fields

        /// <summary>
        /// The expectedStatusCode field
        /// </summary>
        private HttpStatusCode expectedStatusCode;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusCodeValidationRuleViewModel"/> class.
        /// </summary>
        public StatusCodeValidationRuleViewModel()
        {
            this.RuleName = "Status code";
            this.expectedStatusCode = HttpStatusCode.OK;
            this.Type = Common.ValidationType.Status;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ExpectedStatusCode
        /// </summary>
        public HttpStatusCode ExpectedStatusCode
        {
            get
            {
                return this.expectedStatusCode;
            }

            set
            {
                this.expectedStatusCode = value;
                this.OnPropertyChanged("ExpectedStatusCode");
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The ParseModel
        /// </summary>
        /// <param name="input">The <see cref="StatusCodeValidationRuleModel"/></param>
        /// <returns>The <see cref="StatusCodeValidationRuleViewModel"/></returns>
        public static StatusCodeValidationRuleViewModel ParseModel(StatusCodeValidationRuleModel input)
        {
            if (input == null)
            {
                return null;
            }

            return new StatusCodeValidationRuleViewModel
            {
                Result = input.Result,
                ResultText = input.ResultText,
                ExpectedStatusCode = input.ExpectedStatusCode,
                Type = input.Type,
            };
        }

        /// <inheritdoc />
        public override bool Validate(RestResponse restResponse)
        {
            bool result = true;
            if (restResponse.StatusCode == (int)this.ExpectedStatusCode)
            {
                this.Result = Common.ResultState.Pass;
            }
            else
            {
                this.Result = Common.ResultState.Fail;
                this.ResultText = string.Format("Expected {0}({1}) status code however received {2}", this.ExpectedStatusCode.ToString(), (int)this.ExpectedStatusCode, restResponse.StatusCode);
                result = false;
            }

            return result;
        }

        #endregion

        #endregion
    }
}
