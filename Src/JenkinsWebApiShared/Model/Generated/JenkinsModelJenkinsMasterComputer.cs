using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("jenkins.model.Jenkins-MasterComputer")]
    public partial class JenkinsModelJenkinsMasterComputer : JenkinsModelComputer
    {
        // empty
    }
}
