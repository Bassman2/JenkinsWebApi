using JenkinsWebApi.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsWebApi
{
    public sealed partial class Jenkins
    {
        /// <summary>
        /// Run Jenkins job and return after job has finished.
        /// </summary>
        /// <param name="name">Name of the Jenkins job.</param>
        /// <param name="parameters">Parameter list for the Jenkins job.</param>
        /// <param name="pollingTime">Polling time in milliseconds. The default value is 5 seconds.</param>
        /// <returns>Finished build</returns>
        public JenkinsModelRun RunJobComplete(string name, JenkinsBuildParameters parameters = null, int pollingTime = 5000)
        {
            JenkinsModelQueueLeftItem item = RunJobAsync(name, parameters).Result;
            if (item == null)
            {
                return null;
            }

            string path = new Uri(new Uri(item.Executable.Url), "api/xml").ToString();
            JenkinsModelRun run = GetAsync<JenkinsModelRun>(path, CancellationToken.None).Result;
            while (run.IsBuilding)
            {
                Thread.Sleep(pollingTime);
                run = GetAsync<JenkinsModelRun>(path, CancellationToken.None).Result;
            } 

            return run;
        }

        /// <summary>
        /// Run Jenkins job complete asynchron.
        /// </summary>
        /// <param name="name">Name of the Jenkins job.</param>
        /// <param name="parameters">Parameter list for the Jenkins job.</param>
        /// <param name="pollingTime">Polling time in milliseconds. The default value is 5 seconds.</param>
        /// <returns>Finished build</returns>
        public async Task<JenkinsModelRun> RunJobCompleteAsync(string name, JenkinsBuildParameters parameters = null, int pollingTime = 5000)
        {
            JenkinsModelQueueLeftItem item = await RunJobAsync(name, parameters);
            if (item == null)
            {
                return null;
            }

            return await Task.Run<JenkinsModelRun>( () =>
            {
                string path = new Uri(new Uri(item.Executable.Url), "api/xml").ToString();
                JenkinsModelRun run = GetAsync<JenkinsModelRun>(path, CancellationToken.None).Result;
                while (run.IsBuilding)
                {
                    Thread.Sleep(pollingTime);
                    run = GetAsync<JenkinsModelRun>(path, CancellationToken.None).Result;
                }

                return run;
            });
        }
    }
}
