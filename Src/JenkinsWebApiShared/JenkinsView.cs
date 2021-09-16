using JenkinsWebApi.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace JenkinsWebApi
{
    public sealed class JenkinsView
    {
        private readonly Jenkins jenkins;
        private JenkinsModelView modelView;

        internal JenkinsView(Jenkins jenkins, string viewName)
        {
            this.jenkins = jenkins;
            this.modelView = jenkins.GetViewAsync<JenkinsModelView>(viewName).Result;
        }

        public string Description { get { return this.modelView.Description; } }
        public IEnumerable<JenkinsJob> Jobs { get { return this.modelView.Jobs.Select(j => new JenkinsJob(this.jenkins, j.Name)); } }
        public IEnumerable<string> JobNames { get { return this.modelView.Jobs.Select(j => j.Name); } }
        public string Name { get { return this.modelView.Name; } }
        public Uri Url { get { return new Uri(this.modelView.Url); } }

        public string Config 
        { 
            get
            {
                return jenkins.GetViewConfigAsync(this.Name).Result;
            }
            set
            {
                jenkins.SetViewConfigAsync(this.Name, value).Wait();
                Update();
            }
        }

        public XmlDocument ConfigXml
        {
            get
            {
                return jenkins.GetViewConfigXmlAsync(this.Name).Result;
            }
            set
            {
                jenkins.SetViewConfigXmlAsync(this.Name, value).Wait();
                Update();
            }
        }

        public void Update()
        {
            this.modelView = jenkins.GetViewAsync<JenkinsModelView>(this.Name).Result;
        }
    }
}
