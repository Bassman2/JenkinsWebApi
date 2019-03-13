using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.slaves.SlaveComputer
    [XmlRoot("slaveComputer")]
    public partial class JenkinsSlaveComputer : JenkinsComputer
    {
        [XmlElement("absoluteRemotePath")]
        public string[] AbsoluteRemotePaths { get; set; }

    }
}
