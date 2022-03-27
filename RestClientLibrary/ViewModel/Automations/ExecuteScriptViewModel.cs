// <copyright file="ExecuteScriptViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>06-08-2017</date>

namespace RestClientLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Emit;

    #region Interfaces

    /// <summary>
    /// Defines the <see cref="IRestClientBaseExecutionAutomation" />
    /// </summary>
    public interface IRestClientBaseExecutionAutomation
    {
        #region Methods

        /// <summary>
        /// The ClearEnvironmentVariable
        /// </summary>
        /// <param name="variableName">The <see cref="string"/></param>
        void ClearEnvironmentVariable(string variableName);

        /// <summary>
        /// The ClearGlobalVariable
        /// </summary>
        /// <param name="variableName">The <see cref="string"/></param>
        void ClearGlobalVariable(string variableName);

        /// <summary>
        /// The GetUsedVariable
        /// </summary>
        /// <returns>The <see cref="List{string}"/></returns>
        List<string> GetUsedVariable();

        /// <summary>
        /// The GetVariablesList
        /// </summary>
        /// <returns>The <see cref="List{KeyValuePair{string, string}}"/></returns>
        List<KeyValuePair<string, string>> GetVariablesList();

        /// <summary>
        /// The GetVariableValue
        /// </summary>
        /// <param name="variableName">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        string GetVariableValue(string variableName);

        /// <summary>
        /// The SetEnvironmentVariable
        /// </summary>
        /// <param name="variableName">The <see cref="string"/></param>
        /// <param name="value">The <see cref="string"/></param>
        void SetEnvironmentVariable(string variableName, string value);

        /// <summary>
        /// The SetGlobalVariable
        /// </summary>
        /// <param name="variableName">The <see cref="string"/></param>
        /// <param name="value">The <see cref="string"/></param>
        void SetGlobalVariable(string variableName, string value);

        #endregion
    }

    /// <summary>
    /// Defines the <see cref="IRestClientPostExecutionAutomation" />
    /// </summary>
    public interface IRestClientPostExecutionAutomation : IRestClientBaseExecutionAutomation
    {
        #region Methods

        /// <summary>
        /// The GetResonseHeader
        /// </summary>
        /// <param name="key">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        string GetResonseHeader(string key);

        /// <summary>
        /// The GetResonseHeaders
        /// </summary>
        /// <returns>The <see cref="List{KeyValuePair{string, string}}"/></returns>
        List<KeyValuePair<string, string>> GetResonseHeaders();

        /// <summary>
        /// The GetResponseContent
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        string GetResponseContent();

        /// <summary>
        /// The GetResponsePropertyValue
        /// </summary>
        /// <param name="responsePath">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        string GetResponsePropertyValue(string responsePath);

        /// <summary>
        /// The GetResponseSize
        /// </summary>
        /// <returns>The <see cref="long?"/></returns>
        long? GetResponseSize();

        /// <summary>
        /// The GetResponseStatusCode
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        int GetResponseStatusCode();

        /// <summary>
        /// The GetResponseStatusDescription
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        string GetResponseStatusDescription();

        /// <summary>
        /// The GetResponseTime
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        int GetResponseTime();

        /// <summary>
        /// The IsCallSuccessful
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        bool IsCallSuccessful();

        #endregion
    }

    /// <summary>
    /// Defines the <see cref="IRestClientPreExecutionAutomation" />
    /// </summary>
    public interface IRestClientPreExecutionAutomation : IRestClientBaseExecutionAutomation
    {
    }

    /// <summary>
    /// Defines the <see cref="IRestClientValidationsAutomation" />
    /// </summary>
    public interface IRestClientValidationsAutomation : IRestClientPostExecutionAutomation
    {
        #region Methods

        /// <summary>
        /// The Validate
        /// </summary>
        /// <param name="testName">The <see cref="string"/></param>
        /// <param name="expectedValue">The <see cref="string"/></param>
        /// <param name="actualValue">The <see cref="string"/></param>
        /// <param name="compareMode">The <see cref="CompareMode"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool Validate(string testName, string expectedValue, string actualValue, CompareMode compareMode);

        /// <summary>
        /// The ValidateCondition
        /// </summary>
        /// <param name="testName">The <see cref="string"/></param>
        /// <param name="condition">The <see cref="bool"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool ValidateCondition(string testName, bool condition);

        /// <summary>
        /// The ValidateHeader
        /// </summary>
        /// <param name="testName">The <see cref="string"/></param>
        /// <param name="headerKey">The <see cref="string"/></param>
        /// <param name="headerValue">The <see cref="string"/></param>
        /// <param name="compareMode">The <see cref="CompareMode"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool ValidateHeader(string testName, string headerKey, string headerValue, CompareMode compareMode);

        /// <summary>
        /// The ValidateResponseContent
        /// </summary>
        /// <param name="testName">The <see cref="string"/></param>
        /// <param name="expectedContent">The <see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool ValidateResponseContent(string testName, string expectedContent);

        /// <summary>
        /// The ValidateResponseField
        /// </summary>
        /// <param name="testName">The <see cref="string"/></param>
        /// <param name="fieldPath">The <see cref="string"/></param>
        /// <param name="expectedValue">The <see cref="string"/></param>
        /// <param name="compareMode">The <see cref="CompareMode"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool ValidateResponseField(string testName, string fieldPath, string expectedValue, CompareMode compareMode);

        /// <summary>
        /// The ValidateStatusCode
        /// </summary>
        /// <param name="testName">The <see cref="string"/></param>
        /// <param name="expectedStatusCode">The <see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        bool ValidateStatusCode(string testName, int expectedStatusCode);

        #endregion
    }

    #endregion

    /// <summary>
    /// Defines the <see cref="ExecuteScriptViewModel" />
    /// </summary>
    public class ExecuteScriptViewModel
    {
        #region Methods

        #region Public Methods

        /// <summary>
        /// The ExecutePostCode
        /// </summary>
        /// <param name="code">The <see cref="string"/></param>
        /// <param name="restClient">The <see cref="IRestClientPostExecutionAutomation"/></param>
        public void ExecutePostCode(string code, IRestClientPostExecutionAutomation restClient)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ExecuteScriptViewModel).Assembly.Location)
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());

                    Type type = assembly.GetType("RestClientLibrary.RuntimeExecution");
                    object obj = Activator.CreateInstance(type);
                    type.InvokeMember("PostExecution",
                        BindingFlags.Default | BindingFlags.InvokeMethod,
                        null,
                        obj,
                        new object[] { restClient });
                }
            }
        }

        /// <summary>
        /// The ExecutePreCode
        /// </summary>
        /// <param name="code">The <see cref="string"/></param>
        /// <param name="restClient">The <see cref="IRestClientPreExecutionAutomation"/></param>
        public void ExecutePreCode(string code, IRestClientPreExecutionAutomation restClient)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ExecuteScriptViewModel).Assembly.Location)
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());

                    Type type = assembly.GetType("RestClientLibrary.RuntimeExecution");
                    object obj = Activator.CreateInstance(type);
                    type.InvokeMember("PreExecution",
                        BindingFlags.Default | BindingFlags.InvokeMethod,
                        null,
                        obj,
                        new object[] { restClient });
                }
            }
        }

        /// <summary>
        /// The ExecuteValidations
        /// </summary>
        /// <param name="code">The <see cref="string"/></param>
        /// <param name="restClient">The <see cref="IRestClientPreExecutionAutomation"/></param>
        public void ExecuteValidations(string code, IRestClientValidationsAutomation restClient)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ExecuteScriptViewModel).Assembly.Location)
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());

                    Type type = assembly.GetType("RestClientLibrary.RuntimeExecution");
                    object obj = Activator.CreateInstance(type);
                    type.InvokeMember("Validations",
                        BindingFlags.Default | BindingFlags.InvokeMethod,
                        null,
                        obj,
                        new object[] { restClient });
                }
            }
        }

        #endregion

        #endregion
    }
}
