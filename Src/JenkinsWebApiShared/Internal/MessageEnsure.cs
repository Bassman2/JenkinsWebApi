using System.Net;
using System.Net.Http;

namespace JenkinsWebApi.Internal
{
    internal static class MessageEnsure
    {
        internal static void EnsureSuccess(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.Forbidden )
            {
                throw new JenkinsException(response);
            }
        }
    }
}
