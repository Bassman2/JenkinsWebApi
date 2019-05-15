using HtmlAgilityPack;
using JenkinsWebApi.Internal;
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
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JenkinsWebApi
{
    /// <summary>
    /// Main class of the Jenkins server API
    /// </summary>
    public sealed class Jenkins : IDisposable
    {
        private Uri host;
        private HttpClientHandler handler;
        private HttpClient client;
        private bool disposed = false;
        private const int udpPort = 33848;

        private static Type[] viewTypes = AppDomain.CurrentDomain.GetAssemblies()
                                        .SelectMany(s => s.GetTypes())
                                        .Where(t => typeof(JenkinsModelView).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
                                        .ToArray();

        private static Type[] jobTypes = AppDomain.CurrentDomain.GetAssemblies()
                                        .SelectMany(s => s.GetTypes())
                                        .Where(t => typeof(JenkinsModelJob).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
                                        .ToArray();

        private static Type[] buildTypes = AppDomain.CurrentDomain.GetAssemblies()
                                        .SelectMany(s => s.GetTypes())
                                        .Where(t => typeof(JenkinsModelAbstractBuild).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
                                        .ToArray();

        /// <summary>
        /// Initializes a new instance of the Jenkins class.
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server</param>
        public Jenkins(Uri host)
        {
            if (host == null)
            {
                throw new ArgumentNullException("host");
            }

            this.host = host;
            
            // connect
            this.handler = new HttpClientHandler();
            this.handler.CookieContainer = new System.Net.CookieContainer();
            this.handler.UseCookies = true;
            this.client = new HttpClient(this.handler);
            this.client.BaseAddress = host;
        }

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
        /// <param name="login">Login for the Jenkins server</param>
        /// <param name="password">Password for the Jenkins server</param>
        public Jenkins(Uri host, string login, string password) : this(host)
        {
            if (!Login(login, password))
            {
                throw new Exception("Login failed!");
            }
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
        /// Release allocated resources.
        /// </summary>
        public void Dispose()
        {
            if (!this.disposed)
            {
                this.client?.Dispose();
                this.client = null;
                
                this.handler?.Dispose();
                this.handler = null;
                
                // note disposing has been done.
                this.disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Login to the Jenkins server.
        /// </summary>
        /// <param name="login">User login name</param>
        /// <param name="password">User login password</param>
        /// <returns>true if login success; false if failed</returns>
        public bool Login(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new ArgumentNullException(nameof(login));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            var list = new Dictionary<string, string>();
            list.Add("j_username", login);
            list.Add("j_password", password);
            list.Add("remember_me", "on");
            list.Add("Submit", "Anmelden");
            
            var res = PostLoginAsync("j_acegi_security_check", new FormUrlEncodedContent(list)).Result;
            if (res)
            {
                Crumb();
            }
            return res;
        }

        private void Crumb()
        {
            // only on newer Jenkins versions
            // handle CSRF Protection
            try
            {
                JenkinsSecurityCsrfDefaultCrumbIssuer crumb = GetAsync<JenkinsSecurityCsrfDefaultCrumbIssuer>("crumbIssuer/api/xml").Result;
                this.client.DefaultRequestHeaders.Add(crumb.CrumbRequestField, crumb.Crumb);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Get the Jenkins server configuration.
        /// </summary>
        /// <returns>Jenkins server configuration</returns>
        public async Task<JenkinsModelHudson> GetServerAsync()
        {
            JenkinsModelHudson server = await GetAsync<JenkinsModelHudson>("api/xml");
            return server;
        }

        /// <summary>
        /// Get the Jenkins server credentials.
        /// </summary>
        /// <returns>Jenkins server credentials</returns>
        /// <remark>Only in V2 or above</remark>
        public async Task<JenkinsComCloudbeesPluginsCredentialsViewCredentialsActionRootActionImpl> GetCredentialsAsync()
        {
            JenkinsComCloudbeesPluginsCredentialsViewCredentialsActionRootActionImpl credentials = await GetAsync<JenkinsComCloudbeesPluginsCredentialsViewCredentialsActionRootActionImpl>("credentials/api/xml");
            return credentials;
        }

        /// <summary>
        /// Get a list of all Jenkins users.
        /// </summary>
        /// <returns>List of all Jenkins users</returns>
        /// <remarks>For compatibility to old Jenkins version. For new version use GetAsyncPeopleAsync instead.</remarks>
        public async Task<JenkinsModelViewPeople> GetPeopleAsync()
        {
            JenkinsModelViewPeople people = await GetAsync<JenkinsModelViewPeople>("people/api/xml");
            return people;
        }

        /// <summary>
        /// Get a list of all Jenkins users.
        /// </summary>
        /// <returns>List of all Jenkins users</returns>
        public async Task<JenkinsModelViewAsynchPeoplePeople> GetAsyncPeopleAsync()
        {
            JenkinsModelViewAsynchPeoplePeople people = await GetAsync<JenkinsModelViewAsynchPeoplePeople>("asynchPeople/api/xml");
            return people;
        }

        /// <summary>
        /// Get the data of one Jenkins user.
        /// </summary>
        /// <param name="userName">Name of the Jenkins user</param>
        /// <returns>Jenkins user data</returns>
        public async Task<JenkinsModelUser> GetUserAsync(string userName)
        {
            JenkinsModelUser user = await GetAsync<JenkinsModelUser>($"user/{userName}/api/xml");
            return user;
        }

        /// <summary>
        /// Get the data of the current login user.
        /// </summary>
        /// <returns>Jenkins user data</returns>
        public async Task<JenkinsModelUser> GetCurrentUserAsync()
        {
            JenkinsModelUser user = await GetAsync<JenkinsModelUser>("me/api/xml");
            return user;
        }

        

        /// <summary>
        /// Get the Jenkins job data.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <returns>Jenkins job data</returns>
        public async Task<JenkinsModelAbstractItem> GetJobAsync(string jobName)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }
            
            string str = await GetStringAsync($"job/{jobName}/api/xml");
            JenkinsModelAbstractItem job = Deserialize<JenkinsModelAbstractItem>(str, jobTypes);
            return job;
        }
        
        /// <summary>
        /// Get the Jenkins view data.
        /// </summary>
        /// <param name="viewName">Name of the view</param>
        /// <returns></returns>
        public async Task<JenkinsModelView> GetViewAsync(string viewName)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            string str = await GetStringAsync($"view/{viewName}/api/xml");
            JenkinsModelView view = Deserialize<JenkinsModelView>(str, viewTypes);
            return view;
        }

        /// <summary>
        /// Get the Jenkins build data.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <returns>Jenkins build data</returns>
        public async Task<JenkinsModelRun> GetBuildAsync(string jobName, int buildNum)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentException(nameof(jobName));
            }

            string str = await GetStringAsync($"job/{jobName}/{buildNum}/api/xml");
            JenkinsModelRun build = Deserialize<JenkinsModelRun>(str, buildTypes);
            return build;
        }

        /// <summary>
        /// Get the last build
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <returns>Jenkins build data</returns>
        public async Task<JenkinsModelRun> GetLastBuildAsync(string jobName)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentException(nameof(jobName));
            }

            string str = await GetStringAsync($"job/{jobName}/lastBuild/api/xml");
            JenkinsModelRun build = Deserialize<JenkinsModelRun>(str, buildTypes);
            return build;
        }

        /// <summary>
        /// Get the Jenkins build test report.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <returns>Jenkins build test report if available; null if not</returns>
        public async Task<JenkinsTasksJunitTestResult> GetTestReportAsync(string jobName, int buildNum)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            JenkinsTasksJunitTestResult report = await GetAsync<JenkinsTasksJunitTestResult>($"/job/{jobName}/{buildNum}/testReport/api/xml");
            return report;
        }

        /// <summary>
        /// Get the Jenkins queue.
        /// </summary>
        /// <returns>Jenkins queue</returns>
        public async Task<JenkinsModelQueue> GetQueueAsync()
        {
            JenkinsModelQueue queue = await GetAsync<JenkinsModelQueue>("queue/api/xml");
            return queue;
        }
        
        /// <summary>
        /// Get overall load statistics
        /// </summary>
        /// <returns>Statistics result</returns>
        public async Task<JenkinsModelOverallLoadStatistics> GetOverallLoadStatisticsAsync()
        {
            JenkinsModelOverallLoadStatistics statistics = await GetAsync<JenkinsModelOverallLoadStatistics>("overallLoad/api/xml");
            return statistics;
        }

        /// <summary>
        /// Get infos about all Jenkins nodes.
        /// </summary>
        /// <returns>Nodes infos</returns>
        public async Task<JenkinsModelComputerSet> GetComputerSetAsync()
        {
            JenkinsModelComputerSet computerSet = await GetAsync<JenkinsModelComputerSet>("computer/api/xml");
            return computerSet;
        }

        /// <summary>
        /// Get infos about the Jenkins master node
        /// </summary>
        /// <returns>Master node infos</returns>
        public async Task<JenkinsModelHudsonMasterComputer> GetMasterComputerAsync()
        {
            JenkinsModelHudsonMasterComputer computer = await GetAsync<JenkinsModelHudsonMasterComputer>("computer/(master)/api/xml");
            return computer;
        }

        /// <summary>
        /// Get infos of a Jenkins slave node.
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>Node infos</returns>
        public async Task<JenkinsSlavesSlaveComputer> GetComputerAsync(string computerName)
        {
            JenkinsSlavesSlaveComputer computer = await GetAsync<JenkinsSlavesSlaveComputer>($"computer/{computerName}/api/xml");
            return computer;
        }

        public async Task<JenkinsComputerExt> GetComputerExtAsync(string computerName)
        {
            using (HttpResponseMessage response = await this.client.GetAsync($"computer/{computerName}/configure"))
            {
                string str = await response.Content.ReadAsStringAsync();
                JenkinsComputerExt computerExt = new JenkinsComputerExt();
                computerExt.Description = TrimDescription(str);
                computerExt.Label = TrimLabel(str);
                return computerExt;
            }
        }

        /// <summary>
        /// Get the log of the computer
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>Log text</returns>
        public async Task<string> GetComputerLogAsync(string computerName)
        {
            using (HttpResponseMessage response = await this.client.GetAsync($"computer/{computerName}/logText/progressiveHtml"))
            {
                response.EnsureSuccess();
                string str = await response.Content.ReadAsStringAsync();
                return HtmlEntity.DeEntitize(str);
            }
        }

        /// <summary>
        /// Run node script
        /// </summary>
        /// <param name="computerName">Name of the computer</param>
        /// <param name="script">Script to run</param>
        /// <returns>Result</returns>
        /// <example>
        /// println "hostname".execute().text
        /// println InetAddress.localHost
        /// println InetAddress.localHost.hostAddress 
        /// println InetAddress.localHost.hostName
        /// println InetAddress.localHost.canonicalHostName
        /// </example>
        public async Task<string> RunComputerScriptAsync(string computerName, string script)
        {
            var parms = new Dictionary<string, string>();
            parms.Add("script", script);
            var content = new FormUrlEncodedContent(parms);
            using (HttpResponseMessage response = await this.client.PostAsync($"computer/{computerName}/script", content))
            {
                response.EnsureSuccess();
                string str = await response.Content.ReadAsStringAsync();
                return TrimScript(str);
            }
        }

        /// <summary>
        /// Run node script on master
        /// </summary>
        /// <param name="script">Script to run</param>
        /// <returns>Result</returns>
        /// <example>
        /// println "hostname".execute().text
        /// println InetAddress.localHost
        /// println InetAddress.localHost.hostAddress 
        /// println InetAddress.localHost.hostName
        /// println InetAddress.localHost.canonicalHostName
        /// </example>
        public async Task<string> RunMasterComputerScriptAsync(string script)
        {
            var parms = new Dictionary<string, string>();
            //parms.Add("Jenkins-Crumb", crumb);
            parms.Add("script", script);
            var content = new FormUrlEncodedContent(parms);
            using (HttpResponseMessage response = await this.client.PostAsync("computer/(master)/script", content))
            {
                response.EnsureSuccess();
                string str = await response.Content.ReadAsStringAsync();
                return TrimScript(str);
            }
        }

        /// <summary>
        /// Get IP of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>IP address</returns>
        public async Task<string> GetComputerHostAddressAsync(string computerName)
        {
            return await RunComputerScriptAsync(computerName, "println InetAddress.localHost.hostAddress");
        }

        /// <summary>
        /// Get host name of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>Host name</returns>
        public async Task<string> GetComputerHostNameAsync(string computerName)
        {
            return await RunComputerScriptAsync(computerName, "println InetAddress.localHost.hostName");
        }

        /// <summary>
        /// Get canonical host name of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>Canonical host name</returns>
        public async Task<string> GetComputerCanonicalHostNameAsync(string computerName)
        {
            return await RunComputerScriptAsync(computerName, "println InetAddress.localHost.canonicalHostName");
        }

        /// <summary>
        /// Get infos of a Jenkins slave node label.
        /// </summary>
        /// <param name="labelName">Name of the label</param>
        /// <returns>Label info</returns>
        public async Task<JenkinsModelLabelsLabelAtom> GetLabelAsync(string labelName)
        {
            JenkinsModelLabelsLabelAtom label = await GetAsync<JenkinsModelLabelsLabelAtom>($"label/{labelName}/api/xml");
            return label;
        }

        /// <summary>
        /// Get environment variable list
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <returns>Get environment variables</returns>
        /// <remarks>Plugin &quot;Environment Injector Plugin&quot; must be installed </remarks>
        public async Task<JenkinsOrgJenkinsciPluginsEnvinjectEnvInjectVarList> GetEnvInjectVarListAsync(string jobName, int buildNum)
        {
            JenkinsOrgJenkinsciPluginsEnvinjectEnvInjectVarList label = await GetAsync<JenkinsOrgJenkinsciPluginsEnvinjectEnvInjectVarList>($"job/{jobName}/{buildNum}/injectedEnvVars/api/xml");
            return label;
        }

        /// <summary>
        /// Get build graph
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <returns>Get graph information</returns>
        /// <remarks>Plugin &quot;buildgraph-view&quot; must be installed </remarks>
        public async Task<JenkinsOrgJenkinsciPluginsBuildgraphviewBuildGraph> GetBuildGraph(string jobName, int buildNum)
        {
            JenkinsOrgJenkinsciPluginsBuildgraphviewBuildGraph label = await GetAsync<JenkinsOrgJenkinsciPluginsBuildgraphviewBuildGraph>($"job/{jobName}/{buildNum}/BuildGraph/api/xml");
            return label;
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="parameters">Parameters for the Jenkins job</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<object> RunJobAsync(string jobName, JenkinsBuildParameters parameters = null)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            Uri location = null;
            if (parameters == null || parameters.IsEmpty)
            {
                location = await PostRunAsync($"/job/{jobName}/build?delay=0sec", null);
            }
            else 
            {
                location = await PostRunAsync($"/job/{jobName}/build?delay=0sec", parameters);
            }


            // Key	Value  Location http://localhost:8080/queue/item/9/
            // Key	Value  Location http://localhost:8080/queue/item/10/
            // Key Value  Location http://localhost:8080/job/Freestyle%20Test%20Parameter/

            if (location != null && location.ToString().Contains("/queue/item/"))
            {
                // if no delay item.Executable will be null
                await Task.Delay(100);
                string schema = await GetStringAsync(new Uri(location, "api/Schema").ToString());
                //object item = await GetAsync<>(new Uri(location, "api/xml/").ToString());
                //return item;
            }

            return null;            
        }

        /// <summary>
        /// Stop a running Jenkins build
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        public async Task StopJobAsync(string jobName, int buildNum)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            await PostRunAsync($"/job/{jobName}/{buildNum}/stop", null);
        }


        /// <summary>
        /// Create a new job from an XML file.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="stream">XML data of the new job.</param>
        /// <param name="fileName">File name of the data</param>
        /// <returns>Task handle</returns>        
        public async Task CreateJobAsync(string jobName, Stream stream, string fileName)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent(jobName), "name");
            content.Add(new StreamContent(stream), "file0", fileName);
            await PostRunAsync("createItem", content);
        }

        /// <summary>
        /// Clone a new job from an existing.
        /// </summary>
        /// <param name="fromJobName">Existing job name.</param>
        /// <param name="newJobName">Name of the new job.</param>
        /// <returns>Task handle</returns>
        public async Task CloneJobAsync(string fromJobName, string newJobName)
        {
            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();
            param.Add(new KeyValuePair<string, string>("name", newJobName));
            param.Add(new KeyValuePair<string, string>("mode", "Build"));
            param.Add(new KeyValuePair<string, string>("from", fromJobName));
            FormUrlEncodedContent content = new FormUrlEncodedContent(param);
            await PostRunAsync("createItem", content);
        }


        /// <summary>
        /// Enter into the "quiet down" mode.
        /// </summary>
        /// <returns></returns>
        public async Task QuiteDownAsync()
        {
            await PostRunAsync("quietDown", null);
        }

        /// <summary>
        /// Cancel the "quiet down" mode.
        /// </summary>
        /// <returns></returns>
        public async Task CancelQuietDown()
        {
            await PostRunAsync("cancelQuietDown", null);
        }

        /// <summary>
        /// Restart the Jenkins Server
        /// </summary>
        /// <returns></returns>
        public async Task RestartAsync()
        {
            await PostRunAsync("restart", null);
        }

        /// <summary>
        /// Save restart the Jenkins Server if no job is running
        /// </summary>
        /// <returns></returns>
        public async Task SaveRestartAsync()
        {
            await PostRunAsync("safeRestart", null);
        }

        /// <summary>
        /// Launch slave agent
        /// </summary>
        /// <param name="hostName">Name of the slave host</param>
        /// <returns></returns>
        public async Task LaunchSlaveAgent(string hostName)
        {
            await GetStringAsync($"computer/{hostName}/launchSlaveAgent");
        }
        

        /// <summary>
        /// Get a list with all Jenkins servers in the local subnet.
        /// </summary>
        /// <param name="timeout">Timeout of the search.</param>
        /// <returns>List with Jenkins servers</returns>
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

        private async Task<T> GetAsync<T>(string path) where T : class
        {
            T value = null;
            using (HttpResponseMessage response = await this.client.GetAsync(path))
            {
                response.EnsureSuccess();
                string str = await response.Content.ReadAsStringAsync();
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                value = (T)serializer.Deserialize(new StringReader(str));
            }
            return value;
        }

        private async Task<string> GetStringAsync(string path) 
        {
            string value = null;
            using (HttpResponseMessage response = await this.client.GetAsync(path))
            {
                response.EnsureSuccess();
                value = await response.Content.ReadAsStringAsync();
            }
            return value;
        }

        private async Task<bool> PostLoginAsync(string path, HttpContent content)
        {
            using (HttpResponseMessage response = await this.client.PostAsync(path, content))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return false;
                }
                response.EnsureSuccess();
            }
            return true;
        }

        private async Task<Uri> PostRunAsync(string path, HttpContent content) 
        {
            Uri location = null;
            using (HttpResponseMessage response = await this.client.PostAsync(path, content))
            {
                response.EnsureSuccess();
                location = response.Headers.Location;
            }

            return location;
        }

        private T Deserialize<T>(string xmlText, IEnumerable<Type> classTypes)
        {
            using (XmlTextReader reader = new XmlTextReader(new StringReader(xmlText)))
            {
                foreach (Type t in classTypes)
                {
                    XmlSerializer serializer = new XmlSerializer(t);
                    if (serializer.CanDeserialize(reader))
                    {
                        return (T)serializer.Deserialize(reader);
                    }
                }
            }
            throw new Exception($"Not class found for this type: {xmlText.Substring(1, xmlText.IndexOf(' '))}");
        }

        private string TrimScript(string str)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(str);
            var el = doc.DocumentNode.SelectSingleNode("//*[@id='main-panel']/pre[last()]");        // Jenkins ver. 1.424.6
            if (el != null)
            {
                return HtmlEntity.DeEntitize(el.InnerText).Trim();
            }
            return null;
        }

        private string TrimDescription(string str)
        {
            str = str.Replace("&&", "");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(str);
            var el = doc.DocumentNode.SelectSingleNode("//input[@name='_.nodeDescription']");        // Jenkins ver. 1.424.6
            if (el != null)
            {
                HtmlAttribute attr = el.Attributes["value"];
                string res = HtmlEntity.DeEntitize(attr.Value);
                return res.Trim();
            }
            return null;
        }

        private string TrimLabel(string str)
        {
            str = str.Replace("&&", "");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(str);
            var el = doc.DocumentNode.SelectSingleNode("//input[@name='_.labelString']");        // Jenkins ver. 1.424.6
            if (el != null)
            {
                HtmlAttribute attr = el.Attributes["value"];
                string res = HtmlEntity.DeEntitize(attr.Value);
                return res.Trim();
            }
            return null;
        }
    }
}
