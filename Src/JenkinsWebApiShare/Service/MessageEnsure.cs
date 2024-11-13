using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace JenkinsWebApi.Internal
{
    internal static class MessageEnsure
    {
        internal static void EnsureSuccess(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            switch (response.StatusCode)
            {
            case HttpStatusCode.Forbidden:
                throw new JenkinsForbiddenException(response);
            case HttpStatusCode.Unauthorized:
                throw new JenkinsUnauthorizedException(response);
            case HttpStatusCode.NotFound:
                throw new JenkinsNotFoundException(response);
            default:
                throw new JenkinsException(response);
            }
        }

        internal static void EnsureSuccess(this HttpResponseMessage response, HttpStatusCode ignoreStatusCode)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            if (response.StatusCode == ignoreStatusCode)
            {
                return;
            }
            switch (response.StatusCode)
            {
            case HttpStatusCode.Forbidden:
                throw new JenkinsForbiddenException(response);
            case HttpStatusCode.Unauthorized:
                throw new JenkinsUnauthorizedException(response);
            case HttpStatusCode.NotFound:
                throw new JenkinsNotFoundException(response);
            default:
                throw new JenkinsException(response);
            }
        }

        internal static void EnsureSuccess(this HttpResponseMessage response, IEnumerable<HttpStatusCode> ignoreStatusCodes)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            if (ignoreStatusCodes.Contains(response.StatusCode))
            {
                return;
            }
            switch (response.StatusCode)
            {
                case HttpStatusCode.Forbidden:
                    throw new JenkinsForbiddenException(response);
                case HttpStatusCode.Unauthorized:
                    throw new JenkinsUnauthorizedException(response);
                case HttpStatusCode.NotFound:
                    throw new JenkinsNotFoundException(response);
                default:
                    throw new JenkinsException(response);
            }
        }
    }
}
