using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.View-UserInfo")]
    public partial class JenkinsModelViewUserInfo
    {
        [XmlElement("lastChange")]
        public long LastChange { get; set; }

        [XmlElement("project")]
        public JenkinsModelJob Project { get; set; }

        [XmlElement("user")]
        public JenkinsModelUser User { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
