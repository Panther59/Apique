// <copyright file="IResolver.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>10-08-2017</date>

namespace RestClientLibrary.Common
{
    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IResolver" />
    /// </summary>
    public interface IResolver
    {
        #region Methods

        /// <summary>
        /// The Resolve
        /// </summary>
        /// <returns>The <see cref="T"/></returns>
        T Resolve<T>();

        /// <summary>
        /// The Resolve
        /// </summary>
        /// <param name="key">The <see cref="string"/></param>
        /// <returns>The <see cref="T"/></returns>
        T Resolve<T>(string key);

        #endregion
    }

    #endregion
}
