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
        private bool isCompleteLoaded;

        internal JenkinsBuild(Jenkins jenkins, JenkinsJob job, JenkinsModelRun modelRun)
        {
            this.jenkins = jenkins;
            this.job = job;
            this.modelRun = modelRun;
            this.isCompleteLoaded = false;
        }

        internal JenkinsBuild(Jenkins jenkins, JenkinsJob job, int buildNum)
        {
            this.jenkins = jenkins;
            this.job = job;
            this.modelRun = JenkinsRun.Run(() => jenkins.GetBuildAsync<JenkinsModelRun>(this.JobName, buildNum).Result);
            this.isCompleteLoaded = true;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Jenkins Jenkins { get { return this.jenkins; } }

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
        public bool IsBuilding { get { CheckUpdate(); return this.modelRun.IsBuilding; } }
        
        /// <summary>
        /// 
        /// </summary>
        public string Description { get { CheckUpdate(); return this.modelRun.Description; } }
        
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get { CheckUpdate(); return this.modelRun.DisplayName; } }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan? Duration { get { CheckUpdate(); return XmlConverter.ToTimeSpan(this.modelRun.Duration); } }
        
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan? EstimatedDuration { get { CheckUpdate(); return XmlConverter.ToTimeSpan(this.modelRun.EstimatedDuration); } }

        /// <summary>
        /// 
        /// </summary>
        public string FullDisplayName { get { CheckUpdate(); return this.modelRun.FullDisplayName; } }

        /// <summary>
        /// 
        /// </summary>
        public string Id { get { CheckUpdate(); return this.modelRun.Id; } }

        /// <summary>
        /// 
        /// </summary>
        public bool IsKeepLog { get { CheckUpdate(); return this.modelRun.IsKeepLog; } }

        /// <summary>
        /// 
        /// </summary>
        public int Number { get { return this.modelRun.Number; } }

        /// <summary>
        /// 
        /// </summary>
        public long QueueId { get { CheckUpdate(); return this.modelRun.QueueId; } }

        /// <summary>
        /// 
        /// </summary>
        public JenkinsResult Result { get { CheckUpdate(); return this.modelRun.Result; } }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Timestamp { get { CheckUpdate(); return XmlConverter.ToDateTime(this.modelRun.Timestamp); } }

        /// <summary>
        /// 
        /// </summary>
        public Uri Url { get { return new Uri(this.modelRun.Url); } }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            this.modelRun = JenkinsRun.Run(() => jenkins.GetBuildAsync<JenkinsModelRun>(this.JobName, this.modelRun.Number).Result);
        }

        private void CheckUpdate()
        {
            if (!isCompleteLoaded)
            {
                Update();
            }
        }
    }
}
