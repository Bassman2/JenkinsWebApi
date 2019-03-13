using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.View-UserInfo
    public partial class JenkinsViewUserInfo
    {
        [XmlElement("lastChange")]
        public long[] LastChanges { get; set; }

        [XmlElement("project")]
        public JenkinsJob[] Projects { get; set; }

        [XmlElement("user")]
        public JenkinsUser[] Users { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
