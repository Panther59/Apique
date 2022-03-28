// <copyright file="TestRequestViewModel.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>09-08-2017</date>

namespace RestClientLibrary.ViewModel
{
    using RestClientLibrary.Common;
    using RestClientLibrary.Model;
    using RestClientLibrary.ViewModel.Automations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="TestRequestViewModel" />
    /// </summary>
    public class TestRequestViewModel : TransactionViewModel
    {
        #region Fields

        /// <summary>
        /// The result field
        /// </summary>
        private ResultState result;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Result
        /// </summary>
        public ResultState Result
        {
            get
            {
                return this.result;
            }

            set
            {
                this.result = value;
                this.OnPropertyChanged("Result");
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The Execute
        /// </summary>
        /// <param name="globalData">The <see cref="GlobalVariableModel"/></param>
        /// <param name="variables">The <see cref="List{KeyValueModel}"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task Execute(GlobalSetupViewModel globalData, string environmentName)
        {
            await Task.Run(() =>
            {
                List<KeyValueViewModel> variables = this.GetVariables(globalData, environmentName);
                var certs = this.GetCertificates(globalData, environmentName);
                RuntimeCodeViewModel preScript = null;
                RuntimeCodeViewModel postScript = null;
                RuntimeCodeViewModel validationsScript = null;
                if (string.IsNullOrWhiteSpace(this.PreScript) == false)
                {
                    preScript = new RuntimeCodeViewModel();
                    preScript.LoadCode(Constants.PreAutomationScriptStartCode, Constants.AutomationScriptIndentation, Constants.AutomationScriptEndCode);
                    preScript.EditableCode = this.PreScript;
                }

                if (string.IsNullOrWhiteSpace(this.PostScript) == false)
                {
                    postScript = new RuntimeCodeViewModel();
                    postScript.LoadCode(Constants.PostAutomationScriptStartCode, Constants.AutomationScriptIndentation, Constants.AutomationScriptEndCode);
                    postScript.EditableCode = this.PostScript;
                }

                if (string.IsNullOrWhiteSpace(this.ValidationsScript) == false)
                {
                    validationsScript = new RuntimeCodeViewModel();
                    validationsScript.LoadCode(Constants.ValidationScriptStartCode, Constants.AutomationScriptIndentation, Constants.AutomationScriptEndCode);
                    validationsScript.EditableCode = this.ValidationsScript;
                }

                string validationError = this.Validation(this, variables, certs, preScript, postScript, validationsScript);
                if (string.IsNullOrEmpty(validationError))
                {
                    this.Result = ResultState.InProgress;
                    ExecuteScriptViewModel runtimeAutomation = new ExecuteScriptViewModel();

                    if (preScript != null)
                    {
                        RestClientPreExecutionAutomation preAutomation = new RestClientPreExecutionAutomation(globalData, environmentName);
                        runtimeAutomation.ExecutePreCode(preScript.Code, preAutomation);
                        globalData = preAutomation.GlobalVariableData;
                        AppDataHelper.SaveGlobalVariablesData(globalData.ToModel());
                    }

                    this.ApplyVariables(variables);

                    X509Certificate2 clientCert = null;
                    ServiceClient client = null;

                    if (string.IsNullOrWhiteSpace(this.Certificate) == false)
                    {
                        var cert = certs?.LastOrDefault(x => x.Name.Equals(this.Certificate.Trim(), StringComparison.CurrentCultureIgnoreCase));
                        clientCert = this.GetCertificateFromThumprint(cert?.Thumbprint);
                    }

                    if (clientCert != null)
                    {
                        client = new ServiceClient(this.RequestContentType, clientCert);
                    }
                    else
                    {
                        client = new ServiceClient(this.RequestContentType);
                    }

                    RestResponse restresponse = null;
                    if (this.RequestContent != null)
                    {
                        switch (this.Operation)
                        {
                            case Constants.GET:
                                restresponse = client.GET(this.Url, this.GetHeadersPairs());
                                break;
                            case Constants.POST:
                                restresponse = client.POST(this.Url, this.GetHeadersPairs(), this.RequestContent);
                                break;
                            case Constants.PUT:
                                restresponse = client.PUT(this.Url, this.GetHeadersPairs(), this.RequestContent);
                                break;
                            case Constants.DELETE:
                                restresponse = client.DELETE(this.Url, this.GetHeadersPairs(), this.RequestContent);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (this.Operation)
                        {
                            case Constants.GET:
                                restresponse = client.GET(this.Url, this.GetHeadersPairs());
                                break;
                            case Constants.POST:
                                restresponse = client.POST(this.Url, this.GetHeadersPairs(), this.RequestParameters);
                                break;
                            case Constants.PUT:
                                restresponse = client.PUT(this.Url, this.GetHeadersPairs(), this.RequestParameters);
                                break;
                            case Constants.DELETE:
                                restresponse = client.DELETE(this.Url, this.GetHeadersPairs(), this.RequestParameters);
                                break;
                            default:
                                break;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(this.PostScript) == false)
                    {
                        RestClientPostExecutionAutomation postAutomation = new RestClientPostExecutionAutomation(globalData, environmentName, restresponse);
                        runtimeAutomation.ExecutePostCode(postScript.Code, postAutomation);
                        globalData = postAutomation.GlobalVariableData;
                        AppDataHelper.SaveGlobalVariablesData(globalData.ToModel());
                    }


                    if (string.IsNullOrWhiteSpace(this.ValidationsScript) == false)
                    {
                        RestClientValidationsAutomation validations = new RestClientValidationsAutomation(globalData, environmentName, restresponse);
                        runtimeAutomation.ExecuteValidations(validationsScript.Code, validations);
                        globalData = validations.GlobalVariableData;
                        AppDataHelper.SaveGlobalVariablesData(globalData.ToModel());

                        this.Validations = validations.Results;
                    }

                    if (restresponse != null)
                    {
                        this.ResponseHeaders = restresponse.GetResponseHeaders();
                        this.ResponseContentType = restresponse.ContentType;
                        this.StatusCode = restresponse.StatusCode;
                        this.StatusDescription = restresponse.StatusDescription;
                        this.ResponseContent = restresponse.OutputContent;
                        this.ResponseTime = restresponse.Interval;
                        this.ResponseSize = restresponse.ResponseSize;
                        this.IsCallSessessFul = restresponse.IsSuccess;
                        this.IsValidationSuccessFul = (this.Validations == null || this.Validations.Count == 0) ? (bool?)null : (this.Validations.Any(x => x.IsSuccess == false) ? false : true);
                        //SessionHistory = new ObservableCollection<SessionHistoryViewModel>(AppDataHelper.SaveSessionInHistory(transaction, SessionHistory));
                    }

                    this.Result = this.IsValidationSuccessFul.HasValue ? (this.IsValidationSuccessFul.Value ? ResultState.Pass : ResultState.Fail) : ResultState.NA;
                }
                else
                {
                    this.Result = ResultState.Fail;
                    this.IsValidationSuccessFul = false;
                    this.Validations = new List<ValidationResultModel>() { new ValidationResultModel() { IsSuccess = false, TestName = validationError } };
                }
            });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The ExtractVariablesUsed
        /// </summary>
        /// <param name = "usedVariables">The <see cref = "List{string}"/></param>
        /// <param name = "text">The <see cref = "string "/></param>
        /// <returns>The <see cref = "List{string}"/></returns>
        private List<string> ExtractVariablesUsed(List<string> usedVariables, string text)
        {
            if (string.IsNullOrEmpty(text) == false)
            {
                string regexPattern = @"\{{([^}]*)\}}";
                var result = Regex.Matches(text, regexPattern);
                foreach (Match item in result)
                {
                    usedVariables.Add(item.Groups[1].Value);
                }
            }

            return usedVariables;
        }

        /// <summary>
        /// The GetCertificateFromThumprint
        /// </summary>
        /// <param name="thumbprint">The <see cref="string"/></param>
        /// <returns>The <see cref="X509Certificate2"/></returns>
        private X509Certificate2 GetCertificateFromThumprint(string thumbprint)
        {
            if (string.IsNullOrEmpty(thumbprint))
            {
                return null;
            }

            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            List<X509Certificate2> certs = new List<X509Certificate2>();
            if (store.Certificates != null)
            {
                foreach (X509Certificate2 mCert in store.Certificates)
                {
                    if (mCert.Thumbprint.Equals(thumbprint, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return mCert;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// The GetCertificates
        /// </summary>
        /// <param name="globalData">The <see cref="GlobalVariableModel"/></param>
        /// <param name="environmentName">The <see cref="string"/></param>
        /// <returns>The <see cref="List{CertificateModel}"/></returns>
        private List<CertificateViewModel> GetCertificates(GlobalSetupViewModel globalData, string environmentName)
        {
            List<CertificateViewModel> list = new List<CertificateViewModel>();
            //List<CertificateModel> envList = globalData?.Certificates?.FirstOrDefault(x => x.Name.Equals(environmentName, StringComparison.CurrentCultureIgnoreCase))?.Certificates?.ToList();
            //if (envList != null)
            //{
            //    list.AddRange(envList);
            //}

            if (globalData != null && globalData.Variables != null)
            {
                list.AddRange(globalData.Certificates.Where(x => list.Any(y => y.Name.Equals(x.Name, StringComparison.CurrentCultureIgnoreCase)) == false));
            }

            return list;
        }

        /// <summary>
        /// The GetHeadersPairs
        /// </summary>
        /// <returns>The <see cref="List{KeyValuePair{string, string}}"/></returns>
        private List<KeyValuePair<string, string>> GetHeadersPairs()
        {
            List<KeyValuePair<string, string>> output = new List<KeyValuePair<string, string>>();
            string headerText = this.Headers.GetString().Trim();
            if (headerText != null)
            {
                string[] multiheaders = headerText.Replace(Environment.NewLine, "\n").Split('\n');
                foreach (string header in multiheaders)
                {
                    if (header.GetString().Trim() != string.Empty)
                    {
                        int pos = header.IndexOf(':');
                        if (pos > -1)
                        {
                            KeyValuePair<string, string> obj = new KeyValuePair<string, string>(header.Substring(0, pos).GetString().Trim(), header.Substring(pos + 1, header.Length - pos - 1).GetString().Trim());
                            output.Add(obj);
                        }
                        else
                        {
                            KeyValuePair<string, string> obj = new KeyValuePair<string, string>(header.GetString(), string.Empty);
                            output.Add(obj);
                        }
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// The GetVariables
        /// </summary>
        /// <param name="globalData">The <see cref="GlobalVariableModel"/></param>
        /// <param name="environmentName">The <see cref="string"/></param>
        /// <returns>The <see cref="List{KeyValueModel}"/></returns>
        private List<KeyValueViewModel> GetVariables(GlobalSetupViewModel globalData, string environmentName)
        {
            List<KeyValueViewModel> list = new List<KeyValueViewModel>();
            List<KeyValueViewModel> envList = globalData?.Environments?.FirstOrDefault(x => x.Name.Equals(environmentName, StringComparison.CurrentCultureIgnoreCase))?.Variables?.ToList();
            if (envList != null)
            {
                list.AddRange(envList);
            }

            if (globalData != null && globalData.Variables != null)
            {
                list.AddRange(globalData.Variables.Where(x => list.Any(y => y.Key.Equals(x.Key, StringComparison.CurrentCultureIgnoreCase)) == false));
            }

            return list;
        }

        /// <summary>
        /// The ReplaceVariables
        /// </summary>
        /// <param name = "input">The <see cref = "string "/></param>
        /// <param name = "variables">The <see cref = "List{KeyValueModel}"/></param>
        /// <returns>The <see cref = "string "/></returns>
        private string ReplaceVariables(string input, List<KeyValueViewModel> variables)
        {
            if (variables != null)
            {
                foreach (var variable in variables)
                {
                    input = input.GetString().Replace(string.Format("{{{{{0}}}}}", variable.Key), variable.Value);
                }
            }

            return input;
        }

        /// <summary>
        /// The Validation
        /// </summary>
        /// <param name="request">The <see cref="TestRequestViewModel"/></param>
        /// <param name="variable">The <see cref="List{KeyValueModel}"/></param>
        /// <param name="certificates">The <see cref="List{CertificateModel}"/></param>
        /// <param name="preScript">The <see cref="RuntimeCodeViewModel"/></param>
        /// <param name="postScript">The <see cref="RuntimeCodeViewModel"/></param>
        /// <param name="validationsScript">The <see cref="RuntimeCodeViewModel"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string Validation(TestRequestViewModel request, List<KeyValueViewModel> variable, List<CertificateViewModel> certificates, RuntimeCodeViewModel preScript, RuntimeCodeViewModel postScript, RuntimeCodeViewModel validationsScript)
        {
            try
            {
                List<string> usedVariables = new List<string>();
                usedVariables = ExtractVariablesUsed(usedVariables, request.PreUrl);
                usedVariables = ExtractVariablesUsed(usedVariables, request.PreRequestContent);
                usedVariables = ExtractVariablesUsed(usedVariables, request.PreRequestHeaders);
                usedVariables = usedVariables.Distinct().ToList();
                var unavailableVariables = usedVariables.Where(x => variable != null && variable.Any(y => y.Key == x) == false);
                if (unavailableVariables.Any())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Unable to find following variables in the Global or selected environment variables.");
                    sb.AppendLine();
                    foreach (var item in unavailableVariables)
                    {
                        sb.AppendLine(item);
                    }

                    return sb.ToString();
                }

                Uri temp = new Uri(this.ReplaceVariables(request.PreUrl, variable), UriKind.Absolute);

                bool successPreScript = preScript != null ? preScript.CompileCode() : true;
                if (successPreScript == false)
                {
                    return "There are compilation error in pre-script instructions";
                }

                bool successPostScript = postScript != null ? postScript.CompileCode() : true;
                if (successPostScript == false)
                {
                    return "There are compilation error in post-script instructions";
                }

                bool successValidationsScript = validationsScript != null ? validationsScript.CompileCode() : true;
                if (successValidationsScript == false)
                {
                    return "There are compilation error in validation script instructions";
                }

                if (string.IsNullOrWhiteSpace(this.Certificate) == false)
                {
                    var cert = certificates?.LastOrDefault(x => x.Name.Equals(this.Certificate.Trim(), StringComparison.CurrentCultureIgnoreCase));
                    if (cert == null)
                    {
                        return "Unable to find '" + this.Certificate + "' certificate in the Global or selected environment certificates.";
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return "Error - " + ex.Message;
            }
        }

        #endregion

        #endregion
    }
}
