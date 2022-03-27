// <copyright file="ValidationResultModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>06-08-2017</date>

namespace RestClientLibrary.Model
{
    /// <summary>
    /// Defines the <see cref="ValidationResultModel" />
    /// </summary>
    public class ValidationResultModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets TestName
        /// </summary>
        public string TestName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets ExpectedValue
        /// </summary>
        public string ExpectedValue
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets ActualValue
        /// </summary>
        public string ActualValue
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets Description
        /// </summary>
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsSuccess
        /// </summary>
        public bool IsSuccess { get; set; }

        #endregion
    }
}
