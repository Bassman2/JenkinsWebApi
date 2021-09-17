using System;
using System.Net;

namespace JenkinsWebApi.Internal
{
    public class PostRunRes
    {
        public Uri Location { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
