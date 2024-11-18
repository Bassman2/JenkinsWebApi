using JenkinsWebApi.Internal;
using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace JenkinsWebApi.ObjectModel
{
    /// <summary>
    /// Jenkins job class.
    /// </summary>
    public sealed class JenkinsJob : IProgress<JenkinsRunProgress>
    {
        private readonly Jenkins jenkins;
        private JenkinsModelJob modelJob;
        private bool isCompleteLoaded;

        private CancellationTokenSource token = null;
        
        /// <summary>
        /// Event to signal the progress of the running job.
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
        /// The description of this Jenkins job.
        /// </summary>
        public string Description { get { CheckUpdate(); return this.modelJob.Description; } }

        /// <summary>
        /// The display name of this Jenkins job.
        /// </summary>
        public string DisplayName { get { CheckUpdate(); return this.modelJob.DisplayName; } }

        /// <summary>
        /// The display name or null of this Jenkins job.
        /// </summary>
        public string DisplayNameOrNull { get { CheckUpdate(); return this.modelJob.DisplayNameOrNull; } }

        /// <summary>
        /// The full display name of this Jenkins job.
        /// </summary>
        public string FullDisplayName { get { CheckUpdate(); return this.modelJob.FullDisplayName; } }

        /// <summary>
        /// The full name of this Jenkins job.
        /// </summary>
        public string FullName { get { CheckUpdate(); return this.modelJob.FullName; } }

        /// <summary>
        /// The name of this Jenkins job.
        /// </summary>
        public string Name { get { return this.modelJob.Name; } }

        /// <summary>
        /// The URL of this Jenkins job.
        /// </summary>
        public Uri Url { get { return new Uri(this.modelJob.Url); } }

        // JenkinsModelJob


        /// <summary>
        /// Show if the Jenkins job is buildable.
        /// </summary>
        public bool IsBuildable { get { CheckUpdate(); return this.modelJob.IsBuildable; } }

        /// <summary>
        /// Get the first Jenkins build.
        /// </summary>
        public JenkinsBuild FirstBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.FirstBuild); } }

        /// <summary>
        /// Signal if a jenkins job is in build queue.
        /// </summary>
        public bool IsInQueue { get { CheckUpdate(); return this.modelJob.IsInQueue; } }

        /// <summary>
        /// Signal if a Jenkins job keep dependencies.
        /// </summary>
        public bool IsKeepDependencies { get { CheckUpdate(); return this.modelJob.IsKeepDependencies; } }

        /// <summary>
        /// Get the last Jenkins build.
        /// </summary>
        public JenkinsBuild LastBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastBuild); } }

        /// <summary>
        /// Get the last completed Jenkins build.
        /// </summary>
        public JenkinsBuild LastCompletedBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastCompletedBuild); } }

        /// <summary>
        /// Get the last failed Jenkins build.
        /// </summary>
        public JenkinsBuild LastFailedBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastFailedBuild); } }

        /// <summary>
        /// Get the last stable Jenkins build.
        /// </summary>
        public JenkinsBuild LastStableBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastStableBuild); } }


        /// <summary>
        /// Get the last successful Jenkins build.
        /// </summary>
        public JenkinsBuild LastSuccessfulBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastSuccessfulBuild); } }


        /// <summary>
        /// Get the last unstable Jenkins build.
        /// </summary>
        public JenkinsBuild LastUnstableBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastUnstableBuild); } }


        /// <summary>
        /// Get the last unsuccessful Jenkins build.
        /// </summary>
        public JenkinsBuild LastUnsuccessfulBuild { get { CheckUpdate(); return new JenkinsBuild(this.jenkins, this, this.modelJob.LastUnsuccessfulBuild); } }


        /// <summary>
        /// Get the next build number.
        /// </summary>
        public int NextBuildNumber { get { CheckUpdate(); return this.modelJob.NextBuildNumber; } }



        /// <summary>
        /// Get and set the job configuration as text.
        /// </summary>
        public string ConfigText
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
        /// Get and set the job configuration as XmlDocument.
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

        /// <summary>
        /// All builds.
        /// </summary>
        public IEnumerable<JenkinsBuild> Builds { get { CheckUpdate();  return this.modelJob.Builds.Select(j => new JenkinsBuild(this.jenkins, this, j)); } }


        #endregion

        /// <summary>
        /// Start this Jenkins job and return after it is queued.
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
        /// Start this Jenkins job and return after it is startet.
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
        /// Start this Jenkins job and return after it has finished.
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
        /// Breaks the QueueBuild, StartBuild and RunBuild.
        /// </summary>
        public void StopBuild()
        {
            this.token?.Cancel();
        }
                
        void IProgress<JenkinsRunProgress>.Report(JenkinsRunProgress value)
        {
            RunProgress?.Invoke(this, value);
        }

        /// <summary>
        /// Refresh the properties of the Jenkins job.
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
