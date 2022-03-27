// <copyright file="UnityConfig.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>10-08-2017</date>

namespace RestClientLibrary.Startup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.Configuration;
    using Unity;
    using RestClientLibrary.Common;
    using RestClientLibrary.MapperProfiles;
    using RestClientLibrary.ViewModel;
	using System.Reflection;

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
        /// The RegisterMapper
        /// </summary>
        public static void RegisterMapper(IUnityContainer container)
        {
            MapperConfigurationExpression configExp = new MapperConfigurationExpression();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            IEnumerable <Type> profileTypes = FromCheckedAssemblies(AppDomain.CurrentDomain.GetAssemblies(), true).Where(type => type != typeof(BaseProfile) && typeof(BaseProfile).IsAssignableFrom(type));
            foreach (var profileType in profileTypes)
            {
                configExp.AddProfile(profileType);
            }

            container.RegisterInstance<IMapper>(new MapperConfiguration(configExp).CreateMapper());
        }

        private static IEnumerable<Type> FromCheckedAssemblies(IEnumerable<Assembly> assemblies, bool skipOnError)
        {
            return assemblies.SelectMany(delegate (Assembly a)
            {
                IEnumerable<TypeInfo> source;
                try
                {
                    source = a.DefinedTypes;
                }
                catch (ReflectionTypeLoadException ex)
                {
                    if (!skipOnError)
                    {
                        throw;
                    }

                    source = from t in ex.Types.TakeWhile((Type t) => t != null)
                             select t.GetTypeInfo();
                }

                return from ti in source
                       where (ti.IsClass & !ti.IsAbstract) && !ti.IsValueType && ti.IsVisible
                       select ti.AsType();
            });
        }

        /// <summary>
        /// The RegisterTypes
        /// </summary>
        /// <param name="container">The <see cref="IUnityContainer"/></param>
        public static void RegisterTypes(IUnityContainer container)
        {
            Container = container;
            container.RegisterType<IResolver, Resolver>();
            RegisterMapper(container);
            RegisterViewModels(container);
        }

        /// <summary>
        /// The RegisterViewModels
        /// </summary>
        /// <param name="container">The <see cref="IUnityContainer"/></param>
        public static void RegisterViewModels(IUnityContainer container)
        {
            container.RegisterType<TestRunnerViewModel>();
            container.RegisterType<TestRunViewModel>();
        }

        #endregion

        #endregion
    }
}
