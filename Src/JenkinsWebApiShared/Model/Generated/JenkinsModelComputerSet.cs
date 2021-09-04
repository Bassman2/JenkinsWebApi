using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.ComputerSet
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
