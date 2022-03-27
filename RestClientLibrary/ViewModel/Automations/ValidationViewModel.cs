// <copyright file="ValidationViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>06-08-2017</date>

namespace RestClientLibrary.ViewModel.Automations
{
    using DataLibrary;
    using RestClientLibrary.Model;

    /// <summary>
    /// Defines the <see cref="ValidationViewModel" />
    /// </summary>
    public class ValidationViewModel : BaseViewModel
    {
        #region Fields

        /// <summary>
        /// The actualValue field
        /// </summary>
        private string actualValue;

        /// <summary>
        /// The expectedValue field
        /// </summary>
        private string expectedValue;

        /// <summary>
        /// The isSuccess field
        /// </summary>
        private bool isSuccess;

        /// <summary>
        /// The testName field
        /// </summary>
        private string testName;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ActualValue
        /// </summary>
        public string ActualValue
        {
            get
            {
                return this.actualValue;
            }

            set
            {
                this.actualValue = value;
                this.OnPropertyChanged("ActualValue");
            }
        }

        /// <summary>
        /// Gets or sets the ExpectedValue
        /// </summary>
        public string ExpectedValue
        {
            get
            {
                return this.expectedValue;
            }

            set
            {
                this.expectedValue = value;
                this.OnPropertyChanged("ExpectedValue");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsSuccess
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return this.isSuccess;
            }

            set
            {
                this.isSuccess = value;
                this.OnPropertyChanged("IsSuccess");
            }
        }

        /// <summary>
        /// Gets or sets the TestName
        /// </summary>
        public string TestName
        {
            get
            {
                return this.testName;
            }

            set
            {
                this.testName = value;
                this.OnPropertyChanged("TestName");
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The Parse
        /// </summary>
        /// <param name="input">The <see cref="ValidationResultModel"/></param>
        /// <returns>The <see cref="ValidationViewModel"/></returns>
        public static ValidationViewModel Parse(ValidationResultModel input)
        {
            var output = new ValidationViewModel();
            output.ActualValue = input.ActualValue;
            output.ExpectedValue = input.ExpectedValue;
            output.IsSuccess = input.IsSuccess;
            output.TestName = input.TestName;

            return output;
        }

        #endregion

        #endregion
    }
}
