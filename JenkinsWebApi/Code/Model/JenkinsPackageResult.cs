using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.PackageResult
    [XmlRoot("packageResult")]
    public partial class JenkinsPackageResult : JenkinsMetaTabulatedResult
    {
        [XmlElement("child")]
        public JenkinsClassResult[] Childs { get; set; }

        [XmlElement("failCount")]
        public int FailCount { get; set; }

        [XmlElement("name")]
        public string[] Names { get; set; }

        [XmlElement("passCount")]
        public int PassCount { get; set; }

        [XmlElement("skipCount")]
        public int SkipCount { get; set; }

    }
}
