using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.ComputerSet")]
    public partial class JenkinsModelComputerSet
    {
        [JsonPropertyName("busyExecutors")]
        public int BusyExecutors { get; set; }

        [JsonPropertyName("computer")]
        public JenkinsModelComputer[] Computers { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("totalExecutors")]
        public int TotalExecutors { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
