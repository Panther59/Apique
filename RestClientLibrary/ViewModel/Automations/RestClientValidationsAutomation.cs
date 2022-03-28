namespace RestClientLibrary.ViewModel.Automations
{
    using System;
    using System.Collections.Generic;
    using RestClientLibrary.Model;

    /// <summary>
    /// Defines the <see cref = "RestClientValidationsAutomation"/>
    /// </summary>
    public class RestClientValidationsAutomation : RestClientPostExecutionAutomation, IRestClientValidationsAutomation
    {
        /// <summary>
        /// Defines the restresponse
        /// </summary>
        private RestResponse restresponse;

        /// <summary>
        /// Gets or sets Results
        /// </summary>
        public List<ValidationResultModel> Results { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClientValidationsAutomation"/> class.
        /// </summary>
        /// <param name="variableData">The <see cref="GlobalVariableModel"/></param>
        /// <param name="selectedEnvironment">The <see cref="string"/></param>
        /// <param name="restresponse">The <see cref="RestResponse"/></param>
        public RestClientValidationsAutomation(GlobalSetupViewModel variableData, string selectedEnvironment, RestResponse restresponse) : base(variableData, selectedEnvironment, restresponse)
        {
            this.restresponse = restresponse;
        }

        /// <inheritdoc />
        public bool Validate(string testName, string expectedValue, string actualValue, CompareMode compareMode)
        {
            if (string.IsNullOrEmpty(testName))
            {
                testName = Guid.NewGuid().ToString();
            }

            var result = new ValidationResultModel();
            result.ActualValue = actualValue;
            result.ExpectedValue = expectedValue;
            result.TestName = testName;

            if (actualValue != null)
            {
                if (compareMode == CompareMode.Contains)
                {
                    result.IsSuccess = actualValue.Contains(expectedValue);
                }
                else
                {
                    result.IsSuccess = actualValue.Equals(expectedValue, StringComparison.CurrentCultureIgnoreCase);
                }
            }

            this.AddResult(result);
            return result.IsSuccess;
        }

        /// <inheritdoc />
        private void AddResult(ValidationResultModel result)
        {
            if (this.Results == null)
            {
                this.Results = new List<ValidationResultModel>();
            }

            this.Results.Add(result);
        }

        /// <inheritdoc />
        public bool ValidateHeader(string testName, string headerKey, string headerValue, CompareMode compareMode)
        {
            var actualHeaderValue = this.GetResonseHeader(headerKey);
            return this.Validate(testName, headerValue, actualHeaderValue, compareMode); 
        }

        /// <inheritdoc />
        public bool ValidateResponseContent(string testName, string expectedContent)
        {
            return this.Validate(testName, expectedContent, this.GetResponseContent(), CompareMode.Contains);
        }

        /// <inheritdoc />
        public bool ValidateResponseField(string testName, string fieldPath, string expectedValue, CompareMode compareMode)
        {
            var actualFieldValue = this.GetResponsePropertyValue(fieldPath);
            return this.Validate(testName, expectedValue, actualFieldValue, compareMode);
        }

        /// <inheritdoc />
        public bool ValidateStatusCode(string testName, int expectedStatusCode)
        {
            var actualStatusCode = this.GetResponseStatusCode();
            return this.Validate(testName, expectedStatusCode.ToString(), actualStatusCode.ToString(), CompareMode.Equals);
        }

        /// <inheritdoc />
        public bool ValidateCondition(string testName, bool condition)
        {
            var result = new ValidationResultModel();
            result.ActualValue = string.Empty;
            result.ExpectedValue = string.Empty;
            result.TestName = testName;
            result.IsSuccess = condition;
            this.AddResult(result);

            return condition;
        }
    }
}
