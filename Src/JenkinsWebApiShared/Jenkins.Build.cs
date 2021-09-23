using JenkinsWebApi.Internal;
using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsWebApi
{
    public sealed partial class Jenkins
    {

        /// <summary>
        /// Get the Jenkins build data.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <returns>Jenkins build data</returns>
        public async Task<JenkinsModelRun> GetBuildAsync(string jobName, int buildNum)
        {
            return await GetBuildAsync(jobName, buildNum, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins build data.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins build data</returns>
        public async Task<JenkinsModelRun> GetBuildAsync(string jobName, int buildNum, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentException(nameof(jobName));
            }

            string str = await GetApiStringAsync($"job/{jobName}/{buildNum}", cancellationToken);
            JenkinsModelRun build = JenkinsDeserializer.DeserializeBuild<JenkinsModelRun>(str);
            return build;
        }

        /// <summary>
        /// Get the Jenkins build data.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <returns>Jenkins build data</returns>
        public async Task<T> GetBuildAsync<T>(string jobName, int buildNum) where T : JenkinsModelRun
        {
            return await GetBuildAsync<T>(jobName, buildNum, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins build data.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins build data</returns>
        public async Task<T> GetBuildAsync<T>(string jobName, int buildNum, CancellationToken cancellationToken) where T : JenkinsModelRun
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentException(nameof(jobName));
            }

            string str = await GetApiStringAsync($"job/{jobName}/{buildNum}", cancellationToken);
            T build = JenkinsDeserializer.DeserializeBuild<T>(str);
            return build;
        }

        /// <summary>
        /// Get the last build
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <returns>Jenkins build data</returns>
        public async Task<JenkinsModelRun> GetLastBuildAsync(string jobName)
        {
            return await GetLastBuildAsync(jobName, CancellationToken.None);
        }

        /// <summary>
        /// Get the last build
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins build data</returns>
        public async Task<JenkinsModelRun> GetLastBuildAsync(string jobName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentException(nameof(jobName));
            }

            string str = await GetApiStringAsync($"job/{jobName}/lastBuild", cancellationToken);
            JenkinsModelRun build = JenkinsDeserializer.DeserializeBuild<JenkinsModelRun>(str);
            return build;
        }

        /// <summary>
        /// Get the last build
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <returns>Jenkins build data</returns>
        public async Task<T> GetLastBuildAsync<T>(string jobName) where T : JenkinsModelRun
        {
            return await GetLastBuildAsync<T>(jobName, CancellationToken.None);
        }

        /// <summary>
        /// Get the last build
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins build data</returns>
        public async Task<T> GetLastBuildAsync<T>(string jobName, CancellationToken cancellationToken) where T : JenkinsModelRun
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentException(nameof(jobName));
            }

            string str = await GetApiStringAsync($"job/{jobName}/lastBuild", cancellationToken);
            T build = JenkinsDeserializer.DeserializeBuild<T>(str);
            return build;
        }

        /// <summary>
        /// Delete selcted build.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job.</param>
        /// <param name="buildNum">Number of the Jenkins build.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteBuildAsync(string jobName, int buildNum)
        {
            await DeleteBuildAsync(jobName, buildNum, CancellationToken.None);
        }

        /// <summary>
        /// Delete selcted build.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job.</param>
        /// <param name="buildNum">Number of the Jenkins build.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task DeleteBuildAsync(string jobName, int buildNum, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentException(nameof(jobName));
            }
            if (buildNum < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(buildNum));
            }

            // ignore Forbidden, Jenkins returns forbidden because of link to get without crumb
            await PostAsync($"job/{jobName}/{buildNum}/doDelete", HttpStatusCode.Forbidden, cancellationToken);
        }

        /// <summary>
        /// Get the Console Output text of a build.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job.</param>
        /// <param name="buildNum">Number of the Jenkins build.</param>
        /// <param name="start">Line number to start with. Use 0 to start from begin.</param>
        /// <returns>Console Output text.</returns>
        public async Task<string> GetBuildConsoleOutputAsync(string jobName, int buildNum, int start = 0)
        {
            return await GetBuildConsoleOutputAsync(jobName, buildNum, start, CancellationToken.None);
        }

        /// <summary>
        /// Get the Console Output text of a build.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job.</param>
        /// <param name="buildNum">Number of the Jenkins build.</param>
        /// <param name="start">Line number to start with. Use 0 to start from begin.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Console Output text.</returns>
        public async Task<string> GetBuildConsoleOutputAsync(string jobName, int buildNum, int start, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentException(nameof(jobName));
            }
            if (buildNum < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(buildNum));
            }

            string str = await GetStringAsync($"/job/{jobName}/{buildNum}/logText/progressiveText?start={start}", cancellationToken);
            return str;
        }

        /// <summary>
        /// Set name and description for a build
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job.</param>
        /// <param name="buildNum">Number of the Jenkins build.</param>
        /// <param name="displayName">Build display name to set.</param>
        /// <param name="description">Build description to set.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task SetBuildInformation(string jobName, int buildNum, string displayName, string description)
        {
            await SetBuildInformation(jobName, buildNum, displayName, description, CancellationToken.None);
        }

        /// <summary>
        /// Set name and description for a build
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job.</param>
        /// <param name="buildNum">Number of the Jenkins build.</param>
        /// <param name="displayName">Build display name to set.</param>
        /// <param name="description">Build description to set.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task SetBuildInformation(string jobName, int buildNum, string displayName, string description, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentException(nameof(jobName));
            }

            if (buildNum < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(buildNum));
            }

            var content = new Dictionary<string, string>();
            content.Add("displayName", displayName);
            content.Add("description", description);
            content.Add("core:apply", "true");
            content.Add("json", content.ToJson());  // without json bad request
            await PostAsync($"/job/{jobName}/{buildNum}/configSubmit", content, cancellationToken);
        }
    }
}
