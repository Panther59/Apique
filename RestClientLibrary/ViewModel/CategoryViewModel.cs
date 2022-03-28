// <copyright file="CategoryViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>08-09-2017</date>

namespace RestClientLibrary.ViewModel
{
    using DataLibrary;
	using Newtonsoft.Json;
	using RestClientLibrary.Common;
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the <see cref = "CategoryViewModel"/>
    /// </summary>
    public class CategoryViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// Defines the guid
        /// </summary>
        private string guid;

        /// <summary>
        /// The isExpanded field
        /// </summary>
        private bool isExpanded;

        /// <summary>
        /// The isRenaming field
        /// </summary>
        private bool isRenaming;

        /// <summary>
        /// The name field
        /// </summary>
        private string name;

        /// <summary>
        /// The requests field
        /// </summary>
        private ObservableCollection<TransactionViewModel> requests;

        #region Commands

        /// <summary>
        /// The cloneRequestCommand field
        /// </summary>
        private RelayCommand<TransactionViewModel> cloneRequestCommand;

        /// <summary>
        /// The deleteRequestCommand field
        /// </summary>
        private RelayCommand<TransactionViewModel> deleteRequestCommand;

        /// <summary>
        /// The renameCategoryCommand field
        /// </summary>
        private RelayCommand<bool> renameCategoryCommand;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref = "CategoryViewModel"/> class.
        /// </summary>
        public CategoryViewModel()
        {
            this.guid = System.Guid.NewGuid().ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Guid
        /// </summary>
        public string Guid
        {
            get
            {
                return guid;
            }

            set
            {
                guid = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsExpanded
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }

            set
            {
                this.isExpanded = value;
                this.OnPropertyChanged("IsExpanded");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsRenaming
        /// </summary>
        public bool IsRenaming
        {
            get
            {
                return this.isRenaming;
            }

            set
            {
                this.isRenaming = value;
                this.OnPropertyChanged("IsRenaming");
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
        /// Gets or sets the Requests
        /// </summary>
        public ObservableCollection<TransactionViewModel> Requests
        {
            get
            {
                return this.requests;
            }

            set
            {
                this.requests = value;
                this.OnPropertyChanged("Requests");
            }
        }

        #region Commands

        /// <summary>
        /// Gets the CloneRequestCommand
        /// </summary>
        public RelayCommand<TransactionViewModel> CloneRequestCommand
        {
            get
            {
                if (this.cloneRequestCommand == null)
                {
                    this.cloneRequestCommand = new RelayCommand<TransactionViewModel>(command => this.ExecuteCloneRequest(command));
                }

                return this.cloneRequestCommand;
            }
        }

        /// <summary>
        /// Gets the DeleteRequestCommand
        /// </summary>
        public RelayCommand<TransactionViewModel> DeleteRequestCommand
        {
            get
            {
                if (this.deleteRequestCommand == null)
                {
                    this.deleteRequestCommand = new RelayCommand<TransactionViewModel>(command => this.ExecuteDeleteRequest(command));
                }

                return this.deleteRequestCommand;
            }
        }

        /// <summary>
        /// Gets the RenameCategoryCommand
        /// </summary>
        [JsonIgnore]
        public RelayCommand<bool> RenameCategoryCommand
        {
            get
            {
                if (this.renameCategoryCommand == null)
                {
                    this.renameCategoryCommand = new RelayCommand<bool>(command => this.ExecuteRenameCategory(command));
                }

                return this.renameCategoryCommand;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The ReorderRequests
        /// </summary>
        /// <param name="removedIdx">The <see cref="int"/></param>
        /// <param name="targetIdx">The <see cref="int"/></param>
        public void ReorderRequests(TransactionViewModel droppedData, TransactionViewModel target)
        {
            try
            {
                if (this.Requests != null)
                {
                    int removedIdx = this.Requests.IndexOf(droppedData);
                    int targetIdx = this.Requests.IndexOf(target);

                    if (this.Requests.Count > removedIdx)
                    {
                        var itemToMove = this.Requests[removedIdx];

                        if (removedIdx < targetIdx)
                        {
                            this.Requests.Insert(targetIdx + 1, itemToMove);
                            this.Requests.RemoveAt(removedIdx);
                        }
                        else
                        {
                            int remIdx = removedIdx + 1;
                            if (this.Requests.Count + 1 > remIdx)
                            {
                                this.Requests.Insert(targetIdx, itemToMove);
                                this.Requests.RemoveAt(remIdx);
                            }
                        }
                    }
                }

                AppDataHelper.SaveRequestData(this);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Executes CloneRequest
        /// </summary>
        private void ExecuteCloneRequest(TransactionViewModel input)
        {
            try
            {
                var newObj = AppDataHelper.DeepClone<TransactionViewModel>(input);
                newObj.Guid = System.Guid.NewGuid().ToString();
                newObj.Name = input.Name + " - clone";
                newObj.ParentViewModel = input.ParentViewModel;

                int index = this.Requests.IndexOf(input);
                this.Requests.Insert(index + 1, newObj);
                AppDataHelper.SaveRequestData(this);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
        }

        /// <summary>
        /// Executes DeleteRequest
        /// </summary>
        private void ExecuteDeleteRequest(TransactionViewModel input)
        {
            bool delete = WindowServiceHandler.RaiseConfirmationBoxEvent("Delete", "Are you sure you want to delete this request?");

            if (delete && this.Requests != null)
            {
                this.Requests.Remove(input);

                AppDataHelper.SaveRequestData(this);
            }
        }

        /// <summary>
        /// Executes RenameCategory
        /// </summary>
        private void ExecuteRenameCategory(bool isRenaming)
        {
            try
            {
                this.IsRenaming = isRenaming;
                if (isRenaming == false)
                {
                    AppDataHelper.SaveRequestData(this);
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        #endregion

        #endregion
    }
}
