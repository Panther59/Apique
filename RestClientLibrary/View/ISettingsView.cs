// <copyright file="ISettingsView.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>16-08-2017</date>

namespace RestClientLibrary.View
{
    using DataLibrary;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="ISettingsView" />
    /// </summary>
    public interface ISettingsView : IBaseView
    {
        #region Methods

        /// <summary>
        /// The OpenExportWindow
        /// </summary>
        void OpenExportWindow();

        /// <summary>
        /// The OpenFile
        /// </summary>
        /// <param name="exportImportFilter">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        string OpenFile(string exportImportFilter);

        /// <summary>
        /// The OpenImportWindow
        /// </summary>
        /// <param name="path">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool OpenImportWindow(string path);

        #endregion
    }

    #endregion
}
