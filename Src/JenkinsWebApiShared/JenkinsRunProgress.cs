using JenkinsWebApi.Model;
using System;
using System.Net;

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
            if (item is Jenkins.PostRunRes res)
            {
                this.Status = res.StatusCode == HttpStatusCode.Conflict ? JenkinsRunStatus.Disabled : JenkinsRunStatus.Queued;
                this.QueueUrl = (item as Uri)?.ToString();
            }
            else if (item is JenkinsModelQueueBuildableItem buildableItem)
            {
                this.Status = buildableItem.IsStuck ? JenkinsRunStatus.Stuck : JenkinsRunStatus.Queued;
                this.ProblemDescription = buildableItem.Why;
            }
            else if(item is JenkinsModelQueueBlockedItem blockedItem)
            {
                this.Status = blockedItem.IsBlocked ? JenkinsRunStatus.Blocked : JenkinsRunStatus.Queued;
                this.ProblemDescription = blockedItem.Why;

            }
            else if (item is JenkinsModelQueueLeftItem queueItem)
            {
                this.Status = JenkinsRunStatus.Queued;
                this.ProblemDescription = queueItem.Why;
                this.Result = null;
                this.QueueId = queueItem.Id;
                this.QueueUrl = queueItem.Url;
                this.BuildNum = queueItem.Executable?.Number ?? 0;
                this.BuildUrl = queueItem.Executable?.Url ?? null;
            }
            else if (item is JenkinsModelRun run)
            {
                this.Status = run.IsBuilding ? JenkinsRunStatus.Building : JenkinsRunStatus.Finished;
                this.Result = run.IsBuilding ? null : (JenkinsResult?)run.Result;
                this.QueueId = run.QueueId;
                //this.QueueUrl =
                this.BuildNum = run.Number;
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
        /// Problem description
        /// </summary>
        public string ProblemDescription { get; }

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
        /// Id of the queue
        /// </summary>
        public long QueueId { get; }

        /// <summary>
        /// Url of the queue
        /// </summary>
        public string QueueUrl { get; }

        /// <summary>
        /// Number of the build
        /// </summary>
        public int BuildNum { get; }

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
