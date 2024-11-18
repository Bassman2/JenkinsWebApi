using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.cloudbees.hudson.plugins.folder.Folder")]
    [XmlRoot("folder")]
    public partial class JenkinsCloudbeesFolder : JenkinsCloudbeesAbstractFolder
    {
        // empty
    }
}
