using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Actionable
    public partial class JenkinsModelActionable
    {
        [XmlElement("action")]
        public object[] Actions { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
