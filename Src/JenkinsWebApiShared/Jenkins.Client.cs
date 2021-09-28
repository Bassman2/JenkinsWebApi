using JenkinsWebApi.Internal;
using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE0063 // Use simple 'using' statement
#pragma warning restore IDE0079 // Remove unnecessary suppression

namespace JenkinsWebApi
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Jenkins : IDisposable
    {
        private const string apiFormat = JenkinsDeserializer.ApiFormat;


        private HttpClientHandler handler;
        private HttpClient client;

        /// <summary>
        /// 
        /// </summary>
        private Uri BaseAddress { get { return this.client.BaseAddress; } }

        private void Connect(Uri host, string login, string passwordOrToken)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            // connect
            this.handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new System.Net.CookieContainer()
            };
            this.client = new HttpClient(this.handler)
            {
                BaseAddress = host
            };

            // set authorization
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(passwordOrToken))
            {
                Authorize(login, passwordOrToken);
                //this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{login}:{passwordOrToken}")));

                //Crumb();
            }
            else
            {
                // needed for anonymus but ignore Forbidden for later login
                Crumb(true);
            }
        }

        private void Authorize(string login, string passwordOrToken)
        {
            // set authorization
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{login}:{passwordOrToken}")));

            // set crumb
            Crumb();
        }

        private void Crumb(bool ignoreForbidden = false)
        {
            var ignoreList = new List<HttpStatusCode>();
            ignoreList.Add(HttpStatusCode.NotFound); // 404 not found : for old Jenkins versionen without crumb
            if (ignoreForbidden)
            {
                ignoreList.Add(HttpStatusCode.Forbidden);
            }
            JenkinsSecurityCsrfDefaultCrumbIssuer crumb = GetApiAsync<JenkinsSecurityCsrfDefaultCrumbIssuer>("crumbIssuer", ignoreList, CancellationToken.None).Result;
            if (crumb != null)
            {
                this.client.DefaultRequestHeaders.Add(crumb.CrumbRequestField, crumb.Crumb);
            }
        }

        /// <summary>
        /// Release allocated resources.
        /// </summary>
        public void Dispose()
        {
            if (this.client != null)
            {
                this.client.Dispose();
                this.client = null;
            }
            if (this.handler != null)
            {
                this.handler.Dispose();
                this.handler = null;
            }
        }

        private async Task<T> GetApiAsync<T>(string path, CancellationToken cancellationToken) where T : class
        {
            using (HttpResponseMessage response = await this.client.GetAsync(path + apiFormat, cancellationToken))
            {
                response.EnsureSuccess();
                T value = await response.Content.ReadAsAsync<T>(cancellationToken);
                return value;
            }
        }

        private async Task<T> GetApiViewAsync<T>(string path, CancellationToken cancellationToken) where T : JenkinsModelView
        {
            using (HttpResponseMessage response = await this.client.GetAsync(path + apiFormat, cancellationToken))
            {
                response.EnsureSuccess();
                T value = await response.Content.ReadAsViewAsync<T>(cancellationToken);
                return value;
            }
        }

        private async Task<T> GetApiJobAsync<T>(string path, CancellationToken cancellationToken) where T : JenkinsModelAbstractItem
        {
            using (HttpResponseMessage response = await this.client.GetAsync(path + apiFormat, cancellationToken))
            {
                response.EnsureSuccess();
                T value = await response.Content.ReadAsJobAsync<T>(cancellationToken);
                return value;
            }
        }

        private async Task<T> GetApiBuildAsync<T>(string path, CancellationToken cancellationToken) where T : JenkinsModelRun
        {
            using (HttpResponseMessage response = await this.client.GetAsync(path + apiFormat, cancellationToken))
            {
                response.EnsureSuccess();
                T value = await response.Content.ReadAsBuildAsync<T>(cancellationToken);
                return value;
            }
        }

        private async Task<T> GetApiAsync<T>(string path, HttpStatusCode ignoreStatusCode, CancellationToken cancellationToken) where T : class
        {
            using (HttpResponseMessage response = await this.client.GetAsync(path + apiFormat, cancellationToken))
            {
                response.EnsureSuccess(ignoreStatusCode);
                T value = await response.Content.ReadAsAsync<T>(cancellationToken);
                return value;
            }
        }

        
        private async Task<T> GetApiAsync<T>(string path, IEnumerable<HttpStatusCode> ignoreStatusCodes, CancellationToken cancellationToken) where T : class
        {
            using (HttpResponseMessage response = await this.client.GetAsync(path + apiFormat, cancellationToken))
            {
                
                response.EnsureSuccess(ignoreStatusCodes);
                T value = response.StatusCode == HttpStatusCode.OK ? await response.Content.ReadAsAsync<T>(cancellationToken) : null;
                return value;
            }
        }

        private async Task<string> GetApiStringAsync(string path, CancellationToken cancellationToken)
        {
            using (HttpResponseMessage response = await this.client.GetAsync(path + apiFormat, cancellationToken))
            {
                response.EnsureSuccess();
                string str = await response.Content.ReadAsStringAsync(
#if NET
                    cancellationToken
#endif
                    );
                return str;
            }
        }

        private async Task<string> GetStringAsync(string path, CancellationToken cancellationToken)
        {
            using (HttpResponseMessage response = await this.client.GetAsync(path, cancellationToken))
            {
                response.EnsureSuccess();
                string str = await response.Content.ReadAsStringAsync(
#if NET
                    cancellationToken
#endif
                    );
                return str;
            }
        }

        private async Task PostAsync(string path, CancellationToken cancellationToken)
        {
            using (HttpResponseMessage response = await this.client.PostAsync(path, null, cancellationToken))
            {
                string str = await response.Content.ReadAsStringAsync(
#if NET
                    cancellationToken
#endif
                    );
                response.EnsureSuccess();
            }
        }

        private async Task PostAsync(string path, HttpStatusCode ignoreStatusCode, CancellationToken cancellationToken)
        {
            using (HttpResponseMessage response = await this.client.PostAsync(path, null, cancellationToken))
            {
                string str = await response.Content.ReadAsStringAsync(
#if NET
                    cancellationToken
#endif
                    );
                response.EnsureSuccess(ignoreStatusCode);
            }
        }

        // StringContent for application/xml, ...
        // MultipartFormDataContent for multipart/form-data
        // FormUrlEncodedContent for application/x-www-form-urlencoded
        private async Task PostAsync(string path, HttpContent content, CancellationToken cancellationToken)
        {
            using (HttpResponseMessage response = await this.client.PostAsync(path, content, cancellationToken))
            {
                response.EnsureSuccess();
            }
        }

        private async Task PostAsync(string path, IEnumerable<KeyValuePair<string, string>> content, CancellationToken cancellationToken)
        {
            using (HttpResponseMessage response = await this.client.PostAsync(path, new FormUrlEncodedContent(content), cancellationToken))
            {
                response.EnsureSuccess();
            }
        }

        private async Task<string> PostResAsync(string path, IEnumerable<KeyValuePair<string, string>> content, CancellationToken cancellationToken)
        {
            using (HttpResponseMessage response = await this.client.PostAsync(path, new FormUrlEncodedContent(content), cancellationToken))
            {
                response.EnsureSuccess();
                string str = await response.Content.ReadAsStringAsync(
#if NET
                    cancellationToken
#endif
                    );
                return str;
            }
        }
        
        private async Task<PostRunRes> PostRunJobAsync(string path, HttpContent content, CancellationToken cancellationToken)
        {
            using (HttpResponseMessage response = await this.client.PostAsync(path, content, cancellationToken))
            {
                if (response.StatusCode != HttpStatusCode.Conflict)
                {
                    response.EnsureSuccess();
                }

                return new PostRunRes() { Location = response.Headers.Location, StatusCode = response.StatusCode };
            }
        }

        private async Task DeleteAsync(string path, CancellationToken cancellationToken)
        {
            using (HttpResponseMessage response = await this.client.DeleteAsync(path, cancellationToken))
            {
                response.EnsureSuccess();
            }
        }

        private static string TrimScript(string str)
        {
            Match match = Regex.Match(str, @"(<pre>(?<script>[^<]*)<\/pre>)*");
            return match.Success ? match.Groups["script"].Value : null;
        }

    }
}
