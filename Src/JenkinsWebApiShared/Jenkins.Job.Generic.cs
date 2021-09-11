using System;
using System.Collections.Generic;
using System.Text;
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
        public async Task<T> RunJobAsync<T>(string jobName)
        {
            return await RunJobAsync<T>(jobName, null, CancellationToken.None);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="parameters">Parameters for the Jenkins job</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<T> RunJobAsync<T>(string jobName, JenkinsBuildParameters parameters)
        {
            return await RunJobAsync<T>(jobName, parameters, CancellationToken.None);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<T> RunJobAsync<T>(string jobName, CancellationToken cancellationToken)
        {
            return await RunJobAsync<T>(jobName, null, cancellationToken);
        }

        /// <summary>
        /// Run a Jenkins job.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="parameters">Parameters for the Jenkins job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Result and number of the Jenkins build</returns>
        public async Task<T> RunJobAsync<T>(string jobName, JenkinsBuildParameters parameters, CancellationToken cancellationToken)
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

            return default(T);
        }
    }
}
