using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.View-UserInfo
    public partial class JenkinsViewUserInfo
    {
        [XmlElement("lastChange")]
        public long LastChange { get; set; }

        [XmlElement("project")]
        public JenkinsJob Project { get; set; }

        [XmlElement("user")]
        public JenkinsUser User { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
