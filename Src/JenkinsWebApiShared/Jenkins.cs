using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

#pragma warning disable IDE0063 // Use simple 'using' statement

namespace JenkinsWebApi
{
    /// <summary>
    /// Main class of the Jenkins server API
    /// </summary>
    public sealed partial class Jenkins : IDisposable
    {
        private HttpClientHandler handler;
        private HttpClient client;
        private const int udpPort = 33848;

        private readonly static Type[] viewTypes = AppDomain.CurrentDomain.GetAssemblies()
                                        .SelectMany(s => s.GetTypes())
                                        .Where(t => typeof(JenkinsModelView).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
                                        .ToArray();

        private readonly static Type[] jobTypes = AppDomain.CurrentDomain.GetAssemblies()
                                        .SelectMany(s => s.GetTypes())
                                        .Where(t => typeof(JenkinsModelJob).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
                                        .ToArray();

        private readonly static Type[] buildTypes = AppDomain.CurrentDomain.GetAssemblies()
                                        .SelectMany(s => s.GetTypes())
                                        .Where(t => typeof(JenkinsModelRun).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
                                        .ToArray();

        /// <summary>
        /// JobRunAsync progress event.
        /// </summary>
        public event EventHandler<JenkinsRunProgress> RunProgress;

        /// <summary>
        /// JobRunAsync global configuration.
        /// </summary>
        public JenkinsRunConfig RunConfig { get; set; }


        /// <summary>
        /// Initializes a new instance of the Jenkins class.
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server</param>
        public Jenkins(string host) : this(new Uri(host), null, null)
        { }

        /// <summary>
        /// Initializes a new instance of the Jenkins class.
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server</param>
        public Jenkins(Uri host) : this(host, null, null)
        { }

        /// <summary>
        /// Initializes a new instance of the Jenkins class.
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server</param>
        /// <param name="login">Login for the Jenkins server</param>
        /// <param name="passwordOrToken">Password or API token for the Jenkins server</param>
        public Jenkins(string host, string login, string passwordOrToken) : this(new Uri(host), login, passwordOrToken)
        { }        

        /// <summary>
        /// Initializes a new instance of the Jenkins class.
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server</param>
        /// <param name="login">Login for the Jenkins server</param>
        /// <param name="passwordOrToken">Password or API token for the Jenkins server</param>
        public Jenkins(Uri host, string login, string passwordOrToken) 
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            // init variables
            this.RunConfig = new JenkinsRunConfig();

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

        /// <summary>
        /// Login to the Jenkins server.
        /// </summary>
        /// <param name="login">Login for the Jenkins server</param>
        /// <param name="passwordOrToken">Password or API token for the Jenkins server</param>
        /// <returns>true if login success; false if failed</returns>
        public bool Login(string login, string passwordOrToken)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentNullException(nameof(login));
            }

            if (string.IsNullOrEmpty(passwordOrToken))
            {
                throw new ArgumentNullException(nameof(passwordOrToken));
            }

            // set authorization
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{login}:{passwordOrToken}")));

            // set crumb
            Crumb();

            // check if login success            
            return GetCurrentUserAsync().Result != null;
        }

        private void Crumb()
        {
            // only on newer Jenkins versions
            // handle CSRF Protection
            try
            {
                JenkinsSecurityCsrfDefaultCrumbIssuer crumb = GetAsync<JenkinsSecurityCsrfDefaultCrumbIssuer>("crumbIssuer/api/xml", CancellationToken.None).Result;
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
        /// <param name="timeout">Timeout of the search.</param>
        /// <returns>List with Jenkins servers</returns>
        /// <remarks>
        /// Since 2.220 the feature has been completely removed.<br/>
        /// Since 2.219 und LTS 2.204.2 this feature is deactivated by default, <br/>
        /// but can be activated by setting the system property hudson.DNSMultiCast.disabled to false or hudson.udp to 33848.<br/>
        /// <see href="https://www.jenkins.io/security/advisory/2020-01-29/"/><br/>
        /// <see href="https://www.jenkins.io/doc/book/managing/system-properties/"/> 
        /// </remarks>
        [Obsolete("Feature removed in newer Jenkins versions!")]
        public static async Task<IEnumerable<JenkinsInstance>> GetJenkinsInstancesAsync(long timeout = 2000)
        {
            List<JenkinsInstance> list = null;
            using (UdpClient client = new UdpClient())
            {
                await client.SendAsync(new byte[0], 0, new IPEndPoint(IPAddress.Broadcast, udpPort));
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

        public JenkinsView GetView(string viewName)
        {
            return new JenkinsView(this, viewName);
        }

        public JenkinsJob GetJob(string jobName)
        {
            return new JenkinsJob(this, jobName);
        }
    }
}
