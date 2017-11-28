using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.UpdateCenter
    [XmlRoot("updateCenter")]
    public partial class JenkinsUpdateCenter
    {
        [XmlElement("available")]
        public JenkinsUpdateSitePlugin[] Availables { get; set; }

        [XmlElement("job")]
        public JenkinsUpdateCenterUpdateCenterJob[] Jobs { get; set; }

        [XmlElement("restartRequiredForCompletion")]
        public bool IsRestartRequiredForCompletion { get; set; }

        [XmlElement("site")]
        public JenkinsUpdateSite[] Sites { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
