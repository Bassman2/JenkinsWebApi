using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.PackageResult
    [XmlRoot("packageResult")]
    public partial class JenkinsTasksJunitPackageResult : JenkinsTasksTestMetaTabulatedResult
    {
        [XmlElement("child")]
        public JenkinsTasksJunitClassResult[] Childs { get; set; }

        [XmlElement("failCount")]
        public int FailCount { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("passCount")]
        public int PassCount { get; set; }

        [XmlElement("skipCount")]
        public int SkipCount { get; set; }

    }
}
