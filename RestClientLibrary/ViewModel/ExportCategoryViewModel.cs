// <copyright file="ExportCategoryViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>12-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using System.Collections.Generic;
    using System.Linq;
    using DataLibrary;

    /// <summary>
    /// Defines the <see cref="ExportCategoryViewModel" />
    /// </summary>
    public class ExportCategoryViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// The items field
        /// </summary>
        private List<ExportItemViewModel> items;

        /// <summary>
        /// The name field
        /// </summary>
        private string name;

        /// <summary>
        /// The shouldExport field
        /// </summary>
        private bool? shouldExport;

        #region Commands

        /// <summary>
        /// The itemExportChangedCommand field
        /// </summary>
        private RelayCommand<ExportItemViewModel> itemExportChangedCommand;

        /// <summary>
        /// The shouldExportChangedCommand field
        /// </summary>
        private RelayCommand shouldExportChangedCommand;

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Items
        /// </summary>
        public List<ExportItemViewModel> Items
        {
            get
            {
                return this.items;
            }

            set
            {
                this.items = value;
                this.OnPropertyChanged("Items");
            }
        }

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
        public bool? ShouldExport
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

        #region Commands

        /// <summary>
        /// Gets the ItemExportChangedCommand
        /// </summary>
        public RelayCommand<ExportItemViewModel> ItemExportChangedCommand
        {
            get
            {
                if (this.itemExportChangedCommand == null)
                {
                    this.itemExportChangedCommand = new RelayCommand<ExportItemViewModel>(command => this.ExecuteItemExportChanged(command));
                }

                return this.itemExportChangedCommand;
            }
        }

        /// <summary>
        /// Gets the ShouldExportChangedCommand
        /// </summary>
        public RelayCommand ShouldExportChangedCommand
        {
            get
            {
                if (this.shouldExportChangedCommand == null)
                {
                    this.shouldExportChangedCommand = new RelayCommand(command => this.ExecuteShouldExportChanged());
                }

                return this.shouldExportChangedCommand;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Executes ItemExportChanged
        /// </summary>
        private void ExecuteItemExportChanged(ExportItemViewModel input)
        {
            var checkedItems = this.Items.Count(x => x.ShouldExport);
            this.ShouldExport = checkedItems == this.Items.Count ? true : (checkedItems == 0 ? false : (bool?)null);
        }

        /// <summary>
        /// Executes ShouldExportChanged
        /// </summary>
        private void ExecuteShouldExportChanged()
        {
            if (this.Items != null && this.ShouldExport.HasValue)
            {
                foreach (var item in this.Items)
                {
                    item.ShouldExport = this.ShouldExport.Value;
                }
            }
        }

        #endregion

        #endregion
    }
}
