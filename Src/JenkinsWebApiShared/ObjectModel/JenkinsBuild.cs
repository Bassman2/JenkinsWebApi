using JenkinsWebApi.Internal;
using JenkinsWebApi.Model;
using System;

namespace JenkinsWebApi.ObjectModel
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class JenkinsBuild
    {
        private readonly Jenkins jenkins;
        private readonly JenkinsJob job;
        private JenkinsModelRun modelRun;

        internal JenkinsBuild(Jenkins jenkins, JenkinsJob job, JenkinsModelRun modelRun)
        {
            this.jenkins = jenkins;
            this.job = job;
            this.modelRun = modelRun;
        }

        internal JenkinsBuild(Jenkins jenkins, JenkinsJob job, int buildNum)
        {
            this.jenkins = jenkins;
            this.job = job;
            this.modelRun = jenkins.GetBuildAsync<JenkinsModelRun>(this.JobName, buildNum).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        public Jenkins Jenkins { get { return this.jenkins; } }

        /// <summary>
        /// 
        /// </summary>
        public JenkinsJob Job { get { return this.job; } }

        /// <summary>
        /// 
        /// </summary>
        public string JobName { get { return this.job.Name; } }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBuilding { get { return this.modelRun.IsBuilding; } }
        
        /// <summary>
        /// 
        /// </summary>
        public string Description { get { return this.modelRun.Description; } }
        
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get { return this.modelRun.DisplayName; } }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan? Duration { get { return XmlConverter.ToTimeSpan(this.modelRun.Duration); } }
        
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan? EstimatedDuration { get { return XmlConverter.ToTimeSpan(this.modelRun.EstimatedDuration); } }

        /// <summary>
        /// 
        /// </summary>
        public string FullDisplayName { get { return this.modelRun.FullDisplayName; } }

        /// <summary>
        /// 
        /// </summary>
        public string Id { get { return this.modelRun.Id; } }

        /// <summary>
        /// 
        /// </summary>
        public bool IsKeepLog { get { return this.modelRun.IsKeepLog; } }

        /// <summary>
        /// 
        /// </summary>
        public int Number { get { return this.modelRun.Number; } }

        /// <summary>
        /// 
        /// </summary>
        public long QueueId { get { return this.modelRun.QueueId; } }

        /// <summary>
        /// 
        /// </summary>
        public JenkinsResult Result { get { return this.modelRun.Result; } }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Timestamp { get { return XmlConverter.ToDateTime(this.modelRun.Timestamp); } }

        /// <summary>
        /// 
        /// </summary>
        public Uri Url { get { return new Uri(this.modelRun.Url); } }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            this.modelRun = jenkins.GetBuildAsync<JenkinsModelRun>(this.JobName, this.modelRun.Number).Result;
        }
    }
}
