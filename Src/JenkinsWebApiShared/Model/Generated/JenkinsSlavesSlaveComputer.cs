using System.Xml.Serialization;

#pragma warning disable CS1591

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
