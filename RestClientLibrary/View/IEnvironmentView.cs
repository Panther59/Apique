// <copyright file="IEnvironmentView.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>06-09-2017</date>

namespace RestClientLibrary.View
{
    using DataLibrary;
    using RestClientLibrary.ViewModel;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IEnvironmentView" />
    /// </summary>
    public interface IEnvironmentView : IBaseView
    {
        #region Methods

        /// <summary>
        /// The AddNewCertificate
        /// </summary>
        /// <returns>The <see cref="CertificateViewModel"/></returns>
        CertificateViewModel AddNewCertificate();

        #endregion
    }

    #endregion
}
