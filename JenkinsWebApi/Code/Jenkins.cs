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
                                        .Where(t => typeof(JenkinsView).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
                                        .ToArray();

        private static Type[] jobTypes = AppDomain.CurrentDomain.GetAssemblies()
                                        .SelectMany(s => s.GetTypes())
                                        .Where(t => typeof(JenkinsJob).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
                                        .ToArray();

        private static Type[] buildTypes = AppDomain.CurrentDomain.GetAssemblies()
                                        .SelectMany(s => s.GetTypes())
                                        .Where(t => typeof(JenkinsAbstractBuild).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract)
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
            //list.Add("from", "%2F");
            //list.Add("xml", "%7B%22j_username%22%3A+%22bs%22%2C+%22j_password%22%3A+%22VisualEnte6.1Sp0%22%2C+%22remember_me%22%3A+true%2C+%22from%22%3A+%22%2F%22%7D");
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
                JenkinsDefaultCrumbIssuer crumb = GetAsync<JenkinsDefaultCrumbIssuer>("crumbIssuer/api/xml").Result;
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
        public async Task<JenkinsHudson> GetServerAsync()
        {
            JenkinsHudson server = await GetAsync<JenkinsHudson>("api/xml");
            return server;
        }

        /// <summary>
        /// Get the Jenkins server credentials.
        /// </summary>
        /// <returns>Jenkins server credentials</returns>
        /// <remark>Only in V2 or above</remark>
        public async Task<JenkinsViewCredentialsActionRootActionImpl> GetCredentialsAsync()
        {
            JenkinsViewCredentialsActionRootActionImpl credentials = await GetAsync<JenkinsViewCredentialsActionRootActionImpl>("credentials/api/xml");
            return credentials;
        }        

        /// <summary>
        /// Get a list of all Jenkins users.
        /// </summary>
        /// <returns>List of all Jenkins users</returns>
        /// <remarks>For compatibility to old Jenkins version. For new version use GetAsyncPeopleAsync instead.</remarks>
        public async Task<JenkinsViewPeople> GetPeopleAsync()
        {
            JenkinsViewPeople people = await GetAsync<JenkinsViewPeople>("people/api/xml");
                return people;
            }

        /// <summary>
        /// Get a list of all Jenkins users.
        /// </summary>
        /// <returns>List of all Jenkins users</returns>
        public async Task<JenkinsViewAsynchPeoplePeople> GetAsyncPeopleAsync()
            {
           JenkinsViewAsynchPeoplePeople people = await GetAsync<JenkinsViewAsynchPeoplePeople>("asynchPeople/api/xml");
                return people;
            }

        /// <summary>
        /// Get the data of one Jenkins user.
        /// </summary>
        /// <param name="name">Name of the Jenkins user</param>
        /// <returns>Jenkins user data</returns>
        public async Task<JenkinsUser> GetUserAsync(string name)
        {
            JenkinsUser user = await GetAsync<JenkinsUser>($"user/{name}/api/xml");
            return user;
        }

        /// <summary>
        /// Get the data of the current login user.
        /// </summary>
        /// <returns>Jenkins user data</returns>
        public async Task<JenkinsUser> GetCurrentUserAsync()
        {
            JenkinsUser user = await GetAsync<JenkinsUser>("me/api/xml");
            return user;
        }

        

        /// <summary>
        /// Get the Jenkins job data.
        /// </summary>
        /// <param name="name">Name of the job</param>
        /// <returns>Jenkins job data</returns>
        public async Task<JenkinsAbstractItem> GetJobAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            string str = await GetStringAsync($"job/{name}/api/xml");
            JenkinsAbstractItem job = Deserialize<JenkinsAbstractItem>(str, jobTypes);
            return job;
        }
        
        /// <summary>
        /// Get the Jenkins view data.
        /// </summary>
        /// <param name="name">Name of the view</param>
        /// <returns></returns>
        public async Task<JenkinsView> GetViewAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            string str = await GetStringAsync($"view/{name}/api/xml");
            JenkinsView view = Deserialize<JenkinsView>(str, viewTypes);
            return view;
        }

        /// <summary>
        /// Get the Jenkins build data.
        /// </summary>
        /// <param name="name">Name of the Jenkins job</param>
        /// <param name="number">Number of the Jenkins build</param>
        /// <returns>Jenkins build data</returns>
        public async Task<JenkinsRun> GetBuildAsync(string name, int number)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            string str = await GetStringAsync($"job/{name}/{number}/api/xml");
            JenkinsRun build = Deserialize<JenkinsRun>(str, buildTypes);
            return build;
            }

        public async Task<JenkinsRun> GetLastBuildAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            string str = await GetStringAsync($"job/{name}/lastBuild/api/xml");
            JenkinsRun build = Deserialize<JenkinsRun>(str, buildTypes);
            return build;
        }

        /// <summary>
        /// Get the Jenkins build test report.
        /// </summary>
        /// <param name="name">Name of the Jenkins job</param>
        /// <param name="number">Number of the Jenkins build</param>
        /// <returns>Jenkins build test report if available; null if not</returns>
        public async Task<JenkinsTestResult> GetTestReportAsync(string name, int number)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            JenkinsTestResult report = await GetAsync<JenkinsTestResult>($"/job/{name}/{number}/testReport/api/xml");
            return report;
        }

        /*
        public async Task<JenkinsQueue> GetQueueAsync()
        {
            JenkinsQueue queue = await GetAsync<JenkinsQueue>("queue/api/xml");
            return queue;
        }
        */

        public async Task<JenkinsOverallLoadStatistics> GetOverallLoadStatisticsAsync()
        {
            JenkinsOverallLoadStatistics statistics = await GetAsync<JenkinsOverallLoadStatistics>("overallLoad/api/xml");
            return statistics;
        }

        public async Task<JenkinsComputerSet> GetComputerSetAsync()
        {
            JenkinsComputerSet computerSet = await GetAsync<JenkinsComputerSet>("computer/api/xml");
            return computerSet;
        }

        public async Task<JenkinsHudsonMasterComputer> GetMasterComputerAsync()
        {
            JenkinsHudsonMasterComputer computer = await GetAsync<JenkinsHudsonMasterComputer>("computer/(master)/api/xml");
            return computer;
        }

        public async Task<JenkinsSlaveComputer> GetComputerAsync(string computerName)
        {
            JenkinsSlaveComputer computer = await GetAsync<JenkinsSlaveComputer>($"computer/{computerName}/api/xml");
            return computer;
        }

        public async Task<JenkinsLabelAtom> GetLabelAsync(string labelName)
        {
            JenkinsLabelAtom label = await GetAsync<JenkinsLabelAtom>($"label/{labelName}/api/xml");
            return label;
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="name">Name of the Jenkins job</param>
        /// <param name="parameters">Parameters for the Jenkins job</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<object> RunJobAsync(string name, JenkinsBuildParameters parameters = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Uri location = null;
            if (parameters == null || parameters.IsEmpty)
            {
                location = await PostRunAsync($"/job/{name}/build?delay=0sec", null);
            }
            else 
            {
                location = await PostRunAsync($"/job/{name}/build?delay=0sec", parameters);
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
        /// <param name="name">Name of the Jenkins job</param>
        /// <param name="number">Number of the Jenkins build</param>
        public async Task StopJobAsync(string name, int number)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            await PostRunAsync($"/job/{name}/{number}/stop", null);
        }

        
        /// <summary>
        /// Create a new job from an XML file.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>        
        public async Task CreateJob(string jobName, Stream stream, string fileName)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent(jobName), "name");
            content.Add(new StreamContent(stream), "file0", fileName);
            await PostRunAsync("createItem", content);
        }

        public async Task CopyJob(string fromJobName, string newJobName)
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
        public async Task Restart()
        {
            await PostRunAsync("restart", null);
        }

        /// <summary>
        /// Save restart the Jenkins Server if no job is running
        /// </summary>
        /// <returns></returns>
        public async Task SaveRestart()
        {
            await PostRunAsync("safeRestart", null);
        }


        public static async Task<IEnumerable<JenkinsInstance>> GetJenkinsInstances(long timeout = 2000)
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

                //Stream stream = await response.Content.ReadAsStreamAsync();
                //XmlSerializer serializer = new XmlSerializer(typeof(T));
                //value = (T)serializer.Deserialize(stream);

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
    }
}
