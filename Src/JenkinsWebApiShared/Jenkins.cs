using System;

namespace JenkinsWebApi
{
    /// <summary>
    /// Main class of the Jenkins server API
    /// </summary>
    public sealed partial class Jenkins 
    {
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
            Connect(host, login, passwordOrToken);

            // init variables
            this.RunConfig = new JenkinsRunConfig();
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
            Authorize(login, passwordOrToken);

            // check if login success            
            return GetCurrentUserAsync().Result != null;
        }
        
        /// <summary>
        /// Get a view.
        /// </summary>
        /// <param name="viewName">Name of the view to get.</param>
        /// <returns></returns>
        public JenkinsView GetView(string viewName)
        {
            return new JenkinsView(this, viewName);
        }

        /// <summary>
        /// Get a job.
        /// </summary>
        /// <param name="jobName">>Name of the job to get.</param>
        /// <returns></returns>
        public JenkinsJob GetJob(string jobName)
        {
            return new JenkinsJob(this, jobName);
        }
    }
}
