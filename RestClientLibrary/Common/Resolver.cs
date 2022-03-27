// <copyright file="Resolver.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>10-08-2017</date>

namespace RestClientLibrary.Common
{
    using Unity;
    using RestClientLibrary.Startup;

    /// <summary>
    /// Defines the <see cref="Resolver" />
    /// </summary>
    public class Resolver : IResolver
    {
        #region Methods

        #region Public Methods

        /// <summary>
        /// The Resolve
        /// </summary>
        /// <returns>The <see cref="T"/></returns>
        public T Resolve<T>()
        {
            return UnityConfig.Container.Resolve<T>();
        }

        /// <summary>
        /// The Resolve
        /// </summary>
        /// <param name="key">The <see cref="string"/></param>
        /// <returns>The <see cref="T"/></returns>
        public T Resolve<T>(string key)
        {
            return UnityConfig.Container.Resolve<T>(key);
        }

        #endregion

        #endregion
    }
}
