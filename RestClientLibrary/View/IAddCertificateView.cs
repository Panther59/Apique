// <copyright file="IAddCertificateView.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>05-09-2017</date>

namespace RestClientLibrary.View
{
    using DataLibrary;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IAddCertificateView" />
    /// </summary>
    public interface IAddCertificateView : IBaseView
    {
        string GetFilePath();
    }

    #endregion
}
