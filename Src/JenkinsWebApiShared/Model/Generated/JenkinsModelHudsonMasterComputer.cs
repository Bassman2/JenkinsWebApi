using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Hudson-MasterComputer")]
    public partial class JenkinsModelHudsonMasterComputer : JenkinsModelJenkinsMasterComputer
    {
        // empty
    }
}
