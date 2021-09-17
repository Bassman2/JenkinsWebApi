using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.cloudbees.hudson.plugins.folder.Folder")]
    public partial class JenkinsCloudbeesFolder : JenkinsCloudbeesAbstractFolder
    {
        // empty
    }
}
