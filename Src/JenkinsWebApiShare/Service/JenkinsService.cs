



namespace JenkinsWebApi.Service;

internal class JenkinsService(Uri host, string apiKey) : JsonService(host, SourceGenerationContext.Default, new BearerAuthenticator(apiKey))
{
    private const string apiFormat = JenkinsDeserializer.ApiFormat;

    public async Task<JenkinsRunProgress> RunJobAsync(string jobName, JenkinsBuildParameters? parameters, JenkinsRunConfig? runConfig, IProgress<JenkinsRunProgress>? progress, CancellationToken cancellationToken)
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
        string jobUrl = new Uri(host, $"/job/{jobName}").ToString();
        JenkinsRunProgress last = new JenkinsRunProgress(jobName, jobUrl, res);

        // return if 
        if (res.Location == null ||                             // Jenkins server is too old and does not return the location
            res.StatusCode == HttpStatusCode.Conflict ||        // Jenkins job is disabled 
            runConfig.RunMode == JenkinsRunMode.Immediately)    // RunMode = Immediately
        {
            return last;
        }

        string? buildUrl = null;
        while (!cancellationToken.IsCancellationRequested)
        {
            string str = await GetApiStringAsync(res.Location.ToString(), cancellationToken);
            if (str.StartsWith("<buildableItem"))
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
                string schema = await GetApiStringAsync(new Uri(res.Location, "api/schema").ToString(), cancellationToken);
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
            //Console.WriteLine($"IsBuilding: {run.IsBuilding}");
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
                response.EnsureSuccess();
            }

            return new PostRunRes() { Location = response.Headers.Location, StatusCode = response.StatusCode };
        }
    }

    private async Task<string> GetApiStringAsync(string path, CancellationToken cancellationToken)
    {
        using (HttpResponseMessage response = await this.client!.GetAsync(path + apiFormat, cancellationToken))
        {
            response.EnsureSuccess();
            string str = await response.Content.ReadAsStringAsync(cancellationToken);
            return str;
        }
    }
}
