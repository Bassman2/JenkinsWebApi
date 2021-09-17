using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace JenkinsWebApi
{
    public sealed class JenkinsJob : IProgress<JenkinsRunProgress>
    {
        private readonly Jenkins jenkins;
        private JenkinsModelJob modelJob;

        private CancellationTokenSource token = null;
        
        public event EventHandler<JenkinsRunProgress> RunProgress;

        internal JenkinsJob(Jenkins jenkins, string jobName)
        {
            this.jenkins = jenkins;
            this.modelJob = jenkins.GetJobAsync<JenkinsModelJob>(jobName).Result;
        }

        #region Properties

        // JenkinsModelAbstractItem

        public string Description { get { return this.modelJob.Description; } }
        public string DisplayName { get { return this.modelJob.DisplayName; } }
        public string DisplayNameOrNull { get { return this.modelJob.DisplayNameOrNull; } }
        public string FullDisplayName { get { return this.modelJob.FullDisplayName; } }
        public string FullName { get { return this.modelJob.FullName; } }
        public string Name { get { return this.modelJob.Name; } }
        public Uri Url { get { return new Uri(this.modelJob.Url); } }

        // JenkinsModelJob

        public bool IsBuildable { get { return this.modelJob.IsBuildable; } }
        public JenkinsBuild FirstBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.FirstBuild); } }
        public bool IsInQueue { get { return this.modelJob.IsInQueue; } }
        public bool IsKeepDependencies { get { return this.modelJob.IsKeepDependencies; } }

        public JenkinsBuild LastBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastBuild); } }

        public JenkinsBuild LastCompletedBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastCompletedBuild); } }

        public JenkinsBuild LastFailedBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastFailedBuild); } }

        public JenkinsBuild LastStableBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastStableBuild); } }

        public JenkinsBuild LastSuccessfulBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastSuccessfulBuild); } }

        public JenkinsBuild LastUnstableBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastUnstableBuild); } }

        public JenkinsBuild LastUnsuccessfulBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastUnsuccessfulBuild); } }

        public int NextBuildNumber { get { return this.modelJob.NextBuildNumber; } }


        public string Config
        {
            get
            {
                return jenkins.GetJobConfigAsync(this.Name).Result;
            }
            set
            {
                jenkins.SetJobConfigAsync(this.Name, value).Wait();
                Update();
            }
        }

        public XmlDocument ConfigXml
        {
            get
            {
                return jenkins.GetJobConfigXmlAsync(this.Name).Result;
            }
            set
            {
                jenkins.SetJobConfigXmlAsync(this.Name, value).Wait();
                Update();
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JenkinsBuild QueueBuild(JenkinsBuildParameters parameters = null)
        {
            this.token = new CancellationTokenSource();
            return Task.Run(() =>
            {
                JenkinsRunConfig runConfig = this.jenkins.RunConfig.Clone();
                runConfig.RunMode = JenkinsRunMode.Queued;
                JenkinsRunProgress res = this.jenkins.RunJobAsync(this.modelJob.Name, parameters, runConfig, this, this.token.Token).Result;
                return new JenkinsBuild(this.jenkins, this, res.BuildNum);
            }, this.token.Token).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JenkinsBuild StartBuild(JenkinsBuildParameters parameters = null)
        {
            this.token = new CancellationTokenSource();
            return Task.Run(() =>
            {
                JenkinsRunConfig runConfig = this.jenkins.RunConfig.Clone();
                runConfig.RunMode = JenkinsRunMode.Started;
                JenkinsRunProgress res = this.jenkins.RunJobAsync(this.modelJob.Name, parameters, runConfig, this, this.token.Token).Result;
                return new JenkinsBuild(this.jenkins, this, res.BuildNum);
            }, this.token.Token).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JenkinsBuild RunBuild(JenkinsBuildParameters parameters = null)
        {
            this.token = new CancellationTokenSource();
            return Task.Run(() =>
            {
                JenkinsRunConfig runConfig = this.jenkins.RunConfig.Clone();
                runConfig.RunMode = JenkinsRunMode.Finished;
                JenkinsRunProgress res = this.jenkins.RunJobAsync(this.modelJob.Name, parameters, runConfig, this, this.token.Token).Result;
                return new JenkinsBuild(this.jenkins, this, res.BuildNum);
            }, this.token.Token).Result;
        }

        public void StopBuild()
        {
            this.token?.Cancel();
        }

        void IProgress<JenkinsRunProgress>.Report(JenkinsRunProgress value)
        {
            RunProgress?.Invoke(this, value);
        }

        public void Update()
        {
            this.modelJob = jenkins.GetJobAsync<JenkinsModelJob>(this.Name).Result;
        }

        
    }
}
