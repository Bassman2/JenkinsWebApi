using System;
using System.Net;
using System.Net.Http;

namespace JenkinsWebApi
{
    public class JenkinsException : Exception
    {
        public JenkinsException(HttpResponseMessage response) : base(response.ToString())
        {
            this.StatusCode = response.StatusCode;
            this.Reason = response.ReasonPhrase;
        }

        public HttpStatusCode StatusCode { get; }
        public string Reason { get; }
    }
}
