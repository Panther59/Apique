// <copyright file="ServiceClient.cs" company="Accenture">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>DIR\utkarsh.chauhan</author>
// <date>27-08-2017</date>

namespace RestClientLibrary
{
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="ServiceClient" />
	/// </summary>
	public class ServiceClient
    {
        #region Fields

        /// <summary>
        /// Defines the _contentType
        /// </summary>
        private string _contentType = "application/json";

        /// <summary>
        /// Defines the _timeout
        /// </summary>
        private int _timeout = 10000;

        /// <summary>
        /// Defines the cert
        /// </summary>
        private System.Security.Cryptography.X509Certificates.X509Certificate2 cert;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceClient"/> class.
        /// </summary>
        public ServiceClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceClient"/> class.
        /// </summary>
        /// <param name="contentType">The <see cref="string"/></param>
        public ServiceClient(string contentType)
            : this()
        {
            if (contentType != null)
            {
                _contentType = contentType;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceClient"/> class.
        /// </summary>
        /// <param name="contentType">The <see cref="string"/></param>
        /// <param name="timeout">The <see cref="int"/></param>
        public ServiceClient(string contentType, int timeout)
            : this(contentType)
        {
            _timeout = timeout;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceClient"/> class.
        /// </summary>
        /// <param name="cert">The <see cref="System.Security.Cryptography.X509Certificates.X509Certificate2"/></param>
        public ServiceClient(string contentType, System.Security.Cryptography.X509Certificates.X509Certificate2 cert) : this(contentType)
        {
            this.cert = cert;
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// The ValidateCertificate
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="certificate">The <see cref="System.Security.Cryptography.X509Certificates.X509Certificate"/></param>
        /// <param name="chain">The <see cref="System.Security.Cryptography.X509Certificates.X509Chain"/></param>
        /// <param name="sslPolicyErrors">The <see cref="System.Net.Security.SslPolicyErrors"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool ValidateCertificate(
            object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// The ValidateServerCertificate
        /// </summary>
        /// <param name="sender">The <see cref="object"/></param>
        /// <param name="certificate">The <see cref="System.Security.Cryptography.X509Certificates.X509Certificate"/></param>
        /// <param name="chain">The <see cref="System.Security.Cryptography.X509Certificates.X509Chain"/></param>
        /// <param name="sslPolicyErrors">The <see cref="System.Net.Security.SslPolicyErrors"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool ValidateServerCertificate(
            object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
                return true;

            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors)
            {
                Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
                foreach (var chainstat in chain.ChainStatus)
                {
                    Console.WriteLine("{0}", chainstat.Status);
                    Console.WriteLine("{0}", chainstat.StatusInformation);
                }
                return true;
            }

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // allow this client to communicate with unauthenticated servers. 
            return true;
        }

        /// <summary>
        /// The DELETE
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="headers">The <see cref="List{KeyValuePair{string, string}}"/></param>
        /// <param name="requestParameters">The <see cref="List{KeyValuePair{string, string}}"/></param>
        /// <returns>The <see cref="RestResponse"/></returns>
        public RestResponse DELETE(string url, List<KeyValuePair<string, string>> headers, List<KeyValuePair<string, string>> requestParameters)
        {
            // Create a request and set the headers
            RestSharp.RestRequest request = new RestRequest(url, Method.Delete);
            return SendContent(url, request, headers, null, requestParameters);
        }

        /// <summary>
        /// The DELETE
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="headers">The <see cref="List{KeyValuePair{string, string}}"/></param>
        /// <param name="requestContent">The <see cref="string"/></param>
        /// <returns>The <see cref="RestResponse"/></returns>
        public RestResponse DELETE(string url, List<KeyValuePair<string, string>> headers, string requestContent)
        {
            // Create a request and set the headers
            RestSharp.RestRequest request = new RestRequest(url, Method.Delete);
            return SendContent(url, request, headers, requestContent, null);
        }

        /// <summary>
        /// The GET
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="headers">The <see cref="List{KeyValuePair{string, string}}"/></param>
        /// <returns>The <see cref="RestResponse"/></returns>
        public RestResponse GET(string url, List<KeyValuePair<string, string>> headers)
        {
            // Create a request and set the headers
            RestSharp.RestRequest request = new RestRequest(url, Method.Get);
            return SendContent(url, request, headers, null, null);
        }

        /// <summary>
        /// The POST
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="headers">The <see cref="List{KeyValuePair{string, string}}"/></param>
        /// <param name="requestParameters">The <see cref="List{KeyValuePair{string, string}}"/></param>
        /// <returns>The <see cref="RestResponse"/></returns>
        public RestResponse POST(string url, List<KeyValuePair<string, string>> headers, List<KeyValuePair<string, string>> requestParameters)
        {
            // Create a request and set the headers
            RestSharp.RestRequest request = new RestRequest(url, Method.Post);
            return SendContent(url, request, headers, null, requestParameters);
        }

        /// <summary>
        /// The POST
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="headers">The <see cref="List{KeyValuePair{string, string}}"/></param>
        /// <param name="requestContent">The <see cref="string"/></param>
        /// <returns>The <see cref="RestResponse"/></returns>
        public RestResponse POST(string url, List<KeyValuePair<string, string>> headers, string requestContent)
        {
            // Create a request and set the headers
            RestSharp.RestRequest request = new RestRequest(url, Method.Post);
            return SendContent(url, request, headers, requestContent, null);
        }

        /// <summary>
        /// The PUT
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="headers">The <see cref="List{KeyValuePair{string, string}}"/></param>
        /// <param name="requestParameters">The <see cref="List{KeyValuePair{string, string}}"/></param>
        /// <returns>The <see cref="RestResponse"/></returns>
        public RestResponse PUT(string url, List<KeyValuePair<string, string>> headers, List<KeyValuePair<string, string>> requestParameters)
        {
            // Create a request and set the headers
            RestSharp.RestRequest request = new RestRequest(url, Method.Put);
            return SendContent(url, request, headers, null, requestParameters);
        }

        /// <summary>
        /// The PUT
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="headers">The <see cref="List{KeyValuePair{string, string}}"/></param>
        /// <param name="requestContent">The <see cref="string"/></param>
        /// <returns>The <see cref="RestResponse"/></returns>
        public RestResponse PUT(string url, List<KeyValuePair<string, string>> headers, string requestContent)
        {
            // Create a request and set the headers
            RestSharp.RestRequest request = new RestRequest(url, Method.Put);
            return SendContent(url, request, headers, requestContent, null);
        }

		#endregion

		#region Private Methods

		/// <summary>
		/// The AttachCertificate
		/// </summary>
		/// <param name="client">The <see cref="RestClient"/></param>
		private void AttachCertificate(RestClientOptions options)
		{
			if (cert != null)
			{
                options.ClientCertificates = new System.Security.Cryptography.X509Certificates.X509CertificateCollection();
                options.ClientCertificates.Add(cert);

                options.PreAuthenticate = true;
				ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(ValidateCertificate);
			}
		}

		/// <summary>
		/// The BuildResponse
		/// </summary>
		/// <param name="response">The <see cref="IRestResponse"/></param>
		/// <returns>The <see cref="RestResponse"/></returns>
		private RestResponse BuildResponse(RestSharp.RestResponse response)
        {
            RestResponse output = new RestResponse();
            if (response != null)
            {

                // get the response content
                output.OutputContent = response.Content;

                output.StatusCode = (int)response.StatusCode;
                output.IsSuccess = output.StatusCode >= 200 && output.StatusCode < 300;
                output.StatusDescription = response.StatusDescription ?? "No Response";
                if (response.Headers != null)
                {
                    output.OutputHeader = response.Headers.Select(x => new KeyValuePair<string, string>(x.Name, x.Value.ToString())).ToList();
                }

                output.ResponseSize = response.RawBytes != null ? (long?)response.RawBytes.LongLength : null;
                output.ContentType = response.ContentType;
            }
            else
            {
                output.StatusCode = 0;
                output.StatusDescription = "No Response";
                output.IsSuccess = false;
            }
            return output;
        }

        /// <summary>
        /// The SendContent
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="request">The <see cref="IRestRequest"/></param>
        /// <param name="headers">The <see cref="List{KeyValuePair{string, string}}"/></param>
        /// <param name="requestBody">The <see cref="string"/></param>
        /// <returns>The <see cref="RestResponse"/></returns>
        private RestResponse SendContent(string url, RestSharp.RestRequest request, List<KeyValuePair<string, string>> headers, string requestBody)
        {
            return this.SendContent(url, request, headers, requestBody, null);
        }

        /// <summary>
        /// The SendContent
        /// </summary>
        /// <param name="url">The <see cref="string"/></param>
        /// <param name="request">The <see cref="IRestRequest"/></param>
        /// <param name="headers">The <see cref="List{KeyValuePair{string, string}}"/></param>
        /// <param name="requestBody">The <see cref="string"/></param>
        /// <param name="requestParameters">The <see cref="List{KeyValuePair{string, string}}"/></param>
        /// <returns>The <see cref="RestResponse"/></returns>
        private RestResponse SendContent(string url, RestSharp.RestRequest request, List<KeyValuePair<string, string>> headers, string requestBody, List<KeyValuePair<string, string>> requestParameters)
        {
            var options = new RestClientOptions();
            this.AttachCertificate(options);
            var client = new RestClient(options);
            RestSharp.RestResponse response = null;
            DateTime requestTime = DateTime.Now;
            DateTime responseTime = DateTime.Now;
            RestResponse resObj = null;
            try
            {
                //request.UserAgent = userAgent
                //request.Timeout = _timeout;

                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> header in headers)
                    {
                        request.AddHeader(header.Key, header.Value);
                    }
                }
                request.AddHeader(HttpRequestHeader.ContentType.ToString(), _contentType);

                if (_contentType.Contains("xml"))
                {
                    request.RequestFormat = DataFormat.Xml;
                }
                else
                {
                    request.RequestFormat = DataFormat.Json;
                }
                //if (useBasicAuth == true)
                //{
                //    // Create a token for basic authentication and add a header for it
                //    String authorization = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
                //    request.Headers.Add("Authorization", "Basic " + authorization)
                //}

                if (requestBody != null)
                {
                    request.AddParameter(_contentType, requestBody, ParameterType.RequestBody);
                }
                else if (requestParameters != null)
                {
                    requestParameters.ForEach(x => request.AddParameter(x.Key, x.Value));
                }

                // Initialize the response
                // Now try to send the request


                response = client.ExecuteAsync(request).GetAwaiter().GetResult();
                responseTime = DateTime.Now;
            }
            catch (WebException e)
            {
                responseTime = DateTime.Now;
                //if (e.Response != null)
                //{
                //    response = (HttpWebResponse)e.Response;
                //}
            }
            finally
            {
                resObj = BuildResponse(response);
                resObj.Interval = Convert.ToInt32((responseTime - requestTime).TotalMilliseconds);
            }
            return resObj;
        }

        #endregion

        #endregion
    }
}
