// <copyright file="ExportItemViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>12-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using DataLibrary;

    /// <summary>
    /// Defines the <see cref="ExportItemViewModel" />
    /// </summary>
    public class ExportItemViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// The name field
        /// </summary>
        private string name;

        /// <summary>
        /// The shouldExport field
        /// </summary>
        private bool shouldExport;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ShouldExport
        /// </summary>
        public bool ShouldExport
        {
            get
            {
                return this.shouldExport;
            }

            set
            {
                this.shouldExport = value;
                this.OnPropertyChanged("ShouldExport");
            }
        }

        #endregion
    }
}
