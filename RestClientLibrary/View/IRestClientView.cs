// <copyright file="IRestClientView.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.View
{
    using DataLibrary;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IRestClientView" />
    /// </summary>
    public interface IRestClientView : IBaseView
    {
        #region Methods

        /// <summary>
        /// The ExpandCollapseFoldings
        /// </summary>
        /// <param name="isExpanded">The <see cref="bool"/></param>
        /// <param name="isJson">The <see cref="bool"/></param>
        void ExpandCollapseFoldings(bool isExpanded, bool isJson);

        /// <summary>
        /// The OpenSaveRequestWindow
        /// </summary>
        /// <param name="data">The <see cref="RestClientLibrary.ViewModel.SaveRequestViewModel"/></param>
        /// <returns>The <see cref="bool?"/></returns>
        bool? OpenSaveRequestWindow(RestClientLibrary.ViewModel.SaveRequestViewModel data);

        #endregion
    }

    #endregion
}
