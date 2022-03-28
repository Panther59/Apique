// <copyright file="GlobalVariableModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>05-09-2017</date>

namespace RestClientLibrary.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref = "GlobalVariableModel"/>
    /// </summary>
    public class GlobalVariableModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets Certificates
        /// </summary>
        public List<CertificateModel> Certificates { get; set; }

        /// <summary>
        /// Gets or sets Environments
        /// </summary>
        public List<EnvironmentModel> Environments { get; set; }

        /// <summary>
        /// Gets or sets Variables
        /// </summary>
        public List<KeyValueModel> Variables { get; set; }

        public List<string> Workspaces { get; set; }
        #endregion
    }
}
