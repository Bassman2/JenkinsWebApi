using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace JenkinsWebApi
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class JenkinsJob : IProgress<JenkinsRunProgress>
    {
        private readonly Jenkins jenkins;
        private JenkinsModelJob modelJob;

        private CancellationTokenSource token = null;
        
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<JenkinsRunProgress> RunProgress;

        internal JenkinsJob(Jenkins jenkins, string jobName)
        {
            this.jenkins = jenkins;
            this.modelJob = jenkins.GetJobAsync<JenkinsModelJob>(jobName).Result;
        }

        #region Properties

        // JenkinsModelAbstractItem

        /// <summary>
        /// 
        /// </summary>
        public string Description { get { return this.modelJob.Description; } }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get { return this.modelJob.DisplayName; } }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayNameOrNull { get { return this.modelJob.DisplayNameOrNull; } }

        /// <summary>
        /// 
        /// </summary>
        public string FullDisplayName { get { return this.modelJob.FullDisplayName; } }

        /// <summary>
        /// 
        /// </summary>
        public string FullName { get { return this.modelJob.FullName; } }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get { return this.modelJob.Name; } }

        /// <summary>
        /// 
        /// </summary>
        public Uri Url { get { return new Uri(this.modelJob.Url); } }

        // JenkinsModelJob


        /// <summary>
        /// 
        /// </summary>
        public bool IsBuildable { get { return this.modelJob.IsBuildable; } }

        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild FirstBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.FirstBuild); } }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInQueue { get { return this.modelJob.IsInQueue; } }

        /// <summary>
        /// 
        /// </summary>
        public bool IsKeepDependencies { get { return this.modelJob.IsKeepDependencies; } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastCompletedBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastCompletedBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastFailedBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastFailedBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastStableBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastStableBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastSuccessfulBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastSuccessfulBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastUnstableBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastUnstableBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastUnsuccessfulBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastUnsuccessfulBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public int NextBuildNumber { get { return this.modelJob.NextBuildNumber; } }



        /// <summary>
        /// 
        /// </summary>
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


        /// <summary>
        /// 
        /// </summary>
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


        /// <summary>
        /// 
        /// </summary>
        public void StopBuild()
        {
            this.token?.Cancel();
        }


        /// <summary>
        /// 
        /// </summary>
        void IProgress<JenkinsRunProgress>.Report(JenkinsRunProgress value)
        {
            RunProgress?.Invoke(this, value);
        }


        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            this.modelJob = jenkins.GetJobAsync<JenkinsModelJob>(this.Name).Result;
        }
    }
}
