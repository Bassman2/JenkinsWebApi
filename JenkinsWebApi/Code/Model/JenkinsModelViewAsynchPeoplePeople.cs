using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.View-AsynchPeople-People
    [XmlRoot("people")]
    public partial class JenkinsModelViewAsynchPeoplePeople
    {
        [XmlElement("user")]
        public JenkinsModelViewUserInfo[] Users { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
