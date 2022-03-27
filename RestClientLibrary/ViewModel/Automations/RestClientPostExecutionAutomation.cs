namespace RestClientLibrary.ViewModel.Automations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataLibrary;
    using Newtonsoft.Json.Linq;
    using RestClientLibrary.Model;

    /// <summary>
    /// Defines the <see cref = "RestClientPostExecutionAutomation"/>
    /// </summary>
    public class RestClientPostExecutionAutomation : RestClientBaseExecutionAutomation, IRestClientPostExecutionAutomation
    {
        /// <summary>
        /// Defines the restresponse
        /// </summary>
        private RestResponse restresponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClientPostExecutionAutomation"/> class.
        /// </summary>
        /// <param name="variableData">The <see cref="GlobalVariableModel"/></param>
        /// <param name="selectedEnvironment">The <see cref="string"/></param>
        /// <param name="restresponse">The <see cref="RestResponse"/></param>
        public RestClientPostExecutionAutomation(GlobalVariableModel variableData, string selectedEnvironment, RestResponse restresponse) : base(variableData, selectedEnvironment)
        {
            this.restresponse = restresponse;
        }

        public string GetResonseHeader(string key)
        {
            var headers = this.GetResonseHeaders();
            var header = headers?.FirstOrDefault(x => x.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase));
            return header?.Value;
        }

        /// <inheritdoc/>
        public List<KeyValuePair<string, string>> GetResonseHeaders()
        {
            return restresponse.OutputHeader;
        }

        /// <inheritdoc/>
        public string GetResponseContent()
        {
            return restresponse.OutputContent;
        }

        /// <inheritdoc/>
        public string GetResponsePropertyValue(string responsePath)
        {
            string output = string.Empty;
            try
            {
                var obj = JObject.Parse(this.restresponse.OutputContent);
                output = (string)obj.SelectToken(responsePath);
            }
            catch (Exception ex)
            {
                ErrorLog.LogException(ex);
            }

            return output;
        }

        /// <inheritdoc/>
        public long? GetResponseSize()
        {
            return restresponse.ResponseSize;
        }

        /// <inheritdoc/>
        public int GetResponseStatusCode()
        {
            return restresponse.StatusCode;
        }

        /// <inheritdoc/>
        public string GetResponseStatusDescription()
        {
            return restresponse.StatusDescription;
        }

        /// <inheritdoc/>
        public int GetResponseTime()
        {
            return restresponse.Interval;
        }

        /// <inheritdoc/>
        public bool IsCallSuccessful()
        {
            return restresponse.IsSuccess;
        }
    }
}
