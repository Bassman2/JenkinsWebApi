using JenkinsWebApi.Internal;
using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace JenkinsWebApi.ObjectModel
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class JenkinsJob : IProgress<JenkinsRunProgress>
    {
        private readonly Jenkins jenkins;
        private JenkinsModelJob modelJob;
        private bool isCompleteLoaded;

        private CancellationTokenSource token = null;
        
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<JenkinsRunProgress> RunProgress;

        internal JenkinsJob(Jenkins jenkins, JenkinsModelJob modelJob)
        {
            this.jenkins = jenkins;
            this.modelJob = modelJob;
            this.isCompleteLoaded = false;

        }

        internal JenkinsJob(Jenkins jenkins, string jobName)
        {
            this.jenkins = jenkins;
            this.modelJob = JenkinsRun.Run(() => jenkins.GetJobAsync<JenkinsModelJob>(jobName).Result);
            this.isCompleteLoaded = true;
        }

        #region Properties

        // JenkinsModelAbstractItem

        /// <summary>
        /// 
        /// </summary>
        public string Description { get { CheckUpdate(); return this.modelJob.Description; } }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get { CheckUpdate(); return this.modelJob.DisplayName; } }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayNameOrNull { get { CheckUpdate(); return this.modelJob.DisplayNameOrNull; } }

        /// <summary>
        /// 
        /// </summary>
        public string FullDisplayName { get { CheckUpdate(); return this.modelJob.FullDisplayName; } }

        /// <summary>
        /// 
        /// </summary>
        public string FullName { get { CheckUpdate(); return this.modelJob.FullName; } }

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
        public bool IsBuildable { get { CheckUpdate(); return this.modelJob.IsBuildable; } }

        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild FirstBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.FirstBuild); } }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInQueue { get { CheckUpdate(); return this.modelJob.IsInQueue; } }

        /// <summary>
        /// 
        /// </summary>
        public bool IsKeepDependencies { get { CheckUpdate(); return this.modelJob.IsKeepDependencies; } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastCompletedBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastCompletedBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastFailedBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastFailedBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastStableBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastStableBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastSuccessfulBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastSuccessfulBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastUnstableBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastUnstableBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public JenkinsBuild LastUnsuccessfulBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastUnsuccessfulBuild); } }


        /// <summary>
        /// 
        /// </summary>
        public int NextBuildNumber { get { CheckUpdate(); return this.modelJob.NextBuildNumber; } }



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
            return JenkinsRun.Run(() =>
            {
                this.token = new CancellationTokenSource();
                return Task.Run(() =>
                {
                    JenkinsRunConfig runConfig = this.jenkins.RunConfig.Clone();
                    runConfig.RunMode = JenkinsRunMode.Queued;
                    JenkinsRunProgress res = this.jenkins.RunJobAsync(this.modelJob.Name, parameters, runConfig, this, this.token.Token).Result;
                    return new JenkinsBuild(this.jenkins, this, res.BuildNum);
                }, this.token.Token).Result;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JenkinsBuild StartBuild(JenkinsBuildParameters parameters = null)
        {
            return JenkinsRun.Run(() =>
            {
                this.token = new CancellationTokenSource();
                return Task.Run(() =>
                {
                    JenkinsRunConfig runConfig = this.jenkins.RunConfig.Clone();
                    runConfig.RunMode = JenkinsRunMode.Started;
                    JenkinsRunProgress res = this.jenkins.RunJobAsync(this.modelJob.Name, parameters, runConfig, this, this.token.Token).Result;
                    return new JenkinsBuild(this.jenkins, this, res.BuildNum);
                }, this.token.Token).Result;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JenkinsBuild RunBuild(JenkinsBuildParameters parameters = null)
        {
            return JenkinsRun.Run(() =>
            {
                this.token = new CancellationTokenSource();
                return Task.Run(() =>
                {
                    JenkinsRunConfig runConfig = this.jenkins.RunConfig.Clone();
                    runConfig.RunMode = JenkinsRunMode.Finished;
                    JenkinsRunProgress res = this.jenkins.RunJobAsync(this.modelJob.Name, parameters, runConfig, this, this.token.Token).Result;
                    return new JenkinsBuild(this.jenkins, this, res.BuildNum);
                }, this.token.Token).Result;
            });
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
            this.modelJob = JenkinsRun.Run(() => jenkins.GetJobAsync<JenkinsModelJob>(this.Name).Result);
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
