using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.slaves.SlaveComputer")]
    public partial class JenkinsSlavesSlaveComputer : JenkinsModelComputer
    {
        [JsonPropertyName("absoluteRemotePath")]
        public string AbsoluteRemotePath { get; set; }

    }
}
