using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using System.Net.Http.Headers;

//#pragma warning disable IDE0063 // Use simple 'using' statement

namespace JenkinsWebApi
{
    /// <summary>
    /// Main class of the Jenkins server API
    /// </summary>
    public sealed partial class Jenkins : IDisposable
    {
        //private readonly Uri host;
        private HttpClientHandler handler;
        private HttpClient client;
        private const int udpPort = 33848;

        //private readonly static Type[] viewTypes = AppDomain.CurrentDomain.GetAssemblies()
        //                                .SelectMany(s => s.GetTypes())
        //                                .Where(t => typeof(JenkinsModelView).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
        //                                .ToArray();

        //private readonly static Type[] jobTypes = AppDomain.CurrentDomain.GetAssemblies()
        //                                .SelectMany(s => s.GetTypes())
        //                                .Where(t => typeof(JenkinsModelJob).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
        //                                .ToArray();

        //private readonly static Type[] buildTypes = AppDomain.CurrentDomain.GetAssemblies()
        //                                .SelectMany(s => s.GetTypes())
        //                                .Where(t => typeof(JenkinsModelAbstractBuild).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
        //                                .ToArray();

        /// <summary>
        /// Initializes a new instance of the Jenkins class.
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server</param>
        public Jenkins(string host) : this(new Uri(host))
        { }

        /// <summary>
        /// Initializes a new instance of the Jenkins class.
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server</param>
        public Jenkins(Uri host)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            // connect
            this.handler = new HttpClientHandler
            {
                AllowAutoRedirect = true,  // for start job response /queue/item/x
                CookieContainer = new System.Net.CookieContainer(),
                UseCookies = true
            };
            this.client = new HttpClient(this.handler)
            {
                BaseAddress = host
            };

            Crumb();
        }

        /// <summary>
        /// Initializes a new instance of the Jenkins class.
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server</param>
        /// <param name="login">Login for the Jenkins server</param>
        /// <param name="password">Password for the Jenkins server</param>
        public Jenkins(string host, string login, string password) : this(new Uri(host), login, password)
        { }

        /// <summary>
        /// Initializes a new instance of the Jenkins class.
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server</param>
        /// <param name="login">Login for the Jenkins server</param>
        /// <param name="password">Password for the Jenkins server</param>
        public Jenkins(Uri host, string login, string password) 
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            //CredentialCache credentialCache = new CredentialCache();
            //credentialCache.Add(host, "Basic", new NetworkCredential(login, password));

            // connect
            this.handler = new HttpClientHandler
            {
                //PreAuthenticate = true,
                //Credentials = credentialCache, //new NetworkCredential(login, password),

                UseCookies = true,
                CookieContainer = new System.Net.CookieContainer()
            };
            this.client = new HttpClient(this.handler)
            {
                BaseAddress = host
            };
            
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{login}:{password}")));
            //this.client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials);

            Crumb();
        }

        /// <summary>
        /// Initializes a new instance of the Jenkins class.
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server</param>
        /// <param name="accessToken">Access Token for the Jenkins server</param>
        public Jenkins(string host, string accessToken) : this(new Uri(host), accessToken)
        { }

        /// <summary>
        /// Initializes a new instance of the Jenkins class.
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server</param>
        /// <param name="accessToken">Access Token for the Jenkins server</param>
        public Jenkins(Uri host, string accessToken)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            // connect
            this.handler = new HttpClientHandler
            {
                
                PreAuthenticate = true,
                CookieContainer = new System.Net.CookieContainer(),
                UseCookies = true
            };
            this.client = new HttpClient(this.handler)
            {
                BaseAddress = host,
                
            };
            this.client.DefaultRequestHeaders.Add("Authentication-Token", accessToken);
            
            Crumb();

            //this.client.

            //if (!Login(login, password))
            //{
            //    throw new Exception("Login failed!");
            //}
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

        ///////// <summary>
        ///////// Login to the Jenkins server.
        ///////// </summary>
        ///////// <param name="login">User login name</param>
        ///////// <param name="password">User login password</param>
        ///////// <returns>true if login success; false if failed</returns>
        //////public bool Login(string login, string password)
        //////{
        //////    if (string.IsNullOrEmpty(login))
        //////    {
        //////        throw new ArgumentNullException(nameof(login));
        //////    }

        //////    if (string.IsNullOrEmpty(password))
        //////    {
        //////        throw new ArgumentNullException(nameof(password));
        //////    }

        //////    var list = new Dictionary<string, string>
        //////    {
        //////        { "j_username", login },
        //////        { "j_password", password },
        //////        { "remember_me", "on" },
        //////        { "Submit", "Anmelden" }
        //////    };

        //////    var res = PostLoginAsync("j_acegi_security_check", new FormUrlEncodedContent(list), CancellationToken.None).Result;
        //////    //if (res)
        //////    //{
        //////    //    Crumb();
        //////    //}
        //////    return res;
        //////}

        public void Crumb()
        {
            // only on newer Jenkins versions
            // handle CSRF Protection
            try
            {
                JenkinsSecurityCsrfDefaultCrumbIssuer crumb = GetAsync<JenkinsSecurityCsrfDefaultCrumbIssuer>("crumbIssuer/api/json", CancellationToken.None).Result;
                this.client.DefaultRequestHeaders.Add(crumb.CrumbRequestField, crumb.Crumb);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    
        /// <summary>
        /// Get a list with all Jenkins servers in the local subnet.
        /// </summary>
        /// <returns>List with Jenkins servers</returns>
        public static async Task<IEnumerable<JenkinsInstance>> GetJenkinsInstancesAsync()
        {
            return await GetJenkinsInstancesAsync(2000);
        }
        
        /// <summary>
        /// Get a list with all Jenkins servers in the local subnet.
        /// </summary>
        /// <param name="timeout">Timeout of the search.</param>
        /// <returns>List with Jenkins servers</returns>
        public static async Task<IEnumerable<JenkinsInstance>> GetJenkinsInstancesAsync(long timeout)
        {
            List<JenkinsInstance> list = null;
            using (UdpClient client = new UdpClient())
            {
                await client.SendAsync(Array.Empty<byte>(), 0, new IPEndPoint(IPAddress.Broadcast, udpPort));
                int start = Environment.TickCount;
                while (true)
                {
                    while (client.Available > 0)
                    {
                        UdpReceiveResult res = await client.ReceiveAsync();
                        string result = Encoding.ASCII.GetString(res.Buffer);
                        Trace.TraceInformation(result);
                        using (XmlTextReader reader = new XmlTextReader(new StringReader(result)))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(JenkinsInstance));
                            if (serializer.CanDeserialize(reader))
                            {
                                JenkinsInstance inst = (JenkinsInstance)serializer.Deserialize(reader);
                                inst.Address = res.RemoteEndPoint.Address;
                                (list ?? (list = new List<JenkinsInstance>())).Add(inst);
                            }
                            else
                            {
                                Trace.TraceError($"Unknown broadcast response: {result}");
                            }
                        }
                    }
                    if (Environment.TickCount > start + timeout)
                    {
                        break;
                    }
                    await Task.Delay(100);
                }
            }

            return list;
        }
    }
}
