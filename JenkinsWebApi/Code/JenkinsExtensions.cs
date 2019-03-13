using JenkinsWebApi.Model;
using System.Threading;
using System.Threading.Tasks;

namespace JenkinsWebApi
{
    /// <summary>
    /// Extensions for the Jenkins class.
    /// </summary>
    public static class JenkinsExtensions
    {
        /// <summary>
        /// Run Jenkins job and return after job has finished.
        /// </summary>
        /// <param name="jenkins">Jenkins this parameter</param>
        /// <param name="name">Name of the Jenkins job.</param>
        /// <param name="parameters">Parameter list for the Jenkins job.</param>
        /// <param name="pollingTime">Polling time in milliseconds. The default value is 5 seconds.</param>
        /// <returns>Finished build</returns>
        public static JenkinsRun RunJobComplete(this Jenkins jenkins, string name, JenkinsBuildParameters parameters = null, int pollingTime = 5000)
        {
            //    int num = -1;
            //    JenkinsRun build = null;
            //    JenkinsQueueLeftItem item = jenkins.RunJobAsync(name, parameters).Result;
            //    if (item != null)
            //    {
            //        num = item.Executable.Number;  // not item.ID
            //    }
            //    else
            //    {
            //        Thread.Sleep(pollingTime);
            //        build = jenkins.GetLastBuildAsync(name).Result;
            //        num = build.Number;
            //    }
            //    do
            //    {
            //        Thread.Sleep(pollingTime);
            //        build = jenkins.GetBuildAsync(name, num).Result as JenkinsBuild;
            //    } while (build.IsBuilding);

            //    return build;
            return null;
        }

        /// <summary>
        /// Run Jenkins job complete asynchron.
        /// </summary>
        /// <param name="jenkins">Jenkins this parameter</param>
        /// <param name="name">Name of the Jenkins job.</param>
        /// <param name="parameters">Parameter list for the Jenkins job.</param>
        /// <param name="pollingTime">Polling time in milliseconds. The default value is 5 seconds.</param>
        /// <returns>Finished build</returns>
        //public async static Task<JenkinsRun> RunJobCompleteAsync(this Jenkins jenkins, string name, JenkinsBuildParameters parameters = null, int pollingTime = 5000)
        //{
        //    int num = -1;
        //    JenkinsRun build = null;
        //    JenkinsQueueLeftItem item = await jenkins.RunJobAsync(name, parameters);
        //    if (item != null)
        //    {
        //        num = item.Executable.Number; // not item.ID
        //    }
        //    else
        //    {
        //        await Task.Delay(pollingTime);
        //        build = await jenkins.GetLastBuildAsync(name);
        //        num = build.Number;
        //    }
        //    do
        //    {
        //        await Task.Delay(pollingTime);
        //        build = await jenkins.GetBuildAsync(name, num) as JenkinsBuild;
        //    } while (build.IsBuilding);

        //    return build;
        //}
    }
}
