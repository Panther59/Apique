// <copyright file="EnvironmentModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>05-09-2017</date>

namespace RestClientLibrary.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref = "EnvironmentModel"/>
    /// </summary>
    public class EnvironmentModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentModel"/> class.
        /// </summary>
        public EnvironmentModel()
        {
            this.Guid = System.Guid.NewGuid().ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Certificates
        /// </summary>
        public CertificateModel DefaultCertificate { get; set; }

        /// <summary>
        /// Gets or sets the Guid
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Workspace
        /// </summary>
        public string Workspace { get; set; }

        /// <summary>
        /// Gets or sets Variables
        /// </summary>
        public List<KeyValueModel> Variables { get; set; }

        #endregion
    }
}
