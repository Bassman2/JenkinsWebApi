using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.tikal.jenkins.plugins.multijob.MultiJobBuild-SubBuild")]
    public partial class JenkinsTikalMultiJobBuildSubBuild
    {
        [XmlElement("abort")]
        public bool IsAbort { get; set; }

        [XmlElement("build")]
        public JenkinsModelRun Build { get; set; }

        [XmlElement("buildNumber")]
        public int BuildNumber { get; set; }

        [XmlElement("duration")]
        public string Duration { get; set; }

        [XmlElement("icon")]
        public string Icon { get; set; }

        [XmlElement("jobAlias")]
        public string JobAlias { get; set; }

        [XmlElement("jobName")]
        public string JobName { get; set; }

        [XmlElement("multiJobBuild")]
        public bool IsMultiJobBuild { get; set; }

        [XmlElement("parentBuildNumber")]
        public int ParentBuildNumber { get; set; }

        [XmlElement("parentJobName")]
        public string ParentJobName { get; set; }

        [XmlElement("phaseName")]
        public string PhaseName { get; set; }

        [XmlElement("result")]
        public object Result { get; set; }

        [XmlElement("retry")]
        public bool IsRetry { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
