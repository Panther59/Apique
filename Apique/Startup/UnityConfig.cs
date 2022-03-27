// <copyright file="UnityConfig.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>10-08-2017</date>

namespace Apique.Startup
{
    using Unity;

    /// <summary>
    /// Defines the <see cref="UnityConfig" />
    /// </summary>
    public static class UnityConfig
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Container
        /// </summary>
        public static IUnityContainer Container
        {
            get; set;
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The RegisterTypes
        /// </summary>
        public static void RegisterTypes()
        {
            Container = new UnityContainer();
            RestClientLibrary.Startup.UnityConfig.RegisterTypes(Container);
        }

        #endregion

        #endregion
    }
}
