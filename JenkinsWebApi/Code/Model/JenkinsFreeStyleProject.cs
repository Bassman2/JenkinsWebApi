using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.FreeStyleProject
    [XmlRoot("freeStyleProject")]
    public partial class JenkinsFreeStyleProject : JenkinsProject
    {
        // empty
    }
}
