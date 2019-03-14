using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // com.cloudbees.hudson.plugins.folder.AbstractFolder
    public partial class JenkinsComCloudbeesHudsonPluginsFolderAbstractFolder : JenkinsModelAbstractItem
    {
        [XmlElement("healthReport")]
        public JenkinsModelHealthReport[] HealthReports { get; set; }

        [XmlElement("job")]
        public JenkinsModelJob[] Jobs { get; set; }

        [XmlElement("primaryView")]
        public JenkinsModelView PrimaryView { get; set; }

        [XmlElement("view")]
        public JenkinsModelView[] Views { get; set; }

    }
}
