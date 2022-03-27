// <copyright file="ValidationsViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using DataLibrary;
    using RestClientLibrary.Common;
    using RestClientLibrary.Model;
    using RestClientLibrary.ViewModel.Automations;
    using RestClientLibrary.ViewModel.Validations;

    /// <summary>
    /// Defines the <see cref="ValidationsViewModel" />
    /// </summary>
    public class ValidationsViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// The isValidationSetup field
        /// </summary>
        private bool isValidationSetup;

        ///// <summary>
        ///// The result field
        ///// </summary>
        //private ResultState result;

        ///// <summary>
        ///// The test field
        ///// </summary>
        //private string test;

        ///// <summary>
        ///// Defines the 
        ///// </summary>
        //private ObservableCollection<BaseValidationRuleViewModel> validations;

        #region Commands

        ///// <summary>
        ///// Defines the 
        ///// </summary>
        //private RelayCommand<ValidationType> _addValidationCommand;

        ///// <summary>
        ///// The removeValidationCommand field
        ///// </summary>
        //private RelayCommand<BaseValidationRuleViewModel> removeValidationCommand;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationsViewModel"/> class.
        /// </summary>
        public ValidationsViewModel()
        {
            //this.Test = "Test";
        }

        #endregion

        #region Properties

        ///// <summary>
        ///// Gets or sets the Validations
        ///// </summary>
        //public ObservableCollection<BaseValidationRuleViewModel> Validations
        //{
        //    get { return validations; }
        //    set
        //    {
        //        validations = value;
        //        OnPropertyChanged("Validations");
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the Result
        ///// </summary>
        //public ResultState Result
        //{
        //    get
        //    {
        //        return this.result;
        //    }

        //    set
        //    {
        //        this.result = value;
        //        this.OnPropertyChanged("Result");
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the Test
        ///// </summary>
        //public string Test
        //{
        //    get
        //    {
        //        return this.test;
        //    }

        //    set
        //    {
        //        this.test = value;
        //        this.OnPropertyChanged("Test");
        //    }
        //}

        /// <summary>
        /// Gets or sets a value indicating whether IsValidationSetup
        /// </summary>
        public bool IsValidationSetup
        {
            get
            {
                return this.isValidationSetup;
            }

            set
            {
                this.isValidationSetup = value;
                this.OnPropertyChanged("IsValidationSetup");
            }
        }


        #region Commands

        ///// <summary>
        ///// Gets or sets the AddValidationCommand
        ///// </summary>
        //public RelayCommand<ValidationType> AddValidationCommand
        //{
        //    get
        //    {
        //        if (_addValidationCommand == null)
        //        {
        //            _addValidationCommand = new RelayCommand<ValidationType>(command => AddValidation(command));
        //        }
        //        return _addValidationCommand;
        //    }
        //    set { _addValidationCommand = value; }
        //}

        ///// <summary>
        ///// Gets the RemoveValidationRuleCommand
        ///// </summary>
        //public RelayCommand<BaseValidationRuleViewModel> RemoveValidationRuleCommand
        //{
        //    get
        //    {
        //        if (this.removeValidationCommand == null)
        //        {
        //            this.removeValidationCommand = new RelayCommand<BaseValidationRuleViewModel>(command => this.ExecuteRemoveValidationRule(command));
        //        }

        //        return this.removeValidationCommand;
        //    }
        //}

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The ClearValidations
        /// </summary>
        public void ClearValidations()
        {
            this.Validations = null;
            //this.Result = ResultState.NotStarted;
            this.CheckIfValidationSetup();
        }

        public bool? LoadValidations(List<ValidationResultModel> validations)
        {
            bool? result = null;
            this.ClearValidations();

            if (validations != null)
            {
                var outValidations = validations.Select(x => ValidationViewModel.Parse(x));
                this.Validations = new List<ValidationViewModel>(outValidations);
                this.CheckIfValidationSetup();
                result = this.Validations.Any(x => x.IsSuccess == false) ? false : true;
            }

            return result;
        }

        ///// <summary>
        ///// The ResetResult
        ///// </summary>
        //public void ResetResult()
        //{
        //    this.Result = ResultState.NotStarted;
        //    if (this.Validations != null)
        //    {
        //        foreach (var validation in this.Validations)
        //        {
        //            validation.Result = ResultState.NotStarted;
        //            validation.ResultText = string.Empty;
        //        }
        //    }

        //    this.CheckIfValidationSetup();
        //}

        /// <summary>
        /// The Validate
        /// </summary>
        /// <param name="response">The <see cref="RestResponse"/></param>
        /// <returns>The <see cref="bool"/></returns>
        //public bool? Validate(RestResponse response)
        //{
        //    bool? finalResult = null;
        //    if (this.Validations != null && this.Validations.Count > 0)
        //    {
        //        finalResult = true;
        //        foreach (var validation in this.Validations)
        //        {
        //            var result = validation.Validate(response);
        //            if (result == false)
        //            {
        //                finalResult = result;
        //            }
        //        }
        //    }

        //    this.Result = finalResult.HasValue ? (finalResult.Value ? ResultState.Pass : ResultState.Fail) : ResultState.NA;
        //    return finalResult;
        //}

        #endregion

        #region Private Methods

        /// <summary>
        /// The AddValidation
        /// </summary>
        //private void AddValidation(ValidationType type)
        //{
        //    try
        //    {
        //        if (Validations == null)
        //        {
        //            Validations = new ObservableCollection<BaseValidationRuleViewModel>();
        //        }

        //        Validations.Add(GetEmptyValidation(type));
        //        this.CheckIfValidationSetup();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.LogException(ex);
        //    }
        //}

        
        ///// <summary>
        ///// Executes RemoveValidationRule
        ///// </summary>
        //private void ExecuteRemoveValidationRule(BaseValidationRuleViewModel input)
        //{
        //    this.Validations.Remove(input);
        //    this.CheckIfValidationSetup();
        //}

        ///// <summary>
        ///// The GetEmptyValidation
        ///// </summary>
        ///// <returns>The <see cref="BaseValidationRuleViewModel"/></returns>
        //private BaseValidationRuleViewModel GetEmptyValidation(ValidationType type)
        //{
        //    BaseValidationRuleViewModel validation = BaseValidationRuleViewModel.GetInstance(type);
        //    return validation;
        //}

        #endregion

        #endregion
    }
}
