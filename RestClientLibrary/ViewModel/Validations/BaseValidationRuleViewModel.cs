// <copyright file="BaseValidationRuleViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.ViewModel.Validations
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using DataLibrary;
    using RestClientLibrary.Common;
    using RestClientLibrary.Model;

    /// <summary>
    /// Defines the <see cref="BaseValidationRuleViewModel" />
    /// </summary>
    public abstract class BaseValidationRuleViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// Defines the 
        /// </summary>
        private ResultState _result;

        /// <summary>
        /// Defines the 
        /// </summary>
        private string _resultText;

        /// <summary>
        /// The ruleName field
        /// </summary>
        private string ruleName;

        /// <summary>
        /// The type field
        /// </summary>
        private ValidationType type;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public ValidationType Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
                this.OnPropertyChanged("Type");
            }
        }

        /// <summary>
        /// Gets or sets the Result
        /// </summary>
        public ResultState Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        /// <summary>
        /// Gets or sets the ResultText
        /// </summary>
        public string ResultText
        {
            get { return _resultText; }
            set
            {
                _resultText = value;
                OnPropertyChanged("ResultText");
            }
        }

        /// <summary>
        /// Gets or sets the RuleName
        /// </summary>
        public string RuleName
        {
            get
            {
                return this.ruleName;
            }

            set
            {
                this.ruleName = value;
                this.OnPropertyChanged("RuleName");
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The GetInstance
        /// </summary>
        /// <param name="type">The <see cref="ValidationType"/></param>
        /// <returns>The <see cref="BaseValidationRuleViewModel"/></returns>
        public static BaseValidationRuleViewModel GetInstance(ValidationType type)
        {
            switch (type)
            {
                case ValidationType.Status:
                    return new StatusCodeValidationRuleViewModel();
                case ValidationType.ResponseContent:
                    return new ResponseContainsTextRuleViewModel();
                case ValidationType.ResponseField:
                    return new ResponseFieldTextRuleViewModel();
                case ValidationType.Headers:
                    return new HeaderPresentRuleViewModel();
                default:
                    return null;
            }
        }

        /// <summary>
        /// The Parse
        /// </summary>
        /// <param name="input">The <see cref="IEnumerable{BaseValidationRuleModel}"/></param>
        /// <returns>The <see cref="List{BaseValidationRuleViewModel}"/></returns>
        public static List<BaseValidationRuleViewModel> Parse(IEnumerable<BaseValidationRuleModel> input)
        {
            if (input == null)
            {
                return null;
            }

            List<BaseValidationRuleViewModel> output = new List<BaseValidationRuleViewModel>();
            foreach (var item in input)
            {
                BaseValidationRuleViewModel obj = Parse(item);
                output.Add(obj);
            }

            return output;
        }

        /// <summary>
        /// The Parse
        /// </summary>
        /// <param name="list">The <see cref="List{Model.ValidationRuleModel}"/></param>
        /// <returns>The <see cref="ObservableCollection{ValidationRuleViewModel}"/></returns>
        public static ObservableCollection<BaseValidationRuleViewModel> Parse(List<Model.ValidationRuleModel> list)
        {
            if (list == null)
            {
                return null;
            }

            var output = new List<BaseValidationRuleViewModel>();

            foreach (var item in list)
            {
                output.Add(BaseValidationRuleViewModel.Parse(item));
            }

            return new ObservableCollection<BaseValidationRuleViewModel>(output);
        }

        /// <summary>
        /// The Parse
        /// </summary>
        /// <param name="model">The <see cref="ValidationRuleModel"/></param>
        /// <returns>The <see cref="BaseValidationRuleViewModel"/></returns>
        public static BaseValidationRuleViewModel Parse(ValidationRuleModel model)
        {
            if (model == null)
            {
                return null;
            }

            return null;
        }

        /// <summary>
        /// The Validate
        /// </summary>
        /// <param name="restResponse">The <see cref="RestResponse"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public abstract bool Validate(RestResponse restResponse);

        #endregion

        #region Private Methods

        /// <summary>
        /// The Parse
        /// </summary>
        /// <param name="input">The <see cref="BaseValidationRuleModel"/></param>
        /// <returns>The <see cref="BaseValidationRuleViewModel"/></returns>
        private static BaseValidationRuleViewModel Parse(BaseValidationRuleModel input)
        {
            if (input == null)
            {
                return null;
            }

            switch (input.Type)
            {
                case ValidationType.Status:
                    return StatusCodeValidationRuleViewModel.ParseModel(input as StatusCodeValidationRuleModel);
                case ValidationType.ResponseContent:
                    return ResponseContainsTextRuleViewModel.ParseModel(input as ResponseContainsTextRuleModel);
                case ValidationType.ResponseField:
                    return ResponseFieldTextRuleViewModel.ParseModel(input as ResponseFieldTextRuleModel);
                case ValidationType.Headers:
                    return HeaderPresentRuleViewModel.ParseModel(input as HeaderPresentRuleModel);
                default:
                    return null;
            }
        }

        #endregion

        #endregion
    }
}
