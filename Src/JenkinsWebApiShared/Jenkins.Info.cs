using JenkinsWebApi.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsWebApi
{
    public sealed partial class Jenkins
    {
        /// <summary>
        /// Get the Jenkins server configuration.
        /// </summary>
        /// <returns>Jenkins server configuration</returns>
        public async Task<JenkinsModelHudson> GetServerAsync()
        {
            return await GetServerAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins server configuration.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins server configuration</returns>
        public async Task<JenkinsModelHudson> GetServerAsync(CancellationToken cancellationToken)
        {
            JenkinsModelHudson server = await GetAsync<JenkinsModelHudson>("api/xml", cancellationToken);
            return server;
        }

        /// <summary>
        /// Get the Jenkins server credentials.
        /// </summary>
        /// <returns>Jenkins server credentials</returns>
        /// <remark>Only in V2 or above</remark>
        public async Task<JenkinsCloudbeesViewCredentialsActionRootActionImpl> GetCredentialsAsync()
        {
            return await GetCredentialsAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins server credentials.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins server credentials</returns>
        /// <remark>Only in V2 or above</remark>
        public async Task<JenkinsCloudbeesViewCredentialsActionRootActionImpl> GetCredentialsAsync(CancellationToken cancellationToken)
        {
            JenkinsCloudbeesViewCredentialsActionRootActionImpl credentials = await GetAsync<JenkinsCloudbeesViewCredentialsActionRootActionImpl>("credentials/api/xml", cancellationToken);
            return credentials;
        }

        /// <summary>
        /// Get the Jenkins build test report.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <returns>Jenkins build test report if available; null if not</returns>
        public async Task<JenkinsTasksJunitTestResult> GetTestReportAsync(string jobName, int buildNum)
        {
            return await GetTestReportAsync(jobName, buildNum, CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins build test report.
        /// </summary>
        /// <param name="jobName">Name of the Jenkins job</param>
        /// <param name="buildNum">Number of the Jenkins build</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins build test report if available; null if not</returns>
        public async Task<JenkinsTasksJunitTestResult> GetTestReportAsync(string jobName, int buildNum, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                throw new ArgumentNullException(nameof(jobName));
            }

            JenkinsTasksJunitTestResult report = await GetAsync<JenkinsTasksJunitTestResult>($"/job/{jobName}/{buildNum}/testReport/api/xml", cancellationToken);
            return report;
        }

        /// <summary>
        /// Get the Jenkins queue.
        /// </summary>
        /// <returns>Jenkins queue</returns>
        public async Task<JenkinsModelQueue> GetQueueAsync()
        {
            return await GetQueueAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get the Jenkins queue.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Jenkins queue</returns>
        public async Task<JenkinsModelQueue> GetQueueAsync(CancellationToken cancellationToken)
        {
            JenkinsModelQueue queue = await GetAsync<JenkinsModelQueue>("queue/api/xml", cancellationToken);
            return queue;
        }

        /// <summary>
        /// Get overall load statistics
        /// </summary>
        /// <returns>Statistics result</returns>
        public async Task<JenkinsModelOverallLoadStatistics> GetOverallLoadStatisticsAsync()
        {
            return await GetOverallLoadStatisticsAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get overall load statistics
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Statistics result</returns>
        public async Task<JenkinsModelOverallLoadStatistics> GetOverallLoadStatisticsAsync(CancellationToken cancellationToken)
        {
            JenkinsModelOverallLoadStatistics statistics = await GetAsync<JenkinsModelOverallLoadStatistics>("overallLoad/api/xml", cancellationToken);
            return statistics;
        }

        /// <summary>
        /// Get infos about all Jenkins nodes.
        /// </summary>
        /// <returns>Nodes infos</returns>
        public async Task<JenkinsModelComputerSet> GetComputerSetAsync()
        {
            return await GetComputerSetAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get infos about all Jenkins nodes.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Nodes infos</returns>
        public async Task<JenkinsModelComputerSet> GetComputerSetAsync(CancellationToken cancellationToken)
        {
            JenkinsModelComputerSet computerSet = await GetAsync<JenkinsModelComputerSet>("computer/api/xml", cancellationToken);
            return computerSet;
        }

        /// <summary>
        /// Get infos about the Jenkins master node
        /// </summary>
        /// <returns>Master node infos</returns>
        public async Task<JenkinsModelHudsonMasterComputer> GetMasterComputerAsync()
        {
            return await GetMasterComputerAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get infos about the Jenkins master node
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Master node infos</returns>
        public async Task<JenkinsModelHudsonMasterComputer> GetMasterComputerAsync(CancellationToken cancellationToken)
        {
            JenkinsModelHudsonMasterComputer computer = await GetAsync<JenkinsModelHudsonMasterComputer>("computer/(master)/api/xml", cancellationToken);
            return computer;
        }

        /// <summary>
        /// Get infos of a Jenkins slave node.
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>Node infos</returns>
        public async Task<JenkinsSlavesSlaveComputer> GetComputerAsync(string computerName)
        {
            return await GetComputerAsync(computerName, CancellationToken.None);
        }

        /// <summary>
        /// Get infos of a Jenkins slave node.
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Node infos</returns>
        public async Task<JenkinsSlavesSlaveComputer> GetComputerAsync(string computerName, CancellationToken cancellationToken)
        {
            JenkinsSlavesSlaveComputer computer = await GetAsync<JenkinsSlavesSlaveComputer>($"computer/{computerName}/api/xml", cancellationToken);
            return computer;
        }

        ///// <summary>
        ///// Get extended infos of a Jenkins slave node.
        ///// </summary>
        ///// <param name="computerName">Name of the node.</param>
        ///// <returns>Node infos</returns>
        //public async Task<JenkinsComputerExt> GetComputerExtAsync(string computerName)
        //{
        //    return await GetComputerExtAsync(computerName, CancellationToken.None);
        //}

        ///// <summary>
        ///// Get extended infos of a Jenkins slave node.
        ///// </summary>
        ///// <param name="computerName">Name of the node.</param>
        ///// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        ///// <returns>Node infos</returns>
        //public async Task<JenkinsComputerExt> GetComputerExtAsync(string computerName, CancellationToken cancellationToken)
        //{
        //    string str = await GetStringAsync($"computer/{computerName}/configure", cancellationToken);
        //    JenkinsComputerExt computerExt = new JenkinsComputerExt
        //    {
        //        Description = TrimDescription(str),
        //        Label = TrimLabel(str)
        //    };
        //    return computerExt;
        //}

        /// <summary>
        /// Get the log of the computer
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <returns>Log text</returns>
        public async Task<string> GetComputerLogAsync(string computerName)
        {
            return await GetComputerLogAsync(computerName, CancellationToken.None);
        }

        /// <summary>
        /// Get the log of the computer
        /// </summary>
        /// <param name="computerName">Name of the node.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Log text</returns>
        public async Task<string> GetComputerLogAsync(string computerName, CancellationToken cancellationToken)
        {
            return await GetStringAsync($"computer/{computerName}/logText/progressiveText", cancellationToken);
        }

        /// <summary>
        /// Get infos of a Jenkins slave node label.
        /// </summary>
        /// <param name="labelName">Name of the label</param>
        /// <returns>Label info</returns>
        public async Task<JenkinsModelLabelsLabelAtom> GetLabelAsync(string labelName)
        {
            return await GetLabelAsync(labelName, CancellationToken.None);
        }

        /// <summary>
        /// Get infos of a Jenkins slave node label.
        /// </summary>
        /// <param name="labelName">Name of the label</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Label info</returns>
        public async Task<JenkinsModelLabelsLabelAtom> GetLabelAsync(string labelName, CancellationToken cancellationToken)
        {
            JenkinsModelLabelsLabelAtom label = await GetAsync<JenkinsModelLabelsLabelAtom>($"label/{labelName}/api/xml", cancellationToken);
            return label;
        }

        
    }
}
