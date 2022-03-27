// <copyright file="SaveRequestViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>15-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using DataLibrary;
    using RestClientLibrary.Common;

    /// <summary>
    /// Defines the <see cref="SaveRequestViewModel" />
    /// </summary>
    public class SaveRequestViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// Defines the 
        /// </summary>
        private bool _addToCategory = true;

        /// <summary>
        /// Defines the 
        /// </summary>
        private ObservableCollection<CategoryViewModel> _categories;

        /// <summary>
        /// Defines the 
        /// </summary>
        private SaveRequestDataViewModel _data;

        /// <summary>
        /// Defines the 
        /// </summary>
        private string _name;

        /// <summary>
        /// Defines the 
        /// </summary>
        private string _newCategoryName;

        /// <summary>
        /// Defines the 
        /// </summary>
        private bool _newCategoryVisible;

        /// <summary>
        /// Defines the 
        /// </summary>
        private CategoryViewModel _selectedCategory;

        /// <summary>
        /// Defines the 
        /// </summary>
        private View.ISaveRequestView _view;

        #region Commands

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand _addToCategoryChangedCommand;

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand _cancelClickedCommand;

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand _categoryChangedCommand;

        /// <summary>
        /// Defines the 
        /// </summary>
        private RelayCommand _saveClickedCommand;

        /// <summary>
        /// The cloneCategoryCommand field
        /// </summary>
        private RelayCommand<CategoryViewModel> cloneCategoryCommand;

        /// <summary>
        /// The deleteCategoryCommand field
        /// </summary>
        private RelayCommand<CategoryViewModel> deleteCategoryCommand;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveRequestViewModel"/> class.
        /// </summary>
        public SaveRequestViewModel()
        {
            LoadData();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether AddToCategory
        /// </summary>
        public bool AddToCategory
        {
            get { return _addToCategory; }
            set
            {
                _addToCategory = value;
                OnPropertyChanged("AddToCategory");
            }
        }

        /// <summary>
        /// Gets or sets the Categories
        /// </summary>
        public ObservableCollection<CategoryViewModel> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        /// <summary>
        /// Gets or sets the Data
        /// </summary>
        public SaveRequestDataViewModel Data
        {
            get { return _data; }
            set
            {
                _data = value;
                LoadCategories();
                OnPropertyChanged("Data");
            }
        }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets the NewCategoryName
        /// </summary>
        public string NewCategoryName
        {
            get { return _newCategoryName; }
            set
            {
                _newCategoryName = value;
                OnPropertyChanged("NewCategoryName");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether NewCategoryVisible
        /// </summary>
        public bool NewCategoryVisible
        {
            get { return _newCategoryVisible; }
            set
            {
                _newCategoryVisible = value;
                OnPropertyChanged("NewCategoryVisible");
            }
        }

        /// <summary>
        /// Gets or sets the ParentViewModel
        /// </summary>
        public WorkspaceViewModel ParentViewModel
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the SelectedCategory
        /// </summary>
        public CategoryViewModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        /// <summary>
        /// Gets or sets the Transaction
        /// </summary>
        public TransactionViewModel Transaction
        {
            get; set;
        }

        #region Commands

        /// <summary>
        /// Gets or sets the AddToCategoryChangedCommand
        /// </summary>
        public RelayCommand AddToCategoryChangedCommand
        {
            get
            {
                if (_addToCategoryChangedCommand == null)
                {
                    _addToCategoryChangedCommand = new RelayCommand(command => AddToCategoryChanged());
                }
                return _addToCategoryChangedCommand;
            }
            set { _addToCategoryChangedCommand = value; }
        }

        /// <summary>
        /// Gets or sets the CancelClickedCommand
        /// </summary>
        public RelayCommand CancelClickedCommand
        {
            get
            {
                if (_cancelClickedCommand == null)
                {
                    _cancelClickedCommand = new RelayCommand(command => CancelClicked());
                }
                return _cancelClickedCommand;
            }
            set { _cancelClickedCommand = value; }
        }

        /// <summary>
        /// Gets or sets the CategoryChangedCommand
        /// </summary>
        public RelayCommand CategoryChangedCommand
        {
            get
            {
                if (_categoryChangedCommand == null)
                {
                    _categoryChangedCommand = new RelayCommand(command => CategoryChanged());
                }
                return _categoryChangedCommand;
            }
            set { _categoryChangedCommand = value; }
        }

        /// <summary>
        /// Gets the CloneCategoryCommand
        /// </summary>
        public RelayCommand<CategoryViewModel> CloneCategoryCommand
        {
            get
            {
                if (this.cloneCategoryCommand == null)
                {
                    this.cloneCategoryCommand = new RelayCommand<CategoryViewModel>(command => this.ExecuteCloneCategory(command));
                }

                return this.cloneCategoryCommand;
            }
        }

        /// <summary>
        /// Gets the DeleteCategoryCommand
        /// </summary>
        public RelayCommand<CategoryViewModel> DeleteCategoryCommand
        {
            get
            {
                if (this.deleteCategoryCommand == null)
                {
                    this.deleteCategoryCommand = new RelayCommand<CategoryViewModel>(command => this.ExecuteDeleteCategory(command));
                }

                return this.deleteCategoryCommand;
            }
        }

        /// <summary>
        /// Gets or sets the SaveClickedCommand
        /// </summary>
        public RelayCommand SaveClickedCommand
        {
            get
            {
                if (_saveClickedCommand == null)
                {
                    _saveClickedCommand = new RelayCommand(command => SaveClicked(), can => CanSaveEnabled());
                }
                return _saveClickedCommand;
            }
            set { _saveClickedCommand = value; }
        }

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The AttachView
        /// </summary>
        /// <param name="saveRequestView">The <see cref="View.ISaveRequestView"/></param>
        public void AttachView(View.ISaveRequestView saveRequestView)
        {
            _view = saveRequestView;
        }

        /// <summary>
        /// The LoadData
        /// </summary>
        public void LoadData()
        {
            LoadCategories();
        }

        /// <summary>
        /// The LoadData
        /// </summary>
        /// <param name="saveRequestData">The <see cref="SaveRequestDataViewModel"/></param>
        public void LoadData(SaveRequestDataViewModel saveRequestData)
        {
            this.Data = saveRequestData;

            if (this.Data.Categories != null)
            {
                foreach (var item in this.Data.Categories)
                {
                    if (item.Requests != null)
                    {
                        foreach (var re in item.Requests)
                        {
                            re.ParentViewModel = this.ParentViewModel;
                        }

                        item.IsExpanded = true;
                    }
                }
            }

            if (this.Data.Requests != null)
            {
                foreach (var re in this.Data.Requests)
                {
                    re.ParentViewModel = this.ParentViewModel;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The AddToCategoryChanged
        /// </summary>
        private void AddToCategoryChanged()
        {
            try
            {
                if (AddToCategory)
                {
                }
                else
                {
                    NewCategoryName = null;
                    SelectedCategory = Categories[0];
                }

            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// The BeginSaveRequest
        /// </summary>
        /// <param name="ctg">The <see cref="CategoryViewModel"/></param>
        /// <param name="currReq">The <see cref="TransactionViewModel"/></param>
        private void BeginSaveRequest(CategoryViewModel ctg, TransactionViewModel currReq)
        {
            if (ctg == null)
            {
                if (Data.Requests == null)
                {
                    Data.Requests = new ObservableCollection<TransactionViewModel>();
                }
                Data.Requests.Add(currReq);
            }
            else
            {
                CategoryViewModel currCat = null;
                if (Data.Categories != null)
                {
                    currCat = Data.Categories.FirstOrDefault(x => x.Name == ctg.Name);
                }
                else
                {
                    Data.Categories = new ObservableCollection<CategoryViewModel>();
                }
                if (currCat == null)
                {
                    currCat = ctg;
                    Data.Categories.Add(currCat);
                }

                if (currCat.Requests == null)
                {
                    currCat.Requests = new ObservableCollection<TransactionViewModel>();
                }
                if (currCat.Requests.Any(x => x.Name == currCat.Name))
                {
                    var oldCat = currCat.Requests.First(x => x.Name == currCat.Name);
                    currCat.Requests.Remove(oldCat);
                }
                currReq.CategoryName = currCat.Name;
                currCat.Requests.Add(currReq);
            }

            AppDataHelper.SaveRequestData(Data);
        }

        /// <summary>
        /// The CancelClicked
        /// </summary>
        private void CancelClicked()
        {
            try
            {
                if (_view != null)
                {
                    CloseWindow();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// The CanSaveEnabled
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        private bool CanSaveEnabled()
        {
            if (string.IsNullOrEmpty(Name))
                return false;

            if (AddToCategory && SelectedCategory != null && SelectedCategory.Name == Constants.Select)
                return false;

            if (AddToCategory && SelectedCategory != null && SelectedCategory.Name == Constants.AddNew && string.IsNullOrEmpty(NewCategoryName))
                return false;

            return true;
        }

        /// <summary>
        /// The CategoryChanged
        /// </summary>
        private void CategoryChanged()
        {
            try
            {
                if (SelectedCategory != null && SelectedCategory.Name == Constants.AddNew)
                {
                    NewCategoryVisible = true;
                }
                else
                {
                    NewCategoryVisible = false;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// The ClearData
        /// </summary>
        private void ClearData()
        {
            AddToCategory = true;
            Name = null;
            NewCategoryName = null;
            SelectedCategory = Categories[0];
        }

        /// <summary>
        /// The CloseWindow
        /// </summary>
        private void CloseWindow()
        {
            ClearData();
            _view.CloseParentWindow();
        }

        /// <summary>
        /// Executes CloneCategory
        /// </summary>
        private void ExecuteCloneCategory(CategoryViewModel input)
        {
            try
            {
                CategoryViewModel newObj = new CategoryViewModel();
                newObj.Guid = System.Guid.NewGuid().ToString();
                newObj.IsExpanded = false;
                newObj.Name = input.Name + " - clone";
                if (input.Requests != null)
                {
                    newObj.Requests = new ObservableCollection<TransactionViewModel>(input.Requests);
                }

                if (this.Data == null)
                {
                    this.Data = new SaveRequestDataViewModel();
                }
                if (this.Data.Categories == null)
                {
                    this.Data.Categories = new ObservableCollection<CategoryViewModel>();
                }

                var index = this.Data.Categories.IndexOf(input);

                this.Data.Categories.Insert(index + 1, newObj);

                AppDataHelper.SaveRequestData(this.Data);
            }
            catch (Exception ex)
            {
                _view.MessageShow("Error", ex.Message);
                LogException(ex);
            }
        }

        /// <summary>
        /// Executes DeleteCategory
        /// </summary>
        private void ExecuteDeleteCategory(CategoryViewModel input)
        {
            bool delete = WindowServiceHandler.RaiseConfirmationBoxEvent("Delete", "Are you sure you want to delete this Category?");

            if (delete)
            {
                if (this.Data != null && this.Data.Categories != null)
                {
                    this.Data.Categories.Remove(input);
                }

                AppDataHelper.SaveRequestData(this.Data);
            }
        }

        /// <summary>
        /// The LoadCategories
        /// </summary>
        private void LoadCategories()
        {
            List<CategoryViewModel> ctgs = new List<CategoryViewModel>();
            if (Data != null && Data.Categories != null)
            {
                foreach (CategoryViewModel c in Data.Categories)
                {
                    ctgs.Add(new CategoryViewModel() { Name = c.Name });
                }
            }

            ctgs.Insert(0, new CategoryViewModel() { Name = Constants.Select });
            ctgs.Add(new CategoryViewModel() { Name = Constants.AddNew });
            Categories = new ObservableCollection<CategoryViewModel>(ctgs);

            SelectedCategory = Categories[0];
        }

        /// <summary>
        /// The SaveClicked
        /// </summary>
        private void SaveClicked()
        {
            try
            {
                CategoryViewModel ctg = null;
                if (AddToCategory && SelectedCategory != null)
                {
                    if (SelectedCategory.Name == Constants.AddNew)
                    {
                        ctg = new CategoryViewModel();
                        ctg.Name = NewCategoryName;
                    }
                    else
                    {
                        ctg = SelectedCategory;
                    }
                }

                TransactionViewModel req = this.Transaction;

                if (req == null)
                {
                    req = new TransactionViewModel();
                }

                req.ParentViewModel = this.ParentViewModel;
                req.Name = Name;
                BeginSaveRequest(ctg, req);
                //MasterEventHandler.RaiseSaveRequestInitiatedEvent(ctg, req);
                CloseWindow();
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
