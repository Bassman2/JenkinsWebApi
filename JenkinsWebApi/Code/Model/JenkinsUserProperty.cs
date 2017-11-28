using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.UserProperty
    public partial class JenkinsUserProperty
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
