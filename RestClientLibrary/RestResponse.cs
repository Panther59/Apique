// <copyright file="RestResponse.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>27-08-2017</date>

namespace RestClientLibrary
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="RestResponse" />
    /// </summary>
    public class RestResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ContentType
        /// </summary>
        public string ContentType { get; set; }

        //Time elapsed in miliseconds
        //Time elapsed in miliseconds
        /// <summary>
        /// Gets or sets the Interval
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsSuccess
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the OutputContent
        /// </summary>
        public string OutputContent { get; set; }

        /// <summary>
        /// Gets or sets the OutputHeader
        /// </summary>
        public List<KeyValuePair<string, string>> OutputHeader { get; set; }

        /// <summary>
        /// Gets or sets the ResponseSize
        /// </summary>
        public long? ResponseSize { get; set; }

        /// <summary>
        /// Gets or sets the StatusCode
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the StatusDescription
        /// </summary>
        public string StatusDescription { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The GetResponseHeaders
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string GetResponseHeaders()
        {
            StringBuilder sb = new StringBuilder();
            if (OutputHeader != null && OutputHeader.Count > 0)
            {
                foreach (var header in OutputHeader)
                {
                    sb.AppendLine(string.Format("{0}:{1}", header.Key, header.Value));
                }
            }
            return sb.ToString();
        }

        #endregion

        #endregion
    }
}
