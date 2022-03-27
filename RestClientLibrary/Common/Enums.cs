// <copyright file="Enums.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.Common
{
    using System.ComponentModel;

    #region Enums

    /// <summary>
    /// Defines the ResultState
    /// </summary>
    public enum ResultState
    {
        /// <summary>
        /// Defines the NotStarted
        /// </summary>
        NotStarted = 0,
        /// <summary>
        /// Defines the InProgress
        /// </summary>
        InProgress = 1,
        /// <summary>
        /// Defines the Pass
        /// </summary>
        Pass = 2,
        /// <summary>
        /// Defines the Fail
        /// </summary>
        Fail = 3,
        /// <summary>
        /// Defines the NA
        /// </summary>
        NA = 4
    }

    /// <summary>
    /// Defines the SearchCertificateBy
    /// </summary>
    public enum SearchCertificateBy
    {
        /// <summary>
        /// Defines the Thumbprint
        /// </summary>
        Thumbprint = 0,

        /// <summary>
        /// Defines the FriendlyName
        /// </summary>
        FriendlyName = 1
    }

    /// <summary>
    /// Defines the ValidationType
    /// </summary>
    public enum ValidationType
    {
        /// <summary>
        /// Defines the Status
        /// </summary>
        [Description("Status")]
        Status = 0,
        /// <summary>
        /// Defines the ResponseContent
        /// </summary>
        [Description("Response Content")]
        ResponseContent = 1,
        /// <summary>
        /// Defines the ResponseField
        /// </summary>
        [Description("Response Tag")]
        ResponseField = 2,
        /// <summary>
        /// Defines the Headers
        /// </summary>
        [Description("Header")]
        Headers = 3
    }

    /// <summary>
    /// Defines the ValidationConditionType
    /// </summary>
    public enum ValidationConditionType
    {
        /// <summary>
        /// Defines the Equals
        /// </summary>
        Equals = 0,
        /// <summary>
        /// Defines the Contains
        /// </summary>
        Contains = 1,
    }

    #endregion
}
