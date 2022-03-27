// <copyright file="ResponseFieldTextRuleViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel.Validations
{
    using System;
    using Newtonsoft.Json.Linq;
    using RestClientLibrary.Common;

    /// <summary>
    /// Defines the <see cref="ResponseFieldTextRuleViewModel" />
    /// </summary>
    public class ResponseFieldTextRuleViewModel : BaseValidationRuleViewModel
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

        /// <summary>
        /// The fieldPath field
        /// </summary>
        private string fieldPath;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseFieldTextRuleViewModel"/> class.
        /// </summary>
        public ResponseFieldTextRuleViewModel()
        {
            this.RuleName = "Response Field";
            this.Type = Common.ValidationType.ResponseField;
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
        /// Gets or sets the FieldPath
        /// </summary>
        public string FieldPath
        {
            get
            {
                return this.fieldPath;
            }

            set
            {
                this.fieldPath = value;
                this.OnPropertyChanged("FieldPath");
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
        /// <param name="input">The <see cref="ResponseFieldTextRuleModel"/></param>
        /// <returns>The <see cref="ResponseFieldTextRuleViewModel"/></returns>
        public static ResponseFieldTextRuleViewModel ParseModel(ResponseFieldTextRuleModel input)
        {
            if (input == null)
            {
                return null;
            }

            return new ResponseFieldTextRuleViewModel
            {
                Result = input.Result,
                ResultText = input.ResultText,
                CompareMode = input.CompareMode,
                FieldPath = input.FieldPath,
                ExpectedContent = input.ExpectedContent,
                Type = input.Type,
            };
        }

        /// <inheritdoc />
        public override bool Validate(RestResponse restResponse)
        {
            var obj = JObject.Parse(restResponse.OutputContent);
            var actualContent = (string)obj.SelectToken(this.FieldPath);
            bool result = true;
            if (this.CompareMode == ValidationConditionType.Equals)
            {
                if (actualContent.Equals(this.ExpectedContent, StringComparison.CurrentCultureIgnoreCase))
                {
                    this.Result = Common.ResultState.Pass;
                }
                else
                {
                    this.Result = Common.ResultState.Fail;
                    this.ResultText = string.Format("{0} field value was expected '{1}', however received '{2}'", this.FieldPath, this.ExpectedContent, actualContent);
                    result = false;
                }
            }
            else
            {
                if (actualContent.GetString().ToLower().Contains(this.ExpectedContent.GetString().ToLower()))
                {
                    this.Result = Common.ResultState.Pass;
                }
                else
                {
                    this.Result = Common.ResultState.Fail;
                    this.ResultText = string.Format("{0} field doesnt contain '{1}', as received content is '{2}'", this.FieldPath, this.ExpectedContent, actualContent);
                    result = false;
                }
            }

            return result;
        }

        #endregion

        #endregion
    }
}
