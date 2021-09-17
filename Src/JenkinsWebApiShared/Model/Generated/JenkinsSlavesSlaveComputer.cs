using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.slaves.SlaveComputer")]
    public partial class JenkinsSlavesSlaveComputer : JenkinsModelComputer
    {
        [JsonPropertyName("absoluteRemotePath")]
        public string AbsoluteRemotePath { get; set; }

    }
}
