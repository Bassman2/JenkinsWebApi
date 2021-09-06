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
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<object> RunJobAsync(string jobName)
        {
            return await RunJobAsync(jobName, null, CancellationToken.None);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="parameters">Parameters for the Jenkins job</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<object> RunJobAsync(string jobName, JenkinsBuildParameters parameters)
        {
            return await RunJobAsync(jobName, parameters, CancellationToken.None);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<object> RunJobAsync(string jobName, CancellationToken cancellationToken)
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
        public async Task<object> RunJobAsync(string jobName, JenkinsBuildParameters parameters, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            Uri location;
            if (parameters == null || parameters.IsEmpty)
            {
                location = await PostRunAsync($"/job/{jobName}/build?delay=0sec", null, cancellationToken);
            }
            else
            {
                location = await PostRunAsync($"/job/{jobName}/build?delay=0sec", parameters, cancellationToken);
            }


            // Key	Value  Location http://localhost:8080/queue/item/9/
            // Key	Value  Location http://localhost:8080/queue/item/10/
            // Key Value  Location http://localhost:8080/job/Freestyle%20Test%20Parameter/

            if (location != null && location.ToString().Contains("/queue/item/"))
            {
                // if no delay item.Executable will be null
                await Task.Delay(100);
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                string schema = await GetStringAsync(new Uri(location, "api/Schema").ToString(), cancellationToken);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                //object item = await GetAsync<>(new Uri(location, "api/xml/").ToString());
                //return item;
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
