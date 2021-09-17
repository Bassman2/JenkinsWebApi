using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Hudson-MasterComputer")]
    public partial class JenkinsModelHudsonMasterComputer : JenkinsModelJenkinsMasterComputer
    {
        // empty
    }
}
