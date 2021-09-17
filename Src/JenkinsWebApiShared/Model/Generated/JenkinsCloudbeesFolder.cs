using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.cloudbees.hudson.plugins.folder.Folder")]
    public partial class JenkinsCloudbeesFolder : JenkinsCloudbeesAbstractFolder
    {
        // empty
    }
}
