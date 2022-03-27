// <copyright file="BaseProfile.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>10-08-2017</date>

namespace RestClientLibrary.MapperProfiles
{
    using AutoMapper;

    /// <summary>
    /// Defines the <see cref="BaseProfile" />
    /// </summary>
    public abstract class BaseProfile : Profile
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseProfile"/> class.
        /// </summary>
        /// <param name="profileName">The <see cref="string"/></param>
        protected BaseProfile(string profileName) : base(profileName)
        {
            this.CreateMaps();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The CreateMaps
        /// </summary>
        protected abstract void CreateMaps();

        #endregion
    }
}
