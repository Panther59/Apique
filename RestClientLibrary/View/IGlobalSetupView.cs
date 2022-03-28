// <copyright file="IGlobalSetupView.cs" company="Accenture">
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
    /// Defines the <see cref="IGlobalSetupView" />
    /// </summary>
    public interface IGlobalSetupView : IBaseView
    {
        #region Methods

        /// <summary>
        /// The AddNewCertificate
        /// </summary>
        /// <returns>The <see cref="CertificateViewModel"/></returns>
        CertificateViewModel AddNewCertificate();

        /// <summary>
        /// The AddNewEnvironment
        /// </summary>
        /// <param name="environment">The <see cref="EnvironmentViewModel"/></param>
        /// <returns>The <see cref="EnvironmentViewModel"/></returns>
        EnvironmentViewModel AddNewEnvironment(EnvironmentViewModel environment);
		
        string AddNewWorkspace();

		#endregion
	}

    #endregion
}
