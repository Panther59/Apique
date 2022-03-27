// <copyright file="ValidationRuleModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>03-08-2017</date>

namespace RestClientLibrary.Model
{
    using System.Collections.Generic;
    using RestClientLibrary.Common;
    using RestClientLibrary.ViewModel.Validations;

    /// <summary>
    /// Defines the <see cref="ValidationRuleModel" />
    /// </summary>
    public class ValidationRuleModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public ValidationType Type
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the ExpectedValue
        /// </summary>
        public string ExpectedValue
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Value
        /// </summary>
        public string Value
        {
            get; set;
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The Parse
        /// </summary>
        /// <param name="vm">The <see cref="BaseValidationRuleViewModel"/></param>
        /// <returns>The <see cref="ValidationRuleModel"/></returns>
        public static ValidationRuleModel Parse(BaseValidationRuleViewModel vm)
        {
            if (vm == null)
            {
                return null;
            }

            return new ValidationRuleModel()
            {
                Type = vm.Type,
            };
        }

        /// <summary>
        /// The Parse
        /// </summary>
        /// <param name="inputList">The <see cref="System.Collections.ObjectModel.ObservableCollection{BaseValidationRuleViewModel}"/></param>
        /// <returns>The <see cref="List{ValidationRuleModel}"/></returns>
        public static List<ValidationRuleModel> Parse(System.Collections.ObjectModel.ObservableCollection<BaseValidationRuleViewModel> inputList)
        {
            if (inputList == null)
            {
                return null;
            }

            List<ValidationRuleModel> outputList = new List<ValidationRuleModel>();
            foreach (var input in inputList)
            {
                outputList.Add(ValidationRuleModel.Parse(input));
            }
            return outputList;
        }

        #endregion

        #endregion
    }
}
