// <copyright file="ResponseContainsTextRuleViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel.Validations
{
    using System;
    using RestClientLibrary.Common;

    /// <summary>
    /// Defines the <see cref="ResponseContainsTextRuleViewModel" />
    /// </summary>
    public class ResponseContainsTextRuleViewModel : BaseValidationRuleViewModel
    {
        #region Fields

        /// <summary>
        /// The compareMode field
        /// </summary>
        private ValidationConditionType compareMode;

        /// <summary>
        /// The expectedContent field
        /// </summary>
        private string expectedContent;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseContainsTextRuleViewModel"/> class.
        /// </summary>
        public ResponseContainsTextRuleViewModel()
        {
            this.RuleName = "Response Content Check";
            this.Type = Common.ValidationType.ResponseContent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ExpectedContent
        /// </summary>
        public string ExpectedContent
        {
            get
            {
                return this.expectedContent;
            }

            set
            {
                this.expectedContent = value;
                this.OnPropertyChanged("ExpectedContent");
            }
        }

        /// <summary>
        /// Gets or sets the CompareMode
        /// </summary>
        public ValidationConditionType CompareMode
        {
            get
            {
                return this.compareMode;
            }

            set
            {
                this.compareMode = value;
                this.OnPropertyChanged("CompareMode");
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The ParseModel
        /// </summary>
        /// <param name="input">The <see cref="ResponseContainsTextRuleModel"/></param>
        /// <returns>The <see cref="ResponseContainsTextRuleViewModel"/></returns>
        public static ResponseContainsTextRuleViewModel ParseModel(ResponseContainsTextRuleModel input)
        {
            if (input == null)
            {
                return null;
            }

            return new ResponseContainsTextRuleViewModel
            {
                Result = input.Result,
                ResultText = input.ResultText,
                CompareMode = input.CompareMode,
                ExpectedContent = input.ExpectedContent,
                Type = input.Type,
            };
        }

        /// <inheritdoc />
        public override bool Validate(RestResponse restResponse)
        {
            bool result = true;
            if (this.CompareMode == ValidationConditionType.Equals)
            {
                if (restResponse.OutputContent.Equals(this.ExpectedContent, StringComparison.CurrentCultureIgnoreCase))
                {
                    this.Result = Common.ResultState.Pass;
                }
                else
                {
                    this.Result = Common.ResultState.Fail;
                    this.ResultText = string.Format("Response content is not equal to expcted content");
                    result = false;
                }
            }
            else
            {
                if (restResponse.OutputContent.GetString().ToLower().Contains(this.ExpectedContent.GetString().ToLower()))
                {
                    this.Result = Common.ResultState.Pass;
                }
                else
                {
                    this.Result = Common.ResultState.Fail;
                    this.ResultText = string.Format("Could not find the required content in the response content");
                    result = false;
                }
            }

            return result;
        }

        #endregion

        #endregion
    }
}
