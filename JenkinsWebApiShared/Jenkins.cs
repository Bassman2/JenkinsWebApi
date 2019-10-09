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
using System.Threading;
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
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                // open without login
                return;
            }
            
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
            
            var res = PostLoginAsync("j_acegi_security_check", new FormUrlEncodedContent(list), CancellationToken.None).Result;
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
                JenkinsSecurityCsrfDefaultCrumbIssuer crumb = GetAsync<JenkinsSecurityCsrfDefaultCrumbIssuer>("crumbIssuer/api/xml", CancellationToken.None).Result;
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
            return await GetServerAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins server configuration.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins server configuration</returns>
        public async Task<JenkinsModelHudson> GetServerAsync(CancellationToken cancellationToken)
        {
            JenkinsModelHudson server = await GetAsync<JenkinsModelHudson>("api/xml", cancellationToken);
            return server;
        }
        

        /// <summary>
        /// Get the Jenkins server credentials.
        /// </summary>
        /// <returns>Jenkins server credentials</returns>
        /// <remark>Only in V2 or above</remark>
        public async Task<JenkinsComCloudbeesPluginsCredentialsViewCredentialsActionRootActionImpl> GetCredentialsAsync()
        {
            return await GetCredentialsAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins server credentials.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins server credentials</returns>
        /// <remark>Only in V2 or above</remark>
        public async Task<JenkinsComCloudbeesPluginsCredentialsViewCredentialsActionRootActionImpl> GetCredentialsAsync(CancellationToken cancellationToken)
        {
            JenkinsComCloudbeesPluginsCredentialsViewCredentialsActionRootActionImpl credentials = await GetAsync<JenkinsComCloudbeesPluginsCredentialsViewCredentialsActionRootActionImpl>("credentials/api/xml", cancellationToken);
            return credentials;
        }

        /// <summary>
        /// Get a list of all Jenkins users.
        /// </summary>
        /// <returns>List of all Jenkins users</returns>
        /// <remarks>For compatibility to old Jenkins version. For new version use GetAsyncPeopleAsync instead.</remarks>
        public async Task<JenkinsModelViewPeople> GetPeopleAsync()
        {
            return await GetPeopleAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get a list of all Jenkins users.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>List of all Jenkins users</returns>
        /// <remarks>For compatibility to old Jenkins version. For new version use GetAsyncPeopleAsync instead.</remarks>
        public async Task<JenkinsModelViewPeople> GetPeopleAsync(CancellationToken cancellationToken)
        {
            JenkinsModelViewPeople people = await GetAsync<JenkinsModelViewPeople>("people/api/xml", cancellationToken);
            return people;
        }

        /// <summary>
        /// Get a list of all Jenkins users.
        /// </summary>
        /// <returns>List of all Jenkins users</returns>
        public async Task<JenkinsModelViewAsynchPeoplePeople> GetAsyncPeopleAsync()
        {
            return await GetAsyncPeopleAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get a list of all Jenkins users.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>List of all Jenkins users</returns>
        public async Task<JenkinsModelViewAsynchPeoplePeople> GetAsyncPeopleAsync(CancellationToken cancellationToken)
        {
            JenkinsModelViewAsynchPeoplePeople people = await GetAsync<JenkinsModelViewAsynchPeoplePeople>("asynchPeople/api/xml", cancellationToken);
            return people;
        }

        /// <summary>
        /// Get the data of one Jenkins user.
        /// </summary>
        /// <param name="userName">Name of the Jenkins user</param>
        /// <returns>Jenkins user data</returns>
        public async Task<JenkinsModelUser> GetUserAsync(string userName)
        {
            return await GetUserAsync(userName, CancellationToken.None);
        }

        /// <summary>
        /// Get the data of one Jenkins user.
        /// </summary>
        /// <param name="userName">Name of the Jenkins user</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins user data</returns>
        public async Task<JenkinsModelUser> GetUserAsync(string userName, CancellationToken cancellationToken)
        {
            JenkinsModelUser user = await GetAsync<JenkinsModelUser>($"user/{userName}/api/xml", cancellationToken);
            return user;
        }

        /// <summary>
        /// Get the data of the current login user.
        /// </summary>
        /// <returns>Jenkins user data</returns>
        public async Task<JenkinsModelUser> GetCurrentUserAsync()
        {
            return await GetCurrentUserAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get the data of the current login user.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins user data</returns>
        public async Task<JenkinsModelUser> GetCurrentUserAsync(CancellationToken cancellationToken)
        {
            JenkinsModelUser user = await GetAsync<JenkinsModelUser>("me/api/xml", cancellationToken);
            return user;
        }
        
        /// <summary>
        /// Get the Jenkins job data.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <returns>Jenkins job data</returns>
        public async Task<JenkinsModelAbstractItem> GetJobAsync(string jobName)
        {
            return await GetJobAsync(jobName, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins job data.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins job data</returns>
        public async Task<JenkinsModelAbstractItem> GetJobAsync(string jobName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            string str = await GetStringAsync($"job/{jobName}/api/xml", cancellationToken);
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
            return await GetViewAsync(viewName);
        }

        /// <summary>
        /// Get the Jenkins view data.
        /// </summary>
        /// <param name="viewName">Name of the view</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns></returns>
        public async Task<JenkinsModelView> GetViewAsync(string viewName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            string str = await GetStringAsync($"view/{viewName}/api/xml", cancellationToken);
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
            return await GetBuildAsync(jobName, buildNum, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins build data.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins build data</returns>
        public async Task<JenkinsModelRun> GetBuildAsync(string jobName, int buildNum, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentException(nameof(jobName));
            }

            string str = await GetStringAsync($"job/{jobName}/{buildNum}/api/xml", cancellationToken);
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
            return await GetLastBuildAsync(jobName, CancellationToken.None);
        }

        /// <summary>
        /// Get the last build
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins build data</returns>
        public async Task<JenkinsModelRun> GetLastBuildAsync(string jobName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentException(nameof(jobName));
            }

            string str = await GetStringAsync($"job/{jobName}/lastBuild/api/xml", cancellationToken);
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
            return await GetTestReportAsync(jobName, buildNum, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins build test report.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins build test report if available; null if not</returns>
        public async Task<JenkinsTasksJunitTestResult> GetTestReportAsync(string jobName, int buildNum, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            JenkinsTasksJunitTestResult report = await GetAsync<JenkinsTasksJunitTestResult>($"/job/{jobName}/{buildNum}/testReport/api/xml", cancellationToken);
            return report;
        }

        /// <summary>
        /// Get the Jenkins queue.
        /// </summary>
        /// <returns>Jenkins queue</returns>
        public async Task<JenkinsModelQueue> GetQueueAsync()
        {
            return await GetQueueAsync(CancellationToken.None); 
        }

        /// <summary>
        /// Get the Jenkins queue.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins queue</returns>
        public async Task<JenkinsModelQueue> GetQueueAsync(CancellationToken cancellationToken)
        {
            JenkinsModelQueue queue = await GetAsync<JenkinsModelQueue>("queue/api/xml", cancellationToken);
            return queue;
        }

        /// <summary>
        /// Get overall load statistics
        /// </summary>
        /// <returns>Statistics result</returns>
        public async Task<JenkinsModelOverallLoadStatistics> GetOverallLoadStatisticsAsync()
        {
            return await GetOverallLoadStatisticsAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get overall load statistics
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Statistics result</returns>
        public async Task<JenkinsModelOverallLoadStatistics> GetOverallLoadStatisticsAsync(CancellationToken cancellationToken)
        {
            JenkinsModelOverallLoadStatistics statistics = await GetAsync<JenkinsModelOverallLoadStatistics>("overallLoad/api/xml", cancellationToken);
            return statistics;
        }

        /// <summary>
        /// Get infos about all Jenkins nodes.
        /// </summary>
        /// <returns>Nodes infos</returns>
        public async Task<JenkinsModelComputerSet> GetComputerSetAsync()
        {
            return await GetComputerSetAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get infos about all Jenkins nodes.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Nodes infos</returns>
        public async Task<JenkinsModelComputerSet> GetComputerSetAsync(CancellationToken cancellationToken)
        {
            JenkinsModelComputerSet computerSet = await GetAsync<JenkinsModelComputerSet>("computer/api/xml", cancellationToken);
            return computerSet;
        }

        /// <summary>
        /// Get infos about the Jenkins master node
        /// </summary>
        /// <returns>Master node infos</returns>
        public async Task<JenkinsModelHudsonMasterComputer> GetMasterComputerAsync()
        {
            return await GetMasterComputerAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get infos about the Jenkins master node
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Master node infos</returns>
        public async Task<JenkinsModelHudsonMasterComputer> GetMasterComputerAsync(CancellationToken cancellationToken)
        {
            JenkinsModelHudsonMasterComputer computer = await GetAsync<JenkinsModelHudsonMasterComputer>("computer/(master)/api/xml", cancellationToken);
            return computer;
        }

        /// <summary>
        /// Get infos of a Jenkins slave node.
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>Node infos</returns>
        public async Task<JenkinsSlavesSlaveComputer> GetComputerAsync(string computerName)
        {
            return await GetComputerAsync(computerName, CancellationToken.None);
        }

        /// <summary>
        /// Get infos of a Jenkins slave node.
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Node infos</returns>
        public async Task<JenkinsSlavesSlaveComputer> GetComputerAsync(string computerName, CancellationToken cancellationToken)
        {
            JenkinsSlavesSlaveComputer computer = await GetAsync<JenkinsSlavesSlaveComputer>($"computer/{computerName}/api/xml", cancellationToken);
            return computer;
        }

        /// <summary>
        /// Get extended infos of a Jenkins slave node.
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>Node infos</returns>
        public async Task<JenkinsComputerExt> GetComputerExtAsync(string computerName)
        {
            return await GetComputerExtAsync(computerName, CancellationToken.None);
        }

        /// <summary>
        /// Get extended infos of a Jenkins slave node.
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Node infos</returns>
        public async Task<JenkinsComputerExt> GetComputerExtAsync(string computerName, CancellationToken cancellationToken)
        {
            string str = await GetStringAsync($"computer/{computerName}/configure", cancellationToken);
            JenkinsComputerExt computerExt = new JenkinsComputerExt();
            computerExt.Description = TrimDescription(str);
            computerExt.Label = TrimLabel(str);
            return computerExt;
        }

        /// <summary>
        /// Get the log of the computer
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>Log text</returns>
        public async Task<string> GetComputerLogAsync(string computerName)
        {
            return await GetComputerLogAsync(computerName, CancellationToken.None);
        }

        /// <summary>
        /// Get the log of the computer
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Log text</returns>
        public async Task<string> GetComputerLogAsync(string computerName, CancellationToken cancellationToken)
        {
            string str = await GetStringAsync($"computer/{computerName}/logText/progressiveHtml", cancellationToken);
            return HtmlEntity.DeEntitize(str);
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
            return await RunComputerScriptAsync(computerName, script, CancellationToken.None);
        }

        /// <summary>
        /// Run node script
        /// </summary>
        /// <param name="computerName">Name of the computer</param>
        /// <param name="script">Script to run</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result</returns>
        /// <example>
        /// println "hostname".execute().text
        /// println InetAddress.localHost
        /// println InetAddress.localHost.hostAddress 
        /// println InetAddress.localHost.hostName
        /// println InetAddress.localHost.canonicalHostName
        /// </example>
        public async Task<string> RunComputerScriptAsync(string computerName, string script, CancellationToken cancellationToken)
        {
            var parms = new Dictionary<string, string>();
            parms.Add("script", script);
            var content = new FormUrlEncodedContent(parms);
            using (HttpResponseMessage response = await this.client.PostAsync($"computer/{computerName}/script", content, cancellationToken))
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
            return await RunMasterComputerScriptAsync(script, CancellationToken.None);
        }

        /// <summary>
        /// Run node script on master
        /// </summary>
        /// <param name="script">Script to run</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result</returns>
        /// <example>
        /// println "hostname".execute().text
        /// println InetAddress.localHost
        /// println InetAddress.localHost.hostAddress 
        /// println InetAddress.localHost.hostName
        /// println InetAddress.localHost.canonicalHostName
        /// </example>
        public async Task<string> RunMasterComputerScriptAsync(string script, CancellationToken cancellationToken)
        {
            var parms = new Dictionary<string, string>();
            //parms.Add("Jenkins-Crumb", crumb);
            parms.Add("script", script);
            var content = new FormUrlEncodedContent(parms);
            using (HttpResponseMessage response = await this.client.PostAsync("computer/(master)/script", content, cancellationToken))
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
            return await GetComputerHostAddressAsync(computerName, CancellationToken.None);
        }

        /// <summary>
        /// Get IP of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>IP address</returns>
        public async Task<string> GetComputerHostAddressAsync(string computerName, CancellationToken cancellationToken)
        {
            return await RunComputerScriptAsync(computerName, "println InetAddress.localHost.hostAddress", cancellationToken);
        }

        /// <summary>
        /// Get host name of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>Host name</returns>
        public async Task<string> GetComputerHostNameAsync(string computerName)
        {
            return await GetComputerHostNameAsync(computerName, CancellationToken.None);
        }

        /// <summary>
        /// Get host name of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Host name</returns>
        public async Task<string> GetComputerHostNameAsync(string computerName, CancellationToken cancellationToken)
        {
            return await RunComputerScriptAsync(computerName, "println InetAddress.localHost.hostName", cancellationToken);
        }

        /// <summary>
        /// Get canonical host name of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>Canonical host name</returns>
        public async Task<string> GetComputerCanonicalHostNameAsync(string computerName)
        {
            return await GetComputerCanonicalHostNameAsync(computerName, CancellationToken.None);
        }

        /// <summary>
        /// Get canonical host name of a node
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Canonical host name</returns>
        public async Task<string> GetComputerCanonicalHostNameAsync(string computerName, CancellationToken cancellationToken)
        {
            return await RunComputerScriptAsync(computerName, "println InetAddress.localHost.canonicalHostName", cancellationToken);
        }

        /// <summary>
        /// Get infos of a Jenkins slave node label.
        /// </summary>
        /// <param name="labelName">Name of the label</param>
        /// <returns>Label info</returns>
        public async Task<JenkinsModelLabelsLabelAtom> GetLabelAsync(string labelName)
        {
            return await GetLabelAsync(labelName, CancellationToken.None);
        }

        /// <summary>
        /// Get infos of a Jenkins slave node label.
        /// </summary>
        /// <param name="labelName">Name of the label</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Label info</returns>
        public async Task<JenkinsModelLabelsLabelAtom> GetLabelAsync(string labelName, CancellationToken cancellationToken)
        {
            JenkinsModelLabelsLabelAtom label = await GetAsync<JenkinsModelLabelsLabelAtom>($"label/{labelName}/api/xml", cancellationToken);
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
            return await GetEnvInjectVarListAsync(jobName, buildNum, CancellationToken.None);
        }

        /// <summary>
        /// Get environment variable list
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Get environment variables</returns>
        /// <remarks>Plugin &quot;Environment Injector Plugin&quot; must be installed </remarks>
        public async Task<JenkinsOrgJenkinsciPluginsEnvinjectEnvInjectVarList> GetEnvInjectVarListAsync(string jobName, int buildNum, CancellationToken cancellationToken)
        {
            JenkinsOrgJenkinsciPluginsEnvinjectEnvInjectVarList label = await GetAsync<JenkinsOrgJenkinsciPluginsEnvinjectEnvInjectVarList>($"job/{jobName}/{buildNum}/injectedEnvVars/api/xml", cancellationToken);
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
            return await GetBuildGraph(jobName, buildNum, CancellationToken.None);
        }

        /// <summary>
        /// Get build graph
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Get graph information</returns>
        /// <remarks>Plugin &quot;buildgraph-view&quot; must be installed </remarks>
        public async Task<JenkinsOrgJenkinsciPluginsBuildgraphviewBuildGraph> GetBuildGraph(string jobName, int buildNum, CancellationToken cancellationToken)
        {
            JenkinsOrgJenkinsciPluginsBuildgraphviewBuildGraph label = await GetAsync<JenkinsOrgJenkinsciPluginsBuildgraphviewBuildGraph>($"job/{jobName}/{buildNum}/BuildGraph/api/xml", cancellationToken);
            return label;
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<object> RunJobAsync(string jobName)
        {
            return await RunJobAsync(jobName, null, CancellationToken.None);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="parameters">Parameters for the Jenkins job</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<object> RunJobAsync(string jobName, JenkinsBuildParameters parameters)
        {
            return await RunJobAsync(jobName, parameters, CancellationToken.None);            
        }
        
        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<object> RunJobAsync(string jobName, CancellationToken cancellationToken)
        {
            return await RunJobAsync(jobName, null, cancellationToken);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="parameters">Parameters for the Jenkins job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<object> RunJobAsync(string jobName, JenkinsBuildParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            Uri location = null;
            if (parameters == null || parameters.IsEmpty)
            {
                location = await PostRunAsync($"/job/{jobName}/build?delay=0sec", null, cancellationToken);
            }
            else
            {
                location = await PostRunAsync($"/job/{jobName}/build?delay=0sec", parameters, cancellationToken);
            }


            // Key	Value  Location http://localhost:8080/queue/item/9/
            // Key	Value  Location http://localhost:8080/queue/item/10/
            // Key Value  Location http://localhost:8080/job/Freestyle%20Test%20Parameter/

            if (location != null && location.ToString().Contains("/queue/item/"))
            {
                // if no delay item.Executable will be null
                await Task.Delay(100);
                string schema = await GetStringAsync(new Uri(location, "api/Schema").ToString(), cancellationToken);
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
            await StopJobAsync(jobName, buildNum, CancellationToken.None);
        }

        /// <summary>
        /// Stop a running Jenkins build
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task StopJobAsync(string jobName, int buildNum, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            await PostRunAsync($"/job/{jobName}/{buildNum}/stop", null, cancellationToken);
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
            await CreateJobAsync(jobName, stream, fileName, CancellationToken.None);
        }

        /// <summary>
        /// Create a new job from an XML file.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="stream">XML data of the new job.</param>
        /// <param name="fileName">File name of the data</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task handle</returns>        
        public async Task CreateJobAsync(string jobName, Stream stream, string fileName, CancellationToken cancellationToken)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent(jobName), "name");
            content.Add(new StreamContent(stream), "file0", fileName);
            await PostRunAsync("createItem", content, cancellationToken);
        }

        /// <summary>
        /// Clone a new job from an existing.
        /// </summary>
        /// <param name="fromJobName">Existing job name.</param>
        /// <param name="newJobName">Name of the new job.</param>
        /// <returns>Task handle</returns>
        public async Task CloneJobAsync(string fromJobName, string newJobName)
        {
            await CloneJobAsync(fromJobName, newJobName, CancellationToken.None);
        }

        /// <summary>
        /// Clone a new job from an existing.
        /// </summary>
        /// <param name="fromJobName">Existing job name.</param>
        /// <param name="newJobName">Name of the new job.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task handle</returns>
        public async Task CloneJobAsync(string fromJobName, string newJobName, CancellationToken cancellationToken)
        {
            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();
            param.Add(new KeyValuePair<string, string>("name", newJobName));
            param.Add(new KeyValuePair<string, string>("mode", "Build"));
            param.Add(new KeyValuePair<string, string>("from", fromJobName));
            FormUrlEncodedContent content = new FormUrlEncodedContent(param);
            await PostRunAsync("createItem", content, cancellationToken);
        }

        /// <summary>
        /// Enter into the "quiet down" mode.
        /// </summary>
        /// <returns></returns>
        public async Task QuiteDownAsync()
        {
            await QuiteDownAsync(CancellationToken.None);
        }

        /// <summary>
        /// Enter into the "quiet down" mode.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns></returns>
        public async Task QuiteDownAsync(CancellationToken cancellationToken)
        {
            await PostRunAsync("quietDown", null, cancellationToken);
        }

        /// <summary>
        /// Cancel the "quiet down" mode.
        /// </summary>
        /// <returns></returns>
        public async Task CancelQuietDown()
        {
            await CancelQuietDown(CancellationToken.None);
        }

        /// <summary>
        /// Cancel the "quiet down" mode.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns></returns>
        public async Task CancelQuietDown(CancellationToken cancellationToken)
        {
            await PostRunAsync("cancelQuietDown", null, cancellationToken);
        }

        /// <summary>
        /// Restart the Jenkins Server
        /// </summary>
        /// <returns></returns>
        public async Task RestartAsync()
        {
            await RestartAsync(CancellationToken.None);
        }

        /// <summary>
        /// Restart the Jenkins Server
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns></returns>
        public async Task RestartAsync(CancellationToken cancellationToken)
        {
            await PostRunAsync("restart", null, cancellationToken);
        }

        /// <summary>
        /// Save restart the Jenkins Server if no job is running
        /// </summary>
        /// <returns></returns>
        public async Task SaveRestartAsync()
        {
            await SaveRestartAsync(CancellationToken.None);
        }

        /// <summary>
        /// Save restart the Jenkins Server if no job is running
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns></returns>
        public async Task SaveRestartAsync(CancellationToken cancellationToken)
        {
            await PostRunAsync("safeRestart", null, cancellationToken);
        }

        /// <summary>
        /// Launch slave agent
        /// </summary>
        /// <param name="hostName">Name of the slave host</param>
        /// <returns></returns>
        public async Task LaunchSlaveAgent(string hostName)
        {
            await LaunchSlaveAgent(hostName, CancellationToken.None);
        }

        /// <summary>
        /// Launch slave agent
        /// </summary>
        /// <param name="hostName">Name of the slave host</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns></returns>
        public async Task LaunchSlaveAgent(string hostName, CancellationToken cancellationToken)
        {
            await GetStringAsync($"computer/{hostName}/launchSlaveAgent", cancellationToken);
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

        private async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken) where T : class
        {
            T value = null;
            using (HttpResponseMessage response = await this.client.GetAsync(path, cancellationToken))
            {
                response.EnsureSuccess();
                string str = await response.Content.ReadAsStringAsync();
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                value = (T)serializer.Deserialize(new StringReader(str));
            }
            return value;
        }

        private async Task<string> GetStringAsync(string path, CancellationToken cancellationToken) 
        {
            string value = null;
            using (HttpResponseMessage response = await this.client.GetAsync(path, cancellationToken))
            {
                response.EnsureSuccess();
                value = await response.Content.ReadAsStringAsync();
            }
            return value;
        }

        private async Task<bool> PostLoginAsync(string path, HttpContent content, CancellationToken cancellationToken)
        {
            using (HttpResponseMessage response = await this.client.PostAsync(path, content, cancellationToken))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return false;
                }
                response.EnsureSuccess();
            }
            return true;
        }

        private async Task<Uri> PostRunAsync(string path, HttpContent content, CancellationToken cancellationToken) 
        {
            Uri location = null;
            using (HttpResponseMessage response = await this.client.PostAsync(path, content, cancellationToken))
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
