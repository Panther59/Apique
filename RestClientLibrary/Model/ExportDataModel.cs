// <copyright file="ExportDataModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>12-08-2017</date>

namespace RestClientLibrary.Model
{
    using System;
    using System.Collections.Generic;
    using RestClientLibrary.ViewModel;

    /// <summary>
    /// Defines the <see cref="ExportDataModel" />
    /// </summary>
    [Serializable]
    public class ExportDataModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Categories
        /// </summary>
        public List<CategoryViewModel> Categories
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets Environments
        /// </summary>
        public List<EnvironmentModel> Environments
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the History
        /// </summary>
        public List<SessionHistoryViewModel> History
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the TestRuns
        /// </summary>
        public List<TestRunViewModel> TestRuns
        {
            get; set;
        }

        #endregion
    }
}
