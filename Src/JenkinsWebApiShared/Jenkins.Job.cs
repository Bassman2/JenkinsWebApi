using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
        /// <param name="jobName">Name of the job</param>
        /// <returns>Jenkins job data</returns>
        public async Task<T> GetJobAsync<T>(string jobName)
        {
            return await GetJobAsync<T>(jobName, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins job data.
        /// </summary>
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
        public async Task<JenkinsModelQueueLeftItem> RunJobAsync(string jobName)
        {
            return await RunJobAsync(jobName, null, CancellationToken.None);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="parameters">Parameters for the Jenkins job</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<JenkinsModelQueueLeftItem> RunJobAsync(string jobName, JenkinsBuildParameters parameters)
        {
            return await RunJobAsync(jobName, parameters, CancellationToken.None);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<JenkinsModelQueueLeftItem> RunJobAsync(string jobName, CancellationToken cancellationToken)
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
        public async Task<JenkinsModelQueueLeftItem> RunJobAsync(string jobName, JenkinsBuildParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            //Uri location;
            //if (parameters == null || parameters.IsEmpty)
            //{
            //    location = await PostRunAsync($"/job/{jobName}/build?delay=0sec", null, cancellationToken);
            //}
            //else
            //{
            //    location = await PostRunAsync($"/job/{jobName}/build?delay=0sec", parameters, cancellationToken);
            //}


            Uri location = await PostRunAsync($"/job/{jobName}/build?delay=0sec", parameters, cancellationToken);

            // example: http://tiny:8080/queue/item/10/
            if (location != null)
            {
                // if no delay item.Executable will be null
                await Task.Delay(100);
                return await GetAsync<JenkinsModelQueueLeftItem>(new Uri(location, "api/xml").ToString(), CancellationToken.None);
            }

            return null;
        }

        /// <summary>
        /// Stop a running Jenkins build
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
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
        /// <returns>Task handle</returns>        
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
        /// <returns>Task handle</returns>        
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
        /// Clone a new job from an existing.
        /// </summary>
        /// <param name="fromJobName">Existing job name.</param>
        /// <param name="newJobName">Name of the new job.</param>
        /// <returns>Task handle</returns>
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
        /// <returns>Task handle</returns>
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
    }
}
