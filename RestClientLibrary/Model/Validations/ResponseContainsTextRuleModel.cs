// <copyright file="ResponseContainsTextRuleModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel.Validations
{
    using System;
    using RestClientLibrary.Common;

    /// <summary>
    /// Defines the <see cref="ResponseContainsTextRuleModel" />
    /// </summary>
    [Serializable]
    public class ResponseContainsTextRuleModel : BaseValidationRuleModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ExpectedContent
        /// </summary>
        public string ExpectedContent
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the CompareMode
        /// </summary>
        public ValidationConditionType CompareMode
        {
            get; set;
        }

        public static ResponseContainsTextRuleModel ParseViewModel(ResponseContainsTextRuleViewModel input)
        {
            if (input == null)
            {
                return null;
            }

            return new ResponseContainsTextRuleModel
            {
                Result = input.Result,
                ResultText = input.ResultText,
                CompareMode = input.CompareMode,
                ExpectedContent = input.ExpectedContent,
                Type = input.Type,
            };
        }

        #endregion
    }
}
