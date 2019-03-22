using System;
using System.Net.Http;

namespace JenkinsWebApi.Internal
{
    internal static class MessageEnsure
    {
        internal static void EnsureSuccess(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
