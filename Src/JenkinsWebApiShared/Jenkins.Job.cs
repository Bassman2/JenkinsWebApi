using JenkinsWebApi.Internal;
using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace JenkinsWebApi
{
    public sealed partial class Jenkins
    {
        /// <summary>
        /// Get the Jenkins job data.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <returns>Jenkins job data</returns>
        public async Task<JenkinsModelJob> GetJobAsync(string jobName)
        {
            return await GetJobAsync(jobName, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins job data.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins job data</returns>
        /// <remarks><include file="Comments.xml" path="comments/comment[@id='job']/*"/></remarks>
        public async Task<JenkinsModelJob> GetJobAsync(string jobName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            string str = await GetApiStringAsync($"job/{jobName}", cancellationToken);
            JenkinsModelJob job = JenkinsDeserializer.DeserializeJob<JenkinsModelJob>(str);
            return job;
        }

        /// <summary>
        /// Get the Jenkins job data.
        /// </summary>
        /// <typeparam name="T">Job class type</typeparam>
        /// <param name="jobName">Name of the job</param>
        /// <returns>Jenkins job data</returns>
        /// <remarks><include file="Comments.xml" path="comments/comment[@id='job']/*"/></remarks>
        public async Task<T> GetJobAsync<T>(string jobName) where T : JenkinsModelJob
        {
            return await GetJobAsync<T>(jobName, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins job data.
        /// </summary>
        /// <typeparam name="T">Job class type</typeparam>
        /// <param name="jobName">Name of the job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins job data</returns>
        /// <remarks><include file="Comments.xml" path="comments/comment[@id='job']/*"/></remarks>
        public async Task<T> GetJobAsync<T>(string jobName, CancellationToken cancellationToken) where T : JenkinsModelJob
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            string str = await GetApiStringAsync($"job/{jobName}", cancellationToken);
            var job = JenkinsDeserializer.DeserializeJob<T>(str);
            return job;
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<JenkinsRunProgress> RunJobAsync(string jobName)
        {
            return await RunJobAsync(jobName, null, null, null, CancellationToken.None);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<JenkinsRunProgress> RunJobAsync(string jobName, CancellationToken cancellationToken)
        {
            return await RunJobAsync(jobName, null, null, null, cancellationToken);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="parameters">Parameters for the Jenkins job</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<JenkinsRunProgress> RunJobAsync(string jobName, JenkinsBuildParameters parameters)
        {
            return await RunJobAsync(jobName, parameters, null, null, CancellationToken.None);
        }
        
        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="parameters">Parameters for the Jenkins job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<JenkinsRunProgress> RunJobAsync(string jobName, JenkinsBuildParameters parameters, CancellationToken cancellationToken)
        {
            return await RunJobAsync(jobName, parameters, null, null, cancellationToken);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="parameters">Parameters for the Jenkins job</param>
        /// <param name="runConfig"></param>
        /// <param name="progress"></param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<JenkinsRunProgress> RunJobAsync(string jobName, JenkinsBuildParameters parameters, JenkinsRunConfig runConfig, IProgress<JenkinsRunProgress> progress)
        {
            return await RunJobAsync(jobName, parameters, runConfig, progress, CancellationToken.None);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="parameters">Parameters for the Jenkins job</param>
        /// <param name="runConfig"></param>
        /// <param name="progress"></param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<JenkinsRunProgress> RunJobAsync(string jobName, JenkinsBuildParameters parameters, JenkinsRunConfig runConfig, IProgress<JenkinsRunProgress> progress, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            runConfig = runConfig ?? this.RunConfig ?? throw new Exception("No JenkinsRunConfig available.");

            string path = $"/job/{jobName}/{(parameters == null ? "build" : "buildWithParameters")}?delay={runConfig.StartDelay}sec";
            var res = await PostRunAsync(path, null, cancellationToken);

            //Uri location =
            // store last progress info to compare for changes
            string jobUrl = new Uri(this.BaseAddress, $"/job/{jobName}").ToString();
            JenkinsRunProgress last = new JenkinsRunProgress(jobName, jobUrl, res);

            // return if 
            if (res.Location == null ||                             // Jenkins server is too old and does not return the location
                res.StatusCode == HttpStatusCode.Conflict ||        // Jenkins job is disabled 
                runConfig.RunMode == JenkinsRunMode.Immediately)    // RunMode = Immediately
            {
                return last;
            }

            string buildUrl = null;
            while (!cancellationToken.IsCancellationRequested)
            {
                string str = await GetApiStringAsync(res.Location.ToString(), cancellationToken);
                if (str.StartsWith("<buildableItem"))
                {
                    JenkinsModelQueueBuildableItem item = JenkinsDeserializer.Deserialize<JenkinsModelQueueBuildableItem>(str);
                    Debug.WriteLine($"buildableItem: IsPending={item.IsPending} IsBlocked={item.IsBlocked} IsBuildable={item.IsBuildable} IsStuck={item.IsStuck} Why={item.Why}");
                    UpdateProgress(ref last, progress, jobName, jobUrl, item);
                    if (item.IsStuck && runConfig.ReturnIfBlocked)
                    {
                        return last;
                    }
                }
                else if (str.StartsWith("<blockedItem"))
                {
                    JenkinsModelQueueBlockedItem item = JenkinsDeserializer.Deserialize<JenkinsModelQueueBlockedItem>(str);
                    Debug.WriteLine($"blockedItem: IsBlocked={item.IsBlocked} IsBuildable={item.IsBuildable} IsStuck={item.IsStuck} Why={item.Why}");
                    UpdateProgress(ref last, progress, jobName, jobUrl, item);
                    if (item.IsStuck && runConfig.ReturnIfBlocked)
                    {
                        return last;
                    }

                }
                else if (str.StartsWith("<leftItem"))
                {
                    JenkinsModelQueueLeftItem item = JenkinsDeserializer.Deserialize<JenkinsModelQueueLeftItem>(str);
                    Debug.WriteLine($"leftItem: IsCancelled={item.IsCancelled} IsBuildable={item.IsBlocked} IsBuildable={item.IsBuildable} IsStuck={item.IsStuck} Why={item.Why}");
                    UpdateProgress(ref last, progress, jobName, jobUrl, item);
                    if (item.Executable != null)
                    {
                        buildUrl = item.Executable.Url;
                        break;
                    }
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
                string str = await GetApiStringAsync(buildUrl.ToString(), cancellationToken);
                JenkinsModelRun run = JenkinsDeserializer.DeserializeBuild<JenkinsModelRun>(str);
                Debug.WriteLine($"modelRun: IsBuilding={run.IsBuilding} IsKeepLog ={run.IsKeepLog} Result={run.Result}");
                UpdateProgress(ref last, progress, jobName, jobUrl, run);
                Console.WriteLine($"IsBuilding: {run.IsBuilding}");
                if (runConfig.RunMode <= JenkinsRunMode.Started && run.IsBuilding)
                {
                    // build started
                    return last;
                }
                if (!run.IsBuilding)
                {
                    // build finished
                    return last;
                }
                await Task.Delay(runConfig.PollingTime, cancellationToken);
            }
            return last;
        }

        private void UpdateProgress(ref JenkinsRunProgress last, IProgress<JenkinsRunProgress> progress, string jobName, string jobUrl, object item)
        {
            JenkinsRunProgress runProgress = new JenkinsRunProgress(jobName, jobUrl, item);

            if (last != runProgress)
            {
                this.RunProgress?.Invoke(this, runProgress);
                progress?.Report(runProgress);
                last = runProgress;
            }
        }

        /// <summary>
        /// Stop a running Jenkins build
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
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
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task StopJobAsync(string jobName, int buildNum, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            await PostRunAsync($"/job/{jobName}/{buildNum}/stop", null, cancellationToken);
        }

        /// <summary>
        /// Create a new job.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="viewName">Nameof the view.</param>
        /// <param name="type">Type of the class to create.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks><include file="Comments.xml" path="comments/comment[@id='job']/*"/></remarks>
        public async Task CreateJobAsync(string jobName, string viewName, Type type)
        {
            await CreateJobAsync(jobName, viewName, type, CancellationToken.None);
        }

        /// <summary>
        /// Create a new job.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="viewName">Nameof the view.</param>
        /// <param name="type">Type of the class to create.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <remarks><include file="Comments.xml" path="comments/comment[@id='job']/*"/></remarks>
        public async Task CreateJobAsync(string jobName, string viewName, Type type, CancellationToken cancellationToken)
        {
            var content = new Dictionary<string, string>();
            content.Add("name", jobName);
            content.Add("mode", SerializableClassAttribute.GetClassName(type));
            // http://tiny:8080/view/all/createItem
            await PostAsync("/view/{viewName}/createItem", content, cancellationToken);
        }

        /// <summary>
        /// Create a new job from an XML file.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<string> GetJobConfigAsync(string jobName)
        {
            return await GetJobConfigAsync(jobName, CancellationToken.None);
        }

        /// <summary>
        /// Create a new job from an XML file.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<string> GetJobConfigAsync(string jobName, CancellationToken cancellationToken)
        {
            string str = await GetStringAsync($"job/{jobName}/config.xml", cancellationToken);
            return str;
        }

        /// <summary>
        /// Create a new job from an XML file.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<XmlDocument> GetJobConfigXmlAsync(string jobName)
        {
            return await GetJobConfigXmlAsync(jobName, CancellationToken.None);
        }

        /// <summary>
        /// Create a new job from an XML file.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<XmlDocument> GetJobConfigXmlAsync(string jobName, CancellationToken cancellationToken)
        {
            string str = await GetJobConfigAsync(jobName, cancellationToken);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(str);
            return doc;
        }

        /// <summary>
        /// Create a new job from an XML file.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="config"></param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task SetJobConfigAsync(string jobName, string config)
        {
            await SetJobConfigAsync(jobName, config, CancellationToken.None);
        }

        /// <summary>
        /// Create a new job from an XML file.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="config"></param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task SetJobConfigAsync(string jobName, string config, CancellationToken cancellationToken)
        {
            await PostAsync($"job/{jobName}/config.xml", cancellationToken);
        }

        /// <summary>
        /// Create a new job from an XML file.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="config"></param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task SetJobConfigXmlAsync(string jobName, XmlDocument config)
        {
            await SetJobConfigXmlAsync(jobName, config, CancellationToken.None);
        }

        /// <summary>
        /// Create a new job from an XML file.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="config"></param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task SetJobConfigXmlAsync(string jobName, XmlDocument config, CancellationToken cancellationToken)
        {
            await SetJobConfigAsync(jobName, config.ToString(), cancellationToken);
        }

        /// <summary>
        /// Clone a new job from an existing.
        /// </summary>
        /// <param name="fromJobName">Existing job name.</param>
        /// <param name="newJobName">Name of the new job.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
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
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task CloneJobAsync(string fromJobName, string newJobName, CancellationToken cancellationToken)
        {
            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("name", newJobName),
                new KeyValuePair<string, string>("mode", "Build"),
                new KeyValuePair<string, string>("from", fromJobName)
            };
            FormUrlEncodedContent content = new FormUrlEncodedContent(param);
            await PostRunAsync("createItem", content, cancellationToken);
        }

        /// <summary>
        /// Delete an existing job.
        /// </summary>
        /// <param name="jobName">Name of the job to delete.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteJobAsync(string jobName)
        {
            await DeleteJobAsync(jobName, CancellationToken.None);
        }

        /// <summary>
        /// Delete an existing job.
        /// </summary>
        /// <param name="jobName">Name of the job to delete.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteJobAsync(string jobName, CancellationToken cancellationToken)
        {
            await DeleteAsync($"job/{jobName}/", cancellationToken);
        }

        /// <summary>
        /// Delete an existing job.
        /// </summary>
        /// <param name="jobName">Name of the job to delete.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DisableJobAsync(string jobName)
        {
            await DisableJobAsync(jobName, CancellationToken.None);
        }

        /// <summary>
        /// Delete an existing job.
        /// </summary>
        /// <param name="jobName">Name of the job to delete.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DisableJobAsync(string jobName, CancellationToken cancellationToken)
        {
            //Dictionary<string, string> content = new Dictionary<string, string>();
            //content.Add("Submit", "Disable Project");
            //await PostAsync($"job/{jobName}/disable", content, cancellationToken);
            await PostAsync($"/job/{jobName}/disable", cancellationToken);
        }

        /// <summary>
        /// Delete an existing job.
        /// </summary>
        /// <param name="jobName">Name of the job to delete.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task EnableJobAsync(string jobName)
        {
            await EnableJobAsync(jobName, CancellationToken.None);
        }

        /// <summary>
        /// Delete an existing job.
        /// </summary>
        /// <param name="jobName">Name of the job to delete.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task EnableJobAsync(string jobName, CancellationToken cancellationToken)
        {
            //Dictionary<string, string> content = new Dictionary<string, string>();
            //content.Add("Submit", "Enable");
            //await PostAsync($"job/{jobName}/enable", content, cancellationToken);
            await PostAsync($"job/{jobName}/enable", cancellationToken);
        }

        /// <summary>
        /// Get the Jenkins job data.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <returns>Jenkins job data</returns>
        public async Task<string> GetJobDescriptionAsync(string jobName)
        {
            return await GetJobDescriptionAsync(jobName, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins job data.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins job data</returns>
        public async Task<string> GetJobDescriptionAsync(string jobName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }
            string str = await GetApiStringAsync($"job/{jobName}/description", cancellationToken);
            return str;
        }

        /// <summary>
        /// Get the Jenkins job data.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="description"></param>
        /// <returns>Jenkins job data</returns>
        public async Task SetJobDescriptionAsync(string jobName, string description)
        {
            await SetJobDescriptionAsync(jobName, description, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins job data.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="description"></param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins job data</returns>
        public async Task SetJobDescriptionAsync(string jobName, string description, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            Dictionary<string, string> content = new Dictionary<string, string>();
            content.Add("description", description);
            await PostAsync($"job/{jobName}/description", content, cancellationToken);
        }

    }
}

