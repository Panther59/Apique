// <copyright file="HeaderPresentRuleViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel.Validations
{
    using System;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="HeaderPresentRuleViewModel" />
    /// </summary>
    public class HeaderPresentRuleViewModel : BaseValidationRuleViewModel
    {
        #region Fields

        /// <summary>
        /// The expectedHeaderValue field
        /// </summary>
        private string expectedHeaderValue;

        /// <summary>
        /// The headerKey field
        /// </summary>
        private string headerKey;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderPresentRuleViewModel"/> class.
        /// </summary>
        public HeaderPresentRuleViewModel()
        {
            this.RuleName = "Header";
            this.Type = Common.ValidationType.Headers;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the HeaderKey
        /// </summary>
        public string HeaderKey
        {
            get
            {
                return this.headerKey;
            }

            set
            {
                this.headerKey = value;
                this.OnPropertyChanged("HeaderKey");
            }
        }

        /// <summary>
        /// Gets or sets the ExpectedHeaderValue
        /// </summary>
        public string ExpectedHeaderValue
        {
            get
            {
                return this.expectedHeaderValue;
            }

            set
            {
                this.expectedHeaderValue = value;
                this.OnPropertyChanged("ExpectedHeaderValue");
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The ParseModel
        /// </summary>
        /// <param name="input">The <see cref="HeaderPresentRuleModel"/></param>
        /// <returns>The <see cref="HeaderPresentRuleViewModel"/></returns>
        public static HeaderPresentRuleViewModel ParseModel(HeaderPresentRuleModel input)
        {
            if (input == null)
            {
                return null;
            }

            return new HeaderPresentRuleViewModel
            {
                Result = input.Result,
                ResultText = input.ResultText,
                ExpectedHeaderValue = input.ExpectedHeaderValue,
                HeaderKey = input.HeaderKey,
                Type = input.Type,
            };
        }

        /// <inheritdoc />
        public override bool Validate(RestResponse restResponse)
        {
            bool result = true;
            if (string.IsNullOrWhiteSpace(this.HeaderKey) == false)
            {
                if (restResponse.OutputHeader.Any(x => x.Key.Equals(this.HeaderKey.Trim(), StringComparison.CurrentCultureIgnoreCase)))
                {
                    var header = restResponse.OutputHeader.FirstOrDefault(x => x.Key.Equals(this.HeaderKey.Trim(), StringComparison.CurrentCultureIgnoreCase));
                    if (header.Value.Equals(this.ExpectedHeaderValue, StringComparison.CurrentCultureIgnoreCase))
                    {
                        this.Result = Common.ResultState.Pass;
                    }
                    else
                    {
                        this.Result = Common.ResultState.Fail;
                        this.ResultText = string.Format("{0} Header value is expected '{1}', however received '{2}'", header.Key, this.ExpectedHeaderValue, header.Value);
                        result = false;
                    }
                }
                else
                {
                    this.Result = Common.ResultState.Fail;
                    this.ResultText = string.Format("{0} Header was not present", this.HeaderKey);
                    result = false;
                }
            }

            return result;
        }

        #endregion

        #endregion
    }
}
