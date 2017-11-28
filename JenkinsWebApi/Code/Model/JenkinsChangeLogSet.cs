using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.scm.ChangeLogSet
    public partial class JenkinsChangeLogSet
    {
        [XmlElement("item")]
        public object[] Items { get; set; }

        [XmlElement("kind")]
        public string Kind { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
