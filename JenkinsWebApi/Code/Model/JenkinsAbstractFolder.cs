using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // com.cloudbees.hudson.plugins.folder.AbstractFolder
    public partial class JenkinsAbstractFolder : JenkinsAbstractItem
    {
        [XmlElement("healthReport")]
        public JenkinsHealthReport[] HealthReports { get; set; }

        [XmlElement("job")]
        public JenkinsJob[] Jobs { get; set; }

        [XmlElement("primaryView")]
        public JenkinsView PrimaryView { get; set; }

        [XmlElement("view")]
        public JenkinsView[] Views { get; set; }

    }
}
