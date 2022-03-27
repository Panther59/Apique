// <copyright file="BaseValidationRuleViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel.Validations
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using RestClientLibrary.Common;

    /// <summary>
    /// Defines the <see cref="BaseValidationRuleModel" />
    /// </summary>
    [XmlInclude(typeof(HeaderPresentRuleModel))]
    [XmlInclude(typeof(ResponseContainsTextRuleModel))]
    [XmlInclude(typeof(ResponseFieldTextRuleModel))]
    [XmlInclude(typeof(StatusCodeValidationRuleModel))]
    [Serializable]
    public abstract class BaseValidationRuleModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Result
        /// </summary>
        public ResultState Result
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the ResultText
        /// </summary>
        public string ResultText
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public ValidationType Type
        {
            get; set;
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The Parse
        /// </summary>
        /// <param name="input">The <see cref="IEnumerable{BaseValidationRuleViewModel}"/></param>
        /// <returns>The <see cref="List{BaseValidationRuleModel}"/></returns>
        public static List<BaseValidationRuleModel> Parse(IEnumerable<BaseValidationRuleViewModel> input)
        {
            if (input == null)
            {
                return null;
            }

            List<BaseValidationRuleModel> output = new List<BaseValidationRuleModel>();
            foreach (var item in input)
            {
                BaseValidationRuleModel obj = Parse(item);
                output.Add(obj);
            }

            return output;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The Parse
        /// </summary>
        /// <param name="input">The <see cref="BaseValidationRuleViewModel"/></param>
        /// <returns>The <see cref="BaseValidationRuleModel"/></returns>
        private static BaseValidationRuleModel Parse(BaseValidationRuleViewModel input)
        {
            if (input == null)
            {
                return null;
            }

            switch (input.Type)
            {
                case ValidationType.Status:
                    return StatusCodeValidationRuleModel.ParseViewModel(input as StatusCodeValidationRuleViewModel);
                case ValidationType.ResponseContent:
                    return ResponseContainsTextRuleModel.ParseViewModel(input as ResponseContainsTextRuleViewModel);
                case ValidationType.ResponseField:
                    return ResponseFieldTextRuleModel.ParseViewModel(input as ResponseFieldTextRuleViewModel);
                case ValidationType.Headers:
                    return HeaderPresentRuleModel.ParseViewModel(input as HeaderPresentRuleViewModel);
                default:
                    return null;
            }
        }

        #endregion

        #endregion
    }
}
