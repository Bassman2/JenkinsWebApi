namespace JenkinsWebApi
{
    /// <summary>
    /// Type of the Jenkins start job parameter
    /// </summary>
    public enum JenkinsParameterType
    {
        /// <summary>
        /// The parameter is a string.
        /// </summary>
        String,

        /// <summary>
        /// The parameter is a boolean.
        /// </summary>
        Boolean,

        /// <summary>
        /// The parameter is a file stream.
        /// </summary>
        Stream        
    }
}
