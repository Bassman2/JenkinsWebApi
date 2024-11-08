using System.Net.Http;

namespace JenkinsWebApi
{
    /// <summary>
    /// Jenkins exception for HTTP not found error
    /// </summary>
    public class JenkinsNotFoundException : JenkinsException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="response">HTTP response message.</param>
        public JenkinsNotFoundException(HttpResponseMessage response) : base(response)
        { }
    }
}
