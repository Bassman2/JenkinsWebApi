using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.AbstractBuild
    public partial class JenkinsAbstractBuild : JenkinsRun
    {
        [XmlElement("builtOn")]
        public string[] BuiltOns { get; set; }

        [XmlElement("changeSet")]
        public JenkinsChangeLogSet[] ChangeSets { get; set; }

        [XmlElement("culprit")]
        public JenkinsUser[] Culprits { get; set; }

    }
}
