using System.Net.Http;

namespace JenkinsWebApi
{
    /// <summary>
    /// Jenkins exception for HTTP unauthorized error
    /// </summary>
    public class JenkinsUnauthorizedException : JenkinsException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="response">HTTP response message.</param>
        public JenkinsUnauthorizedException(HttpResponseMessage response) : base(response)
        { }
    }
}
