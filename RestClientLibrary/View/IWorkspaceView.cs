// <copyright file="IMainView.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>09-08-2017</date>

namespace RestClientLibrary.View
{
    using DataLibrary;
    using RestClientLibrary.Model;
    using RestClientLibrary.ViewModel;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IWorkspaceView" />
    /// </summary>
    public interface IWorkspaceView : IBaseView
    {
        #region Properties

        /// <summary>
        /// Gets the HistoryView
        /// </summary>
        IHistoryView HistoryView
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The OpenExportWindow
        /// </summary>
        void OpenExportWindow();

        /// <summary>
        /// The OpenFile
        /// </summary>
        /// <param name="filter">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        string OpenFile(string filter);

        /// <summary>
        /// The OpenSaveRequestWindow
        /// </summary>
        /// <param name="data">The <see cref="RestClientLibrary.ViewModel.SaveRequestViewModel"/></param>
        /// <returns>The <see cref="bool?"/></returns>
        bool? OpenSaveRequestWindow(RestClientLibrary.ViewModel.SaveRequestViewModel data);

        /// <summary>
        /// The OpenTestRunner
        /// </summary>
        void OpenTestRunner();

        /// <summary>
        /// The SaveFile
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        string SaveFile();

        /// <summary>
        /// The ViewEnvironmentWindow
        /// </summary>
        /// <param name="globalData">The <see cref="GlobalVariableModel"/></param>
        /// <param name="environment">The <see cref="EnvironmentModel"/></param>
        /// <returns>The <see cref="EnvironmentModel"/></returns>
        EnvironmentModel ViewEnvironmentWindow(GlobalVariableModel globalData, EnvironmentModel environment);

        /// <summary>
        /// The ViewSettingsWindow
        /// </summary>
        /// <param name="settings">The <see cref="SettingsViewModel"/></param>
        void ViewSettingsWindow(SettingsViewModel settings);

        #endregion
    }

    #endregion
}
