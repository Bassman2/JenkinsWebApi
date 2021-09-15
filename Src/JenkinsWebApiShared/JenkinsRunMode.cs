namespace JenkinsWebApi
{
    /// <summary>
    /// Jenkins job run mode
    /// </summary>
    public enum JenkinsRunMode
    {
        /// <summary>
        /// JobRunAsync returns immediately without and informations about the build.
        /// </summary>
        Immediately,
        
        /// <summary>
        /// JobRunAsync returns after the job was queued.
        /// </summary>
        Queued, 

        /// <summary>
        /// JobRunAsync returns after the job was started.
        /// </summary>
        Started, 

        /// <summary>
        /// JobRunAsync returns after the job was finished.
        /// </summary>
        Ready
    }
}
