// <copyright file="IExportView.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>11-08-2017</date>

namespace RestClientLibrary.View
{
    using DataLibrary;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IExportView" />
    /// </summary>
    public interface IExportView : IBaseView
    {
        string SaveFileDialog();
    }

    #endregion
}
