namespace JenkinsWebApi
{
    /// <summary>
    /// Configuration class for JobRunAsync
    /// </summary>
    public class JenkinsRunConfig
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public JenkinsRunConfig()
        {
            this.RunMode = JenkinsRunMode.Finished;
            this.ReturnIfBlocked = true;
            this.PollingTime = 1000;
            this.StartDelay = 0;
        }

        /// <summary>
        /// Get or set the JenkinsRunMode. The default value is <see cref="JenkinsRunMode.Finished"/> Ready.
        /// </summary>
        public JenkinsRunMode RunMode { get; set;}

        /// <summary>
        /// Select the behaviour if a jenkins build blocks. The default value is true.
        /// </summary>
        public bool ReturnIfBlocked { get; set; }
 
        /// <summary>
        /// Status update polling time in milli seconds. The default value is 1 second.
        /// </summary>
        public int PollingTime { get; set; }

        /// <summary>
        /// RunJobAsync start delay in seconds. Defalut value is 0 seconds.
        /// </summary>
        public int StartDelay { get; set; }
    }
}
