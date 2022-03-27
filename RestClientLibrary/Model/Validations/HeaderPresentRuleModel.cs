// <copyright file="HeaderPresentRuleModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

using System;

namespace RestClientLibrary.ViewModel.Validations
{
    /// <summary>
    /// Defines the <see cref="HeaderPresentRuleModel" />
    /// </summary>
    [Serializable]
    public class HeaderPresentRuleModel : BaseValidationRuleModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the HeaderKey
        /// </summary>
        public string HeaderKey
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the ExpectedHeaderValue
        /// </summary>
        public string ExpectedHeaderValue
        {
            get; set;
        }

        public static HeaderPresentRuleModel ParseViewModel(HeaderPresentRuleViewModel input)
        {
            if (input == null)
            {
                return null;
            }

            return new HeaderPresentRuleModel
            {
                Result = input.Result,
                ResultText = input.ResultText,
                ExpectedHeaderValue = input.ExpectedHeaderValue,
                HeaderKey = input.HeaderKey,
                Type = input.Type,
            };
        }

        #endregion
    }
}
