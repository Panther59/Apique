// <copyright file="ViewEnvironmentsViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>22-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using System.Collections.Generic;
    using DataLibrary;

    /// <summary>
    /// Defines the <see cref="ViewEnvironmentsViewModel" />
    /// </summary>
    public class ViewEnvironmentsViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// The environment field
        /// </summary>
        private EnvironmentViewModel environment;

        /// <summary>
        /// The variables field
        /// </summary>
        private List<KeyValueViewModel> variables;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Environment
        /// </summary>
        public EnvironmentViewModel Environment
        {
            get
            {
                return this.environment;
            }

            set
            {
                this.environment = value;
                this.OnPropertyChanged("Environment");
            }
        }

        /// <summary>
        /// Gets or sets the Variables
        /// </summary>
        public List<KeyValueViewModel> Variables
        {
            get
            {
                return this.variables;
            }

            set
            {
                this.variables = value;
                this.OnPropertyChanged("Variables");
            }
        }

        #endregion
    }
}
