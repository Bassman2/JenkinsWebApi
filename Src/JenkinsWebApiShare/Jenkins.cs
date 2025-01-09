namespace JenkinsWebApi;

/// <summary>
/// Main class of the Jenkins server API
/// </summary>
public sealed partial class Jenkins : IDisposable
{
    private JenkinsService? service;

    public Jenkins(string storeKey, string appName)
        : this(new Uri(KeyStore.Key(storeKey)?.Host!), KeyStore.Key(storeKey)!.Token!, appName)
    { }

    public Jenkins(Uri host, string token, string appName)
    {
        service = new(host, new BearerAuthenticator(token), appName);
    }
    
    public void Dispose()
    {
        if (this.service != null)
        {
            this.service.Dispose();
            this.service = null;
        }
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// JobRunAsync global configuration.
    /// </summary>
    public JenkinsRunConfig? RunConfig { get; set; } = new JenkinsRunConfig();

    /// <summary>
    /// Run a Jenkins job.
    /// </summary>
    /// <param name="jobName">Name of the Jenkins job</param>
    /// <returns>Result and number of the Jenkins build</returns>
    public async Task<JenkinsRunProgress> RunJobAsync(string jobName)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        return await service!.RunJobAsync(jobName, null, RunConfig, null, CancellationToken.None);
    }

    /// <summary>
    /// Run a Jenkins job.
    /// </summary>
    /// <param name="jobName">Name of the Jenkins job</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Result and number of the Jenkins build</returns>
    public async Task<JenkinsRunProgress> RunJobAsync(string jobName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNullOrNotConnected(this.service);

        return await service!.RunJobAsync(jobName, null, RunConfig, null, cancellationToken);
    }

    ///// <summary>
    ///// Run a Jenkins job.
    ///// </summary>
    ///// <param name="jobName">Name of the Jenkins job</param>
    ///// <param name="parameters">Parameters for the Jenkins job</param>
    ///// <returns>Result and number of the Jenkins build</returns>
    //public async Task<JenkinsRunProgress> RunJobAsync(string jobName, JenkinsBuildParameters parameters)
    //{
    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

    //    return await service!.RunJobAsync(jobName, parameters, RunConfig, null, CancellationToken.None);
    //}

    ///// <summary>
    ///// Run a Jenkins job.
    ///// </summary>
    ///// <param name="jobName">Name of the Jenkins job</param>
    ///// <param name="parameters">Parameters for the Jenkins job</param>
    ///// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    ///// <returns>Result and number of the Jenkins build</returns>
    //public async Task<JenkinsRunProgress> RunJobAsync(string jobName, JenkinsBuildParameters parameters, CancellationToken cancellationToken = default)
    //{
    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

    //    return await service!.RunJobAsync(jobName, parameters, RunConfig, null, cancellationToken);
    //}

    ///// <summary>
    ///// Run a Jenkins job.
    ///// </summary>
    ///// <param name="jobName">Name of the Jenkins job</param>
    ///// <param name="parameters">Parameters for the Jenkins job</param>
    ///// <param name="runConfig"></param>
    ///// <param name="progress"></param>
    ///// <returns>Result and number of the Jenkins build</returns>
    //public async Task<JenkinsRunProgress> RunJobAsync(string jobName, JenkinsBuildParameters parameters, JenkinsRunConfig? runConfig, IProgress<JenkinsRunProgress> progress)
    //{
    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

    //    return await service!.RunJobAsync(jobName, parameters, runConfig ?? RunConfig, progress, CancellationToken.None);
    //}

    ///// <summary>
    ///// Run a Jenkins job.
    ///// </summary>
    ///// <param name="jobName">Name of the Jenkins job</param>
    ///// <param name="parameters">Parameters for the Jenkins job</param>
    ///// <param name="runConfig"></param>
    ///// <param name="progress"></param>
    ///// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    ///// <returns>Result and number of the Jenkins build</returns>
    //public async Task<JenkinsRunProgress> RunJobAsync(string jobName, JenkinsBuildParameters parameters, JenkinsRunConfig? runConfig, IProgress<JenkinsRunProgress> progress, CancellationToken cancellationToken = default)
    //{
    //    WebServiceException.ThrowIfNullOrNotConnected(this.service);

    //    return await service!.RunJobAsync(jobName, parameters, runConfig ?? RunConfig, progress, cancellationToken);
    //}

}
