using JenkinsWebApi.Internal;
using JenkinsWebApi.Model;
using System;

namespace JenkinsWebApi
{
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

        public Jenkins Jenkins { get { return this.jenkins; } }
        public JenkinsJob Job { get { return this.job; } }

        public string JobName { get { return this.job.Name; } }

        public bool IsBuilding { get { return this.modelRun.IsBuilding; } }
        public string Description { get { return this.modelRun.Description; } }
        public string DisplayName { get { return this.modelRun.DisplayName; } }
        public TimeSpan? Duration { get { return XmlConverter.ToTimeSpan(this.modelRun.Duration); } }
        public TimeSpan? EstimatedDuration { get { return XmlConverter.ToTimeSpan(this.modelRun.EstimatedDuration); } }
        public string FullDisplayName { get { return this.modelRun.FullDisplayName; } }
        public string Id { get { return this.modelRun.Id; } }
        public bool IsKeepLog { get { return this.modelRun.IsKeepLog; } }
        public int Number { get { return this.modelRun.Number; } }
        public long QueueId { get { return this.modelRun.QueueId; } }
        public JenkinsResult Result { get { return this.modelRun.Result; } }
        public DateTime? Timestamp { get { return XmlConverter.ToDateTime(this.modelRun.Timestamp); } }
        public Uri Url { get { return new Uri(this.modelRun.Url); } }

        public void Update()
        {
            this.modelRun = jenkins.GetBuildAsync<JenkinsModelRun>(this.JobName, this.modelRun.Number).Result;
        }
    }
}
