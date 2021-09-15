using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JenkinsWebApi
{
    /// <summary>
    /// JobRunAsync progress information class
    /// </summary>
    public class JenkinsRunProgress : EventArgs, IEquatable<JenkinsRunProgress>
    {
        internal JenkinsRunProgress(string jobName, string jobUrl, object item)
        {
            this.JobName = jobName;
            this.JobUrl = jobUrl;

            if (item is JenkinsModelQueueBuildableItem)
            {
                JenkinsModelQueueBuildableItem buildableItem = item as JenkinsModelQueueBuildableItem;
                this.Status = JenkinsRunStatus.Queued;
                this.Result = null;
                this.BuildUrl = null;
            }
            else if (item is JenkinsModelQueueLeftItem)
            {
                JenkinsModelQueueLeftItem queueItem = item as JenkinsModelQueueLeftItem;
                this.Status = JenkinsRunStatus.Queued;
                this.Result = null;
                this.BuildUrl = queueItem.Executable?.Url ?? null;
            }
            else if (item is JenkinsModelRun)
            {
                JenkinsModelRun run = item as JenkinsModelRun;
                this.Status = run.IsBuilding ? JenkinsRunStatus.Building : JenkinsRunStatus.Finished;
                this.Result = run.IsBuilding ? null : (JenkinsResult?)run.Result;
                this.BuildUrl = run.Url;
            }
            else 
            {
                throw new Exception("Unknown class");
            }
        }

        /// <summary>
        /// Status of the run
        /// </summary>
        public JenkinsRunStatus Status { get; }

        /// <summary>
        /// Result of the run
        /// </summary>
        public JenkinsResult? Result { get; }

        /// <summary>
        /// Name of the Jenkins job which is running.
        /// </summary>
        public string JobName { get; }

        /// <summary>
        /// Url of the Jenkins job which is running.
        /// </summary>
        public string JobUrl { get; }


        /// <summary>
        /// Url of the build
        /// </summary>
        public string BuildUrl { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(JenkinsRunProgress other)
        {
            return
                this.Status == other.Status &&
                this.Result == other.Result &&
                this.JobName == other.JobName &&
                this.JobUrl == other.JobUrl &&
                this.BuildUrl == other.BuildUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals((JenkinsRunProgress)obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
