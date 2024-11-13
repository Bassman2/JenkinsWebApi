using System;
using System.Net;

namespace JenkinsWebApi.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class PostRunRes
    {
        /// <summary>
        /// 
        /// </summary>
        public Uri Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }
}
