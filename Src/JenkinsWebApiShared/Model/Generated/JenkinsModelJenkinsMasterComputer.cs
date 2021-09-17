using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("jenkins.model.Jenkins-MasterComputer")]
    public partial class JenkinsModelJenkinsMasterComputer : JenkinsModelComputer
    {
        // empty
    }
}
