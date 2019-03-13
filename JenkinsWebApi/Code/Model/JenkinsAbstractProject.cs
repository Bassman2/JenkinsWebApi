using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.AbstractProject
    public partial class JenkinsAbstractProject : JenkinsJob
    {
        [XmlElement("concurrentBuild")]
        public bool IsConcurrentBuild { get; set; }

        [XmlElement("downstreamProject")]
        public JenkinsAbstractProject[] DownstreamProjects { get; set; }

        [XmlElement("labelExpression")]
        public string[] LabelExpressions { get; set; }

        [XmlElement("scm")]
        public JenkinsSCM[] Scms { get; set; }

        [XmlElement("upstreamProject")]
        public JenkinsAbstractProject[] UpstreamProjects { get; set; }

    }
}
