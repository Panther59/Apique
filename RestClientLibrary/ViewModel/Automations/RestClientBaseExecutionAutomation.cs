// <copyright file="RestClientBaseExecutionAutomation.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>06-08-2017</date>

namespace RestClientLibrary.ViewModel.Automations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RestClientLibrary.Model;

    /// <summary>
    /// Defines the <see cref="RestClientBaseExecutionAutomation" />
    /// </summary>
    public class RestClientBaseExecutionAutomation : IRestClientBaseExecutionAutomation
    {
        #region Fields

        /// <summary>
        /// Defines the 
        /// </summary>
        private string selectedEnvironment;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClientBaseExecutionAutomation"/> class.
        /// </summary>
        /// <param name="variableData">The <see cref="GlobalVariableModel"/></param>
        /// <param name="selectedEnvironment">The <see cref="string"/></param>
        public RestClientBaseExecutionAutomation(
            GlobalVariableModel variableData,
            string selectedEnvironment)
        {
            this.GlobalVariableData = variableData;
            this.selectedEnvironment = selectedEnvironment;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Log
        /// </summary>
        public string Log
        {
            get; private set;
        }

        /// <summary>
        /// Gets or sets the GlobalVariableData
        /// </summary>
        public GlobalVariableModel GlobalVariableData
        {
            get; private set;
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <inheritdoc/>
        public void ClearEnvironmentVariable(string variableName)
        {
            this.SetEnvironmentVariable(variableName, string.Empty);
        }

        /// <inheritdoc/>
        public void ClearGlobalVariable(string variableName)
        {
            this.SetGlobalVariable(variableName, string.Empty);
        }

        /// <inheritdoc/>
        public List<string> GetUsedVariable()
        {
            return null;
        }

        /// <inheritdoc/>
        public List<KeyValuePair<string, string>> GetVariablesList()
        {
            var env = this.GetCurrentEnvironment();
            List<KeyValuePair<string, string>> list = env?.Variables?.Select(x => new KeyValuePair<string, string>(x.Key, x.Value)).ToList();
            var globalList = this.GlobalVariableData?.Variables?
                .Where(x => list.Any(y => y.Key.Equals(x.Key, StringComparison.CurrentCultureIgnoreCase)) == false)
                .Select(x => new KeyValuePair<string, string>(x.Key, x.Value)).ToList();

            list?.AddRange(globalList);

            return list;
        }

        /// <inheritdoc/>
        public string GetVariableValue(string variableName)
        {
            KeyValueModel variable = null;
            var env = this.GetCurrentEnvironment();
            if (env != null && env.Variables != null)
            {
                variable = env.Variables.FirstOrDefault(x => this.VariableCompareExpression(x, variableName));
            }

            if (variable != null)
            {
                return variable.Value;
            }

            variable = this.GlobalVariableData?.Variables?.FirstOrDefault(x => this.VariableCompareExpression(x, variableName));

            return variable?.Value;
        }

        /// <inheritdoc/>
        public void SetEnvironmentVariable(string variableName, string value)
        {
            var env = this.GetCurrentEnvironment();
            if (env == null)
            {
                this.WriteLog("This step was skipped as no environment was selected");
                return;
            }
            else
            {
                var variable = env.Variables?.FirstOrDefault(x => x.Key == variableName);

                if (variable != null)
                {
                    variable.Value = value;
                    this.WriteLog(string.Format("Value of the variable '{0}' was set to '{2}' in '{1}' environment", variableName, env.Name, value));
                }
                else
                {
                    this.WriteLog(string.Format("This step was skipped as '{0}' was not found in '{1}' environment", variableName, env.Name));
                }
            }
        }

        /// <inheritdoc/>
        public void SetGlobalVariable(string variableName, string value)
        {
            var variable = GlobalVariableData?.Variables?.FirstOrDefault(x => this.VariableCompareExpression(x, variableName));

            if (variable != null)
            {
                variable.Value = value;
                this.WriteLog(string.Format("Value of the variable '{0}' was set to '{1}' in global variables", variableName, value));
            }
            else
            {
                this.WriteLog(string.Format("This step was skipped as '{0}' was not found in global variables", variableName));
            }
        }

        /// <summary>
        /// The WriteLog
        /// </summary>
        /// <param name="message">The <see cref="string"/></param>
        public void WriteLog(string message)
        {
            this.Log += message;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The GetCurrentEnvironment
        /// </summary>
        /// <returns>The <see cref="EnvironmentModel"/></returns>
        private EnvironmentModel GetCurrentEnvironment()
        {
            if (selectedEnvironment == null)
            {
                return null;
            }

            if (GlobalVariableData != null && GlobalVariableData.Environments != null)
            {
                var env = GlobalVariableData.Environments.FirstOrDefault(x => x.Name == selectedEnvironment);
                return env;
            }

            return null;
        }

        /// <summary>
        /// The VariableCompareExpression
        /// </summary>
        /// <param name="arg">The <see cref="KeyValueModel"/></param>
        /// <param name="variableName">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool VariableCompareExpression(KeyValueModel arg, string variableName)
        {
            return arg.Key.Equals(variableName, System.StringComparison.CurrentCultureIgnoreCase);
        }

        #endregion

        #endregion
    }
}
