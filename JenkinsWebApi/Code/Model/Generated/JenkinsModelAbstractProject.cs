using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.AbstractProject
    public partial class JenkinsModelAbstractProject : JenkinsModelJob
    {
        [XmlElement("concurrentBuild")]
        public bool IsConcurrentBuild { get; set; }

        [XmlElement("downstreamProject")]
        public JenkinsModelAbstractProject[] DownstreamProjects { get; set; }

        [XmlElement("labelExpression")]
        public string LabelExpression { get; set; }

        [XmlElement("scm")]
        public JenkinsScmSCM Scm { get; set; }

        [XmlElement("upstreamProject")]
        public JenkinsModelAbstractProject[] UpstreamProjects { get; set; }

    }
}
