using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.cloudbees.plugins.credentials.ViewCredentialsAction-RootActionImpl")]
    [XmlRoot("rootActionImpl")]
    public partial class JenkinsCloudbeesViewCredentialsActionRootActionImpl : JenkinsCloudbeesViewCredentialsAction
    {
        // empty
    }
}
