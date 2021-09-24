using System.Net.Http;

namespace JenkinsWebApi
{
    /// <summary>
    /// Jenkins exception for HTTP forbidden error
    /// </summary>
    public class JenkinsForbiddenException : JenkinsException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="response">HTTP response message.</param>
        public JenkinsForbiddenException(HttpResponseMessage response) : base(response)
        { }
    }
}
