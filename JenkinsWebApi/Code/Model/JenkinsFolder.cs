using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // com.cloudbees.hudson.plugins.folder.Folder
    [XmlRoot("folder")]
    public partial class JenkinsFolder : JenkinsAbstractFolder
    {
        // empty
    }
}
