// <copyright file="StatusCodeValidationRuleViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel.Validations
{
    using System;
    using System.Net;

    /// <summary>
    /// Defines the <see cref="StatusCodeValidationRuleModel" />
    /// </summary>
    [Serializable]
    public class StatusCodeValidationRuleModel : BaseValidationRuleModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ExpectedStatusCode
        /// </summary>
        public HttpStatusCode ExpectedStatusCode
        {
            get; set;
        }

        public static StatusCodeValidationRuleModel ParseViewModel(StatusCodeValidationRuleViewModel input)
        {
            if (input == null)
            {
                return null;
            }

            return new StatusCodeValidationRuleModel
            {
                Result = input.Result,
                ResultText = input.ResultText,
                ExpectedStatusCode = input.ExpectedStatusCode,
                Type = input.Type,
            };
        }

        #endregion
    }
}
