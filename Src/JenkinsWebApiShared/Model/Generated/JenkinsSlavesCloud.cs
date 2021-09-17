using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.slaves.Cloud")]
    public partial class JenkinsSlavesCloud : JenkinsModelActionable
    {
        // empty
    }
}
