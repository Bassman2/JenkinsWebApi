using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.slaves.SlaveComputer
    [XmlRoot("slaveComputer")]
    public partial class JenkinsSlavesSlaveComputer : JenkinsModelComputer
    {
        [XmlElement("absoluteRemotePath")]
        public string AbsoluteRemotePath { get; set; }

    }
}
