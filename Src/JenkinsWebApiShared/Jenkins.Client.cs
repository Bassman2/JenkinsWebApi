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
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{login}:{passwordOrToken}")));
            }

            // set crumb
            Crumb();
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

        private void Authorize(string login, string passwordOrToken)
        {
            // set authorization
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{login}:{passwordOrToken}")));

            // set crumb
            Crumb();
        }

        private void Crumb()
        {
            // only on newer Jenkins versions
            // handle CSRF Protection
            try
            {
                JenkinsSecurityCsrfDefaultCrumbIssuer crumb = GetApiAsync<JenkinsSecurityCsrfDefaultCrumbIssuer>("crumbIssuer", CancellationToken.None).Result;
                this.client.DefaultRequestHeaders.Add(crumb.CrumbRequestField, crumb.Crumb);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task<T> GetApiAsync<T>(string path, CancellationToken cancellationToken) where T : class
        {
            using (HttpResponseMessage response = await this.client.GetAsync(path + apiFormat, cancellationToken))
            {
                response.EnsureSuccess();
                string str = await response.Content.ReadAsStringAsync(
#if NET
                    cancellationToken
#endif
                    );
                T value = JenkinsDeserializer.Deserialize<T>(str);
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

        //private async Task PostAsync(string path, HttpContent content, CancellationToken cancellationToken)
        //{
        //    using (HttpResponseMessage response = await this.client.PostAsync(path, content, cancellationToken))
        //    {
        //        response.EnsureSuccess();
        //    }
        //}

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

        private async Task PostCreateJobAsync(string path, string config, CancellationToken cancellationToken)
        {
            //HttpRequestHeaders defHeader = this.client.DefaultRequestHeaders;

            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
            ////request.Content = header;
            //request.Headers.Authorization = defHeader.Authorization;

            //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            var content = new StringContent(config, Encoding.UTF8, "application/xml");
            using (HttpResponseMessage response = await this.client.PostAsync(path, content, cancellationToken))
            {
                response.EnsureSuccess();
            }
        }

        //private async Task PostCreateJobAsync2(string path, string config, CancellationToken cancellationToken)
        //{
        //    await Task.Run(() =>
        //    {
        //        var authorization = this.client.DefaultRequestHeaders.Authorization;
        //        string auth = authorization.ToString(); // TODO check if equal

        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.client.BaseAddress + path);
        //        //string mergedCredentials = string.Format("{0}:{1}", "username", "password");
        //        //byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
        //        //string base64Credentials = Convert.ToBase64String(byteCredentials);

        //        request.Headers.Add("Authorization", authorization.Scheme + authorization.Parameter);
        //        request.Method = "POST";
        //        request.ContentType = "application/xml";
        //        request.CookieContainer = this.handler.CookieContainer;

        //        byte[] byteArray = Encoding.UTF8.GetBytes(config);
        //        request.ContentLength = byteArray.Length;

        //        Stream dataStream = request.GetRequestStream();
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        dataStream.Close();

        //        using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        //        {
        //            HttpStatusCode statusCode = response.StatusCode;
        //            if (statusCode != HttpStatusCode.OK)
        //            {
        //                throw new Exception();
        //            }
        //        }
        //    }, cancellationToken);
        //}

        /*
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        string mergedCredentials = string.Format("{0}:{1}", "username", "password");
        byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
        string base64Credentials = Convert.ToBase64String(byteCredentials);
        request.Headers.Add("Authorization", "Basic " + base64Credentials);
        request.Method = "POST";
        request.ContentType = "application/xml";

        StreamReader reader = new StreamReader(fileName);
        string ret = reader.ReadToEnd();
        reader.Close();
        string postData = ret;
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        request.ContentLength = byteArray.Length;

        Stream dataStream = request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        string result = string.Empty;
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            result = reader.ReadToEnd();
        }
        */ 
        
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
