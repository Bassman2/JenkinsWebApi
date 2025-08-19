namespace JenkinsWebApi;

/// <summary>
/// Represents a Jenkins client that can interact with a Jenkins server.
/// </summary>
public sealed class Jenkins : JsonService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Jenkins"/> class using a store key and application name.
    /// </summary>
    /// <param name="storeKey">The key used to store credentials or settings.</param>
    /// <param name="appName">The name of the application using the Jenkins client.</param>
    public Jenkins(string storeKey, string appName) : base(storeKey, appName, SourceGenerationContext.Default)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Jenkins"/> class using a host URI, an optional authenticator, and an application name.
    /// </summary>
    /// <param name="host">The URI of the Jenkins server.</param>
    /// <param name="authenticator">The authenticator used for authentication, or <c>null</c> if not required.</param>
    /// <param name="appName">The name of the application using the Jenkins client.</param>
    public Jenkins(Uri host, IAuthenticator? authenticator, string appName) : base(host, authenticator, appName, SourceGenerationContext.Default)
    { }

    /// <summary>
    /// Gets the URL used to test authentication with the Jenkins server.
    /// </summary>
    protected override string? AuthenticationTestUrl => null; //"api/json";

    /// <summary>
    /// JobRunAsync global configuration.
    /// </summary>
    public JenkinsRunConfig? RunConfig { get; set; } = new JenkinsRunConfig();


    /// <summary>
    /// Gets the version string of the Jenkins server asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the version string of the Jenkins server, or <c>null</c> if not available.
    /// </returns>
    public override async Task<string?> GetVersionStringAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        using HttpResponseMessage response = await client.GetAsync("api/json", cancellationToken);

        if (response.Headers.TryGetValues("x-jenkins", out IEnumerable<string>? values))
        {
            return values.FirstOrDefault() ?? "0.0.0";
        }
        return "0.0.0";
    }


    /// <summary>
    /// Runs a Jenkins job asynchronously.
    /// </summary>
    /// <param name="jobName">The name of the Jenkins job.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the progress of the Jenkins run.</returns>
    public async Task<JenkinsRunProgress> RunJobAsync(string jobName)
    {
        WebServiceException.ThrowIfNotConnected(client);

        return await RunJobIntAsync(jobName, null, RunConfig, null, CancellationToken.None);
    }

    /// <summary>
    /// Runs a Jenkins job asynchronously with a cancellation token.
    /// </summary>
    /// <param name="jobName">The name of the Jenkins job.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the progress of the Jenkins run.</returns>
    public async Task<JenkinsRunProgress> RunJobAsync(string jobName, CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        return await RunJobIntAsync(jobName, null, RunConfig, null, cancellationToken);
    }

    ///// <summary>
    ///// Run a Jenkins job.
    ///// </summary>
    ///// <param name="jobName">Name of the Jenkins job</param>
    ///// <param name="parameters">Parameters for the Jenkins job</param>
    ///// <returns>Result and number of the Jenkins build</returns>
    //public async Task<JenkinsRunProgress> RunJobAsync(string jobName, JenkinsBuildParameters parameters)
    //{
    //            WebServiceException.ThrowIfNotConnected(client);


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
    //            WebServiceException.ThrowIfNotConnected(client);


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
    //            WebServiceException.ThrowIfNotConnected(client);


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
    //            WebServiceException.ThrowIfNotConnected(client);


    //    return await service!.RunJobAsync(jobName, parameters, runConfig ?? RunConfig, progress, cancellationToken);
    //}


    private async Task<JenkinsRunProgress> RunJobIntAsync(string jobName, JenkinsBuildParameters? parameters, JenkinsRunConfig? runConfig, IProgress<JenkinsRunProgress>? progress, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(jobName))
        {
            throw new ArgumentNullException(nameof(jobName));
        }

        runConfig = runConfig ?? throw new Exception("No JenkinsRunConfig available.");

        string path = $"/job/{jobName}/{(parameters == null ? "build" : "buildWithParameters")}?delay={runConfig.StartDelay}sec";
        var res = await PostRunJobAsync(path, null, cancellationToken);

        //Uri location =
        // store last progress info to compare for changes
        string jobUrl = new Uri(Host, $"/job/{jobName}").ToString();
        JenkinsRunProgress last = new JenkinsRunProgress(jobName, jobUrl, res);

        // return if 
        if (res.Location == null ||                             // Jenkins server is too old and does not return the location
            res.StatusCode == HttpStatusCode.Conflict ||        // Jenkins job is disabled 
            runConfig.RunMode == JenkinsRunMode.Immediately)    // RunMode = Immediately
        {
            return last;
        }

        //string? buildUrl = null;
        while (!cancellationToken.IsCancellationRequested)
        {
            string? str = await GetStringAsync(res.Location.ToString(), cancellationToken);
            if (str!.StartsWith("<buildableItem"))
            {
                //JenkinsModelQueueBuildableItem item = JenkinsDeserializer.Deserialize<JenkinsModelQueueBuildableItem>(str);
                //Debug.WriteLine($"buildableItem: IsPending={item.IsPending} IsBlocked={item.IsBlocked} IsBuildable={item.IsBuildable} IsStuck={item.IsStuck} Why={item.Why}");
                //UpdateProgress(ref last, progress, jobName, jobUrl, item);
                //if (item.IsStuck && runConfig.ReturnIfBlocked)
                //{
                //    return last;
                //}
            }
            else if (str.StartsWith("<blockedItem"))
            {
                //JenkinsModelQueueBlockedItem item = JenkinsDeserializer.Deserialize<JenkinsModelQueueBlockedItem>(str);
                //Debug.WriteLine($"blockedItem: IsBlocked={item.IsBlocked} IsBuildable={item.IsBuildable} IsStuck={item.IsStuck} Why={item.Why}");
                //UpdateProgress(ref last, progress, jobName, jobUrl, item);
                //if (item.IsStuck && runConfig.ReturnIfBlocked)
                //{
                //    return last;
                //}

            }
            else if (str.StartsWith("<leftItem"))
            {
                //JenkinsModelQueueLeftItem item = JenkinsDeserializer.Deserialize<JenkinsModelQueueLeftItem>(str);
                //Debug.WriteLine($"leftItem: IsCancelled={item.IsCancelled} IsBuildable={item.IsBlocked} IsBuildable={item.IsBuildable} IsStuck={item.IsStuck} Why={item.Why}");
                //UpdateProgress(ref last, progress, jobName, jobUrl, item);
                //if (item.Executable != null)
                //{
                //    buildUrl = item.Executable.Url;
                //    break;
                //}
            }
            else
            {
                string? schema = await GetStringAsync(new Uri(res.Location, "api/schema").ToString(), cancellationToken);
                throw new Exception($"Unknown XML Schema!!!\r\n{schema}");
            }
            await Task.Delay(runConfig.PollingTime, cancellationToken);
        }

        if (runConfig.RunMode <= JenkinsRunMode.Queued)
        {
            return last;
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            //JenkinsModelRun? run = await GetApiBuildAsync<JenkinsModelRun>(buildUrl!.ToString(), cancellationToken);
            ////Debug.WriteLine($"modelRun: IsBuilding={run.IsBuilding} IsKeepLog ={run.IsKeepLog} Result={run.Result}");
            //UpdateProgress(ref last, progress, jobName, jobUrl, run);
            //Debug.WriteLine($"IsBuilding: {run.IsBuilding}");
            //if (runConfig.RunMode <= JenkinsRunMode.Started && run.IsBuilding)
            //{
            //    // build started
            //    return last;
            //}
            //if (!run.IsBuilding)
            //{
            //    // build finished
            //    return last;
            //}
            await Task.Delay(runConfig.PollingTime, cancellationToken);
        }
        return last;
    }

    private async Task<PostRunRes> PostRunJobAsync(string path, HttpContent? content, CancellationToken cancellationToken)
    {
        using (HttpResponseMessage response = await this.client!.PostAsync(path, content, cancellationToken))
        {
            if (response.StatusCode != HttpStatusCode.Conflict)
            {
                response.EnsureSuccessStatusCode();
            }

            return new PostRunRes() { Location = response.Headers.Location, StatusCode = response.StatusCode };
        }
    }
}
