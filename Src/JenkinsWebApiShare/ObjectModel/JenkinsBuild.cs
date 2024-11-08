using JenkinsWebApi.Internal;
using JenkinsWebApi.Model;
using System;
using System.Net.Http;

namespace JenkinsWebApi.ObjectModel
{
    /// <summary>
    /// Jenkins build class
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

        /// <summary>
        /// Jenkins job where the build comes from.
        /// </summary>
        public JenkinsJob Job { get { return this.job; } }

        /// <summary>
        /// Name of the job where the build comes from.
        /// </summary>
        public string JobName { get { return this.job.Name; } }

        /// <summary>
        /// Signal if the Jenkins build is running.
        /// </summary>
        public bool IsBuilding { get { CheckUpdate(); return this.modelRun.IsBuilding; } }
        
        /// <summary>
        /// Description of the Jenkins build.
        /// </summary>
        public string Description { get { CheckUpdate(); return this.modelRun.Description; } }
        
        /// <summary>
        /// Display name of the Jenkins build.
        /// </summary>
        public string DisplayName { get { CheckUpdate(); return this.modelRun.DisplayName; } }

        /// <summary>
        /// Duration of the Jenkins build.
        /// </summary>
        public TimeSpan? Duration { get { CheckUpdate(); return XmlConverter.ToTimeSpan(this.modelRun.Duration); } }
        
        /// <summary>
        /// Estimated time of the Jenkins build.
        /// </summary>
        public TimeSpan? EstimatedDuration { get { CheckUpdate(); return XmlConverter.ToTimeSpan(this.modelRun.EstimatedDuration); } }

        /// <summary>
        /// Full display name of the Jenkins build.
        /// </summary>
        public string FullDisplayName { get { CheckUpdate(); return this.modelRun.FullDisplayName; } }

        /// <summary>
        /// Id of the Jenkins build.
        /// </summary>
        public string Id { get { CheckUpdate(); return this.modelRun.Id; } }

        /// <summary>
        /// Signals if the Jenkins build keep a log.
        /// </summary>
        public bool IsKeepLog { get { CheckUpdate(); return this.modelRun.IsKeepLog; } }

        /// <summary>
        /// Number of the jenkins build.
        /// </summary>
        public int Number { get { return this.modelRun.Number; } }

        /// <summary>
        /// Queue ID id the Jenkins build.
        /// </summary>
        public long QueueId { get { CheckUpdate(); return this.modelRun.QueueId; } }

        /// <summary>
        /// Result of the Jenkins build.
        /// </summary>
        public JenkinsResult Result { get { CheckUpdate(); return this.modelRun.Result; } }

        /// <summary>
        /// Time stamp of the Jenkins build. 
        /// </summary>
        public DateTime? Timestamp { get { CheckUpdate(); return XmlConverter.ToDateTime(this.modelRun.Timestamp); } }

        /// <summary>
        /// URL of the Jenkins build.
        /// </summary>
        public Uri Url { get { return new Uri(this.modelRun.Url); } }

        /// <summary>
        /// Console output of the Jenkins build.
        /// </summary>
        public string ConsoleOutput { get { return JenkinsRun.Run(() => jenkins.GetBuildConsoleOutputAsync(this.job.Name, this.modelRun.Number, 0).Result); } }

        /// <summary>
        /// Refresh the properties of the Jenkins build.
        /// </summary>
        public void Update()
        {
            this.modelRun = JenkinsRun.Run(() => jenkins.GetBuildAsync<JenkinsModelRun>(this.JobName, this.modelRun.Number).Result);
        }

        /// <summary>
        /// Delete Jenkins build.
        /// </summary>
        public void Delete()
        {
            JenkinsRun.Run(() => jenkins.DeleteBuildAsync(this.job.Name, this.modelRun.Number).Wait());
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
