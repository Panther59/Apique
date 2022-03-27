// <copyright file="ResponseFieldTextRuleModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel.Validations
{
    using System;
    using RestClientLibrary.Common;

    /// <summary>
    /// Defines the <see cref="ResponseFieldTextRuleModel" />
    /// </summary>
    [Serializable]
    public class ResponseFieldTextRuleModel : BaseValidationRuleModel
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
        /// Gets or sets the FieldPath
        /// </summary>
        public string FieldPath
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

        public static ResponseFieldTextRuleModel ParseViewModel(ResponseFieldTextRuleViewModel input)
        {
            if (input == null)
            {
                return null;
            }

            return new ResponseFieldTextRuleModel
            {
                Result = input.Result,
                ResultText = input.ResultText,
                CompareMode = input.CompareMode,
                FieldPath = input.FieldPath,
                ExpectedContent = input.ExpectedContent,
                Type = input.Type,
            };
        }

        #endregion
    }
}
