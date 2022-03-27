// <copyright file="Constants.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>11-08-2017</date>

namespace RestClientLibrary.Common
{
    /// <summary>
    /// Defines the <see cref="Constants" />
    /// </summary>
    public class Constants
    {
        #region Fields

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string AddNew = "-- Add New --";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string AutomationScriptEndCode = @"        }
    }
}";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string AutomationScriptIndentation = "            ";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string DELETE = "DELETE";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string GET = "GET";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string None = "-- None --";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string NoResponseContent = "Response does not contain any data.";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string POST = "POST";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string PostAutomationScriptStartCode = @"namespace RestClientLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class RuntimeExecution
    {
        public void PostExecution(IRestClientPostExecutionAutomation restClient)
        {
";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string PreAutomationScriptStartCode = @"namespace RestClientLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class RuntimeExecution
    {
        public void PreExecution(IRestClientPreExecutionAutomation restClient)
        {
";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string PUT = "PUT";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string Select = "-- Select --";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string SetAction = "Set";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string UseAction = "Use";

        /// <summary>
        /// Defines the 
        /// </summary>
        public const string ValidationScriptStartCode = @"namespace RestClientLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class RuntimeExecution
    {
        public void Validations(IRestClientValidationsAutomation restClient)
        {
";

        public const string ExportImportFilter = "Rest Client Data|*.rcd";
        public const string ImportCertFilter = "Certificate|*.p12;*.pfx";

        #endregion

        #region Enums

        /// <summary>
        /// Defines the MainTab
        /// </summary>
        public enum MainTab
        {
            /// <summary>
            /// Defines the RestClient
            /// </summary>
            RestClient = 0,
            /// <summary>
            /// Defines the History
            /// </summary>
            History = 1,
            /// <summary>
            /// Defines the Settings
            /// </summary>
            Settings = 2,
            /// <summary>
            /// Defines the SavedRequest
            /// </summary>
            SavedRequest = 3,
            /// <summary>
            /// Defines the TestCase
            /// </summary>
            TestCase = 4,
            /// <summary>
            /// Defines the TestSuite
            /// </summary>
            TestSuite = 5,
            /// <summary>
            /// Defines the TestClient
            /// </summary>
            TestClient = 6,
        }

        /// <summary>
        /// Defines the ResponseTab
        /// </summary>
        public enum ResponseTab
        {
            /// <summary>
            /// Defines the Json
            /// </summary>
            Json = 0,
            /// <summary>
            /// Defines the Xml
            /// </summary>
            Xml = 1,
            /// <summary>
            /// Defines the Html
            /// </summary>
            Html = 2,
            /// <summary>
            /// Defines the Raw
            /// </summary>
            Raw = 3,
            /// <summary>
            /// Defines the Header
            /// </summary>
            Header = 4
        }

        /// <summary>
        /// Defines the HeaderTab
        /// </summary>
        public enum HeaderTab
        {
            /// <summary>
            /// Defines the Raw
            /// </summary>
            Raw = 0,
            /// <summary>
            /// Defines the Form
            /// </summary>
            Form = 1,
            /// <summary>
            /// Defines the Authorization
            /// </summary>
            Authorization = 2,
        }

        #endregion
    }
}
