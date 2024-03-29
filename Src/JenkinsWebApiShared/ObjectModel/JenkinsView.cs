﻿using JenkinsWebApi.Internal;
using JenkinsWebApi.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace JenkinsWebApi.ObjectModel
{
    /// <summary>
    /// Jenkins view class.
    /// </summary>
    public sealed class JenkinsView
    {
        private readonly Jenkins jenkins;
        private JenkinsModelView modelView;
        private bool isCompleteLoaded;

        internal JenkinsView(Jenkins jenkins, JenkinsModelView modelView)
        {
            this.jenkins = jenkins;
            this.modelView = modelView;
            this.isCompleteLoaded = false;
        }

        internal JenkinsView(Jenkins jenkins, string viewName)
        {
            this.jenkins = jenkins;
            this.modelView = JenkinsRun.Run(() => jenkins.GetViewAsync<JenkinsModelView>(viewName).Result);
            this.isCompleteLoaded = true;
        }

        /// <summary>
        /// Description of the Jenkins view.
        /// </summary>
        public string Description { get { CheckUpdate(); return this.modelView.Description; } }

        /// <summary>
        /// All Jenkins jobs included in this view.
        /// </summary>
        public IEnumerable<JenkinsJob> Jobs { get { CheckUpdate(); return this.modelView.Jobs.Select(j => new JenkinsJob(this.jenkins, j.Name)); } }

        /// <summary>
        /// The names of all jobs included in this view.
        /// </summary>
        public IEnumerable<string> JobNames { get { CheckUpdate(); return this.modelView.Jobs.Select(j => j.Name); } }

        /// <summary>
        /// The name of this Jenkins view.
        /// </summary>
        public string Name { get { return this.modelView.Name; } }

        /// <summary>
        ///  The URL of this Jenkins view. 
        /// </summary>
        public Uri Url { get { return new Uri(this.modelView.Url); } }

        /// <summary>
        /// Get and set the view configuration as text.
        /// </summary>
        public string ConfigText 
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

        /// <summary>
        /// Get and set the view configuration as XmlDocument.
        /// </summary>
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

        /// <summary>
        /// Update the view data.
        /// </summary>
        public void Update()
        {
            this.modelView = JenkinsRun.Run(() => jenkins.GetViewAsync<JenkinsModelView>(this.Name).Result);
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
