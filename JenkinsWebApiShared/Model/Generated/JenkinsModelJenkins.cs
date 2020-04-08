using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // jenkins.model.Jenkins
    public partial class JenkinsModelJenkins : JenkinsModelAbstractCIBase
    {
        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("job")]
        public JenkinsModelJob[] Jobs { get; set; }

        [XmlElement("overallLoad")]
        public JenkinsModelOverallLoadStatistics OverallLoad { get; set; }

        [XmlElement("primaryView")]
        public JenkinsModelView PrimaryView { get; set; }

        [XmlElement("quietingDown")]
        public bool IsQuietingDown { get; set; }

        [XmlElement("slaveAgentPort")]
        public int SlaveAgentPort { get; set; }

        [XmlElement("unlabeledLoad")]
        public JenkinsModelLoadStatistics UnlabeledLoad { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("useCrumbs")]
        public bool UseCrumbs { get; set; }

        [XmlElement("useSecurity")]
        public bool UseSecurity { get; set; }

        [XmlElement("view")]
        public JenkinsModelView[] Views { get; set; }

    }
}
