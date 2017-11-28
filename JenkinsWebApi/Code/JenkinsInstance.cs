
using System.Net;
using System.Xml.Serialization;

namespace JenkinsWebApi
{
    [XmlRoot("hudson")]
    public class JenkinsInstance
    {
        [XmlElement("version")]
        public string Version { get; set; }

        [XmlElement("server-id")]
        public string ServerId { get; set; }
        
        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("slave-port")]
        public int SlavePort { get; set; }

        [XmlIgnore]
        public IPAddress Address { get; set; }
    }
}
