using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.UpdateCenter-UpdateCenterJob
    public partial class JenkinsUpdateCenterUpdateCenterJob
    {
        [XmlElement("errorMessage")]
        public string ErrorMessage { get; set; }

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
