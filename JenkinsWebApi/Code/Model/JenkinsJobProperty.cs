using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.JobProperty
    public partial class JenkinsJobProperty
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
