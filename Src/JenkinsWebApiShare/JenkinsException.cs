using System;
using System.Net;
using System.Net.Http;

namespace JenkinsWebApi
{
    /// <summary>
    /// Base class for Jenkins exception
    /// </summary>
    public class JenkinsException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="response">HTTP response message.</param>
        public JenkinsException(HttpResponseMessage response) : base(response.ToString())
        {
            this.StatusCode = response.StatusCode;
            this.Reason = response.ReasonPhrase;
        }

        /// <summary>
        /// HTTP status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Reason for HTTP status code.
        /// </summary>
        public string Reason { get; }
    }
}
