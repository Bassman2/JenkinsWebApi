using JenkinsWebApi.Internal;
using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JenkinsWebApi.ObjectModel
{
    /// <summary>
    /// Main class of the Jenkins server Object Model
    /// </summary>
    public sealed class JenkinsServer : IDisposable
    {
        private readonly Jenkins jenkins;
        private JenkinsModelJenkins modelJenkins;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jenkins"></param>
        public JenkinsServer(Jenkins jenkins)
        {
            this.jenkins = jenkins;
            Update();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server.</param>
        public JenkinsServer(string host) : this(new Uri(host), null, null) 
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server.</param>
        public JenkinsServer(Uri host) : this(host, null, null)
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server.</param>
        /// <param name="login">Login for the Jenkins server.</param>
        /// <param name="passwordOrToken">Password or API token for the Jenkins server</param>
        public JenkinsServer(string host, string login, string passwordOrToken) : this(new Uri(host), login, passwordOrToken)
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="host">Host URL of the Jenkins server.</param>
        /// <param name="login">Login for the Jenkins server.</param>
        /// <param name="passwordOrToken">Password or API token for the Jenkins server.</param>
        public JenkinsServer(Uri host, string login, string passwordOrToken)
        { 
            this.jenkins = JenkinsRun.Run(() => new Jenkins(host, login, passwordOrToken));
            Update();
        }

        /// <summary>
        /// Release allocated resources.
        /// </summary>
        public void Dispose()
        {
            this.jenkins?.Dispose();
        }

        /// <summary>
        /// Description of the Jenkins server
        /// </summary>
        public string Description { get { return this.modelJenkins.Description; } }

        /// <summary>
        /// Url address of the Jenkins server
        /// </summary>
        public Uri Url { get { return new Uri(this.modelJenkins.Url); } }

        /// <summary>
        /// Primary view of the Jenkins server.
        /// </summary>
        public JenkinsView PrimaryView { get { return new JenkinsView(this.jenkins, this.modelJenkins.PrimaryView); } }

        /// <summary>
        /// Get all global views.
        /// </summary>
        public IEnumerable<JenkinsView> Views { get { return this.modelJenkins.Views.Select(v => new JenkinsView(this.jenkins, v)); } }

        /// <summary>
        /// Get all jobs.
        /// </summary>
        public IEnumerable<JenkinsJob> Jobs { get { return this.modelJenkins.Jobs.Select(j => new JenkinsJob(this.jenkins, j)); } }


        /// <summary>
        /// Get a view.
        /// </summary>
        /// <param name="viewName">Name of the view to get.</param>
        /// <returns></returns>
        public JenkinsView GetView(string viewName)
        {
            return new JenkinsView(this.jenkins, viewName);
        }

        /// <summary>
        /// Get a job.
        /// </summary>
        /// <param name="jobName">>Name of the job to get.</param>
        /// <returns></returns>
        public JenkinsJob GetJob(string jobName)
        {
            return new JenkinsJob(this.jenkins, jobName);
        }

        /// <summary>
        /// Reload server properties.
        /// </summary>
        public void Update()
        {
            JenkinsRun.Run(() => this.modelJenkins = this.jenkins.GetServerAsync().Result);
        }

        
    }
}
