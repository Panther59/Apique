namespace RestClientLibrary.ViewModel.Automations
{
    using System.Collections.Generic;
    using RestClientLibrary.Model;

    /// <summary>
    /// Defines the <see cref="RestClientPreExecutionAutomation" />
    /// </summary>
    public class RestClientPreExecutionAutomation : RestClientBaseExecutionAutomation, IRestClientPreExecutionAutomation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestClientPreExecutionAutomation"/> class.
        /// </summary>
        /// <param name="variableData">The <see cref="GlobalVariableModel"/></param>
        /// <param name="selectedEnvironment">The <see cref="string"/></param>
        public RestClientPreExecutionAutomation(GlobalSetupViewModel variableData, string selectedEnvironment) : base(variableData, selectedEnvironment)
        {
        }
    }
}
