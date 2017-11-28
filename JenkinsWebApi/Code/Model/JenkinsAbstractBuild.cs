using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.AbstractBuild
    public partial class JenkinsAbstractBuild : JenkinsRun
    {
        [XmlElement("builtOn")]
        public string BuiltOn { get; set; }

        [XmlElement("changeSet")]
        public JenkinsChangeLogSet ChangeSet { get; set; }

        [XmlElement("fingerprint")]
        public JenkinsFingerprint[] Fingerprints { get; set; }

    }
}
