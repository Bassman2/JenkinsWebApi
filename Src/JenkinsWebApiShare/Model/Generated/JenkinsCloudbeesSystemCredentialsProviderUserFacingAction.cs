using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.cloudbees.plugins.credentials.SystemCredentialsProvider-UserFacingAction")]
    [XmlRoot("userFacingAction")]
    public partial class JenkinsCloudbeesSystemCredentialsProviderUserFacingAction : JenkinsCloudbeesCredentialsStoreAction
    {
        // empty
    }
}
