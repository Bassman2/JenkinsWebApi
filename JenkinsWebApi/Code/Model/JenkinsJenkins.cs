using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // jenkins.model.Jenkins
    public partial class JenkinsJenkins : JenkinsAbstractCIBase
    {
        [XmlElement("description")]
        public string[] Descriptions { get; set; }

        [XmlElement("job")]
        public JenkinsJob[] Jobs { get; set; }

        [XmlElement("overallLoad")]
        public JenkinsOverallLoadStatistics[] OverallLoads { get; set; }

        [XmlElement("primaryView")]
        public JenkinsView[] PrimaryViews { get; set; }

        [XmlElement("quietingDown")]
        public bool IsQuietingDown { get; set; }

        [XmlElement("slaveAgentPort")]
        public int SlaveAgentPort { get; set; }

        [XmlElement("unlabeledLoad")]
        public JenkinsLoadStatistics[] UnlabeledLoads { get; set; }

        [XmlElement("useCrumbs")]
        public bool UseCrumbs { get; set; }

        [XmlElement("useSecurity")]
        public bool UseSecurity { get; set; }

        [XmlElement("view")]
        public JenkinsView[] Views { get; set; }

    }
}
