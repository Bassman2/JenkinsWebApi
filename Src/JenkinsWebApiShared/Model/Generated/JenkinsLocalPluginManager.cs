using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.LocalPluginManager")]
    public partial class JenkinsLocalPluginManager : JenkinsPluginManager
    {
        // empty
    }
}
