using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.AbstractBuild
    public partial class JenkinsModelAbstractBuild : JenkinsModelRun
    {
        [XmlElement("builtOn")]
        public string BuiltOn { get; set; }

        [XmlElement("changeSet")]
        public JenkinsScmChangeLogSet ChangeSet { get; set; }

        [XmlElement("culprit")]
        public JenkinsModelUser[] Culprits { get; set; }

    }
}
