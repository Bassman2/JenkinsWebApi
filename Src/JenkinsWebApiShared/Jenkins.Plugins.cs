using JenkinsWebApi.Model;
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
        /// Get environment variable list
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <returns>Get environment variables</returns>
        /// <remarks>Plugin &quot;Environment Injector Plugin&quot; must be installed </remarks>
        public async Task<JenkinsJenkinsciEnvInjectVarList> GetEnvInjectVarListAsync(string jobName, int buildNum)
        {
            return await GetEnvInjectVarListAsync(jobName, buildNum, CancellationToken.None);
        }

        /// <summary>
        /// Get environment variable list
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Get environment variables</returns>
        /// <remarks>Plugin &quot;Environment Injector Plugin&quot; must be installed </remarks>
        public async Task<JenkinsJenkinsciEnvInjectVarList> GetEnvInjectVarListAsync(string jobName, int buildNum, CancellationToken cancellationToken)
        {
            JenkinsJenkinsciEnvInjectVarList label = await GetApiAsync<JenkinsJenkinsciEnvInjectVarList>($"job/{jobName}/{buildNum}/injectedEnvVars", cancellationToken);
            return label;
        }
        
        /// <summary>
        /// Get build graph
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <returns>Get graph information</returns>
        /// <remarks>Plugin &quot;buildgraph-view&quot; must be installed </remarks>
        public async Task<JenkinsJenkinsciBuildGraph> GetBuildGraph(string jobName, int buildNum)
        {
            return await GetBuildGraph(jobName, buildNum, CancellationToken.None);
        }

        /// <summary>
        /// Get build graph
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Get graph information</returns>
        /// <remarks>Plugin &quot;buildgraph-view&quot; must be installed </remarks>
        public async Task<JenkinsJenkinsciBuildGraph> GetBuildGraph(string jobName, int buildNum, CancellationToken cancellationToken)
        {
            JenkinsJenkinsciBuildGraph label = await GetApiAsync<JenkinsJenkinsciBuildGraph>($"job/{jobName}/{buildNum}/BuildGraph", cancellationToken);
            return label;
        }
    }
}
