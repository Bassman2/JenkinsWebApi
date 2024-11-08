using System.Net;
using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // must always be XML
    // no JSON support

    /// <summary>
    /// Jenkins server instance information.
    /// </summary>
    [XmlRoot("hudson")]
    public class JenkinsInstance
    {
        /// <summary>
        /// Version of the Jenkins server.
        /// </summary>
        [XmlElement("version")]
        public string Version { get; set; }

        /// <summary>
        /// Id of the Jenkins server.
        /// </summary>
        [XmlElement("server-id")]
        public string ServerId { get; set; }
        
        /// <summary>
        /// URL of the Jenkins server.
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// Port to connect the Jenkins server.
        /// </summary>
        [XmlElement("slave-port")]
        public int SlavePort { get; set; }

        /// <summary>
        /// IP of the Jenkins server
        /// </summary>
        [XmlIgnore]
        public IPAddress Address { get; set; }
    }
}
