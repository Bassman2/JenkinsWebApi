using System.Net.Http;

namespace JenkinsWebApi
{
    public class JenkinsNotFoundException : JenkinsException
    {

        public JenkinsNotFoundException(HttpResponseMessage response) : base(response)
        { }
    }
}
