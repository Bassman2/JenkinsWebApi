using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JenkinsWebApi
{
    public sealed partial class Jenkins
    {
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
        /// Get the Jenkins job data.
        /// </summary>
        /// <typeparam name="T">Job class type</typeparam>
        /// <param name="jobName">Name of the job</param>
        /// <returns>Jenkins job data</returns>
        public async Task<T> GetJobAsync<T>(string jobName)
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
        public async Task<T> GetJobAsync<T>(string jobName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            string str = await GetStringAsync($"job/{jobName}/api/xml", cancellationToken);
            var job = Deserialize<T>(str, jobTypes);
            return job;
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<string> RunJobAsync(string jobName)
        {
            return await RunJobAsync(jobName, null, CancellationToken.None);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="parameters">Parameters for the Jenkins job</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<string> RunJobAsync(string jobName, JenkinsBuildParameters parameters)
        {
            return await RunJobAsync(jobName, parameters, CancellationToken.None);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<string> RunJobAsync(string jobName, CancellationToken cancellationToken)
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
        public async Task<string> RunJobAsync(string jobName, JenkinsBuildParameters parameters, CancellationToken cancellationToken)
        {
            return await RunJobAsync(jobName, null, null, null, cancellationToken);
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
        public async Task<string> RunJobAsync(string jobName, JenkinsBuildParameters parameters, JenkinsRunConfig runConfig, IProgress<JenkinsRunProgress> progress, CancellationToken cancellationToken)
        {
            // IProgress<T> x, 

            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            runConfig = runConfig ?? this.RunConfig ?? throw new Exception("No JenkinsRunConfig available.");
            
            string path = $"/job/{jobName}/{(parameters == null ? "build" : "buildWithParameters")}?delay={runConfig.StartDelay}sec";
            Uri location = await PostRunAsync(path, null, cancellationToken);
            
            if (runConfig.RunMode == JenkinsRunMode.Immediately)
            {
                return location?.ToString();
            }

            // store last progress info to compare for changes
            JenkinsRunProgress last = null;

            // example: http://tiny:8080/queue/item/10/
            if (location != null)
            {
                string buildUrl = null;

                while (!cancellationToken.IsCancellationRequested)
                {
                    string str = await GetStringAsync(new Uri(location, "api/xml").ToString(), cancellationToken);
                    if (str.StartsWith("<buildableItem"))
                    {
                        JenkinsModelQueueBuildableItem item = XmlDeserialize<JenkinsModelQueueBuildableItem>(str);
                        UpdateProgress(ref last, progress, jobName, item);
                    }
                    else if (str.StartsWith("<leftItem"))
                    {
                        JenkinsModelQueueLeftItem item = XmlDeserialize<JenkinsModelQueueLeftItem>(str);
                        UpdateProgress(ref last, progress, jobName, item);

                        if (item.Executable != null)
                        {
                            buildUrl = item.Executable.Url;
                            break;
                        }
                    }
                    else
                    {
                        throw new Exception("Unknown XML Schema!!!");
                    }
                    await Task.Delay(runConfig.PollingTime);
                }

                if (runConfig.RunMode <= JenkinsRunMode.Queued)
                {
                    return buildUrl;
                }                

                while (!cancellationToken.IsCancellationRequested)
                {
                    string str = await GetStringAsync(new Uri(new Uri(buildUrl), "api/xml").ToString(), cancellationToken);
                    JenkinsModelRun run = Deserialize<JenkinsModelRun>(str, buildTypes);

                    UpdateProgress(ref last, progress, jobName, run);

                    Console.WriteLine($"IsBuilding: {run.IsBuilding}");
                    if (runConfig.RunMode <= JenkinsRunMode.Started && run.IsBuilding)
                    {
                        // build started
                        return buildUrl;
                    }
                    if (runConfig.RunMode <= JenkinsRunMode.Started && !run.IsBuilding)
                    {
                        // build finished
                        return buildUrl;
                    }
                    await Task.Delay(runConfig.PollingTime);
                }
            }

            return null;
        }

        private void UpdateProgress(ref JenkinsRunProgress last, IProgress<JenkinsRunProgress> progress, string jobName, object item)
        {
            if (this.RunProgress == null & progress == null)
            {
                // no progress handler
                return; 
            }

            string jobUrl = new Uri(this.client.BaseAddress, $"/job/{jobName}").ToString();
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
        /// Create a new job from an XML file.
        /// </summary>
        /// <param name="jobName">Name of the job</param>
        /// <param name="stream">XML data of the new job.</param>
        /// <param name="fileName">File name of the data</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
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
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task CreateJobAsync(string jobName, Stream stream, string fileName, CancellationToken cancellationToken)
        {
            MultipartFormDataContent content = new MultipartFormDataContent
            {
                { new StringContent(jobName), "name" },
                { new StreamContent(stream), "file0", fileName }
            };
            await PostRunAsync("createItem", content, cancellationToken);
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
            await PostAsync($"job/{jobName}/disable", cancellationToken);
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
            string str = await GetStringAsync($"job/{jobName}/description", cancellationToken);
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

