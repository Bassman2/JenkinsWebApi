using System.Net.Http;

namespace JenkinsWebApi
{
    public class JenkinsUnauthorizedException : JenkinsException
    {

        public JenkinsUnauthorizedException(HttpResponseMessage response) : base(response)
        { }
    }
}
