using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.View-People
    [XmlRoot("people")]
    public partial class JenkinsViewPeople
    {
        [XmlElement("user")]
        public JenkinsViewUserInfo[] Users { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
