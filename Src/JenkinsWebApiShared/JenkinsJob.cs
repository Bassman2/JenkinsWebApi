using JenkinsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace JenkinsWebApi
{
    public sealed class JenkinsJob
    {
        private readonly Jenkins jenkins;
        private JenkinsModelJob modelJob;

        internal JenkinsJob(Jenkins jenkins, string jobName)
        {
            this.jenkins = jenkins;
            this.modelJob = jenkins.GetJobAsync<JenkinsModelJob>(jobName).Result;
        }

        public string Name { get { return this.modelJob.Name; } }
        public string Description { get { return this.modelJob.Description; } }

        public JenkinsBuild LastBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastBuild); } }

        public JenkinsBuild LastCompletedBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastCompletedBuild); } }

        public JenkinsBuild LastFailedBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastFailedBuild); } }

        public JenkinsBuild LastStableBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastStableBuild); } }

        public JenkinsBuild LastSuccessfulBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastSuccessfulBuild); } }

        public JenkinsBuild LastUnstableBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastUnstableBuild); } }

        public JenkinsBuild LastUnsuccessfulBuild { get { return new JenkinsBuild(this.jenkins, this, this.modelJob.LastUnsuccessfulBuild); } }



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

        public void Update()
        {
            this.modelJob = jenkins.GetJobAsync<JenkinsModelJob>(this.Name).Result;
        }
    }
}
