using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.LoadStatistics
    public partial class JenkinsModelLoadStatistics
    {
        [JsonPropertyName("availableExecutors")]
        public JenkinsModelMultiStageTimeSeries AvailableExecutors { get; set; }

        [JsonPropertyName("busyExecutors")]
        public JenkinsModelMultiStageTimeSeries BusyExecutors { get; set; }

        [JsonPropertyName("connectingExecutors")]
        public JenkinsModelMultiStageTimeSeries ConnectingExecutors { get; set; }

        [JsonPropertyName("definedExecutors")]
        public JenkinsModelMultiStageTimeSeries DefinedExecutors { get; set; }

        [JsonPropertyName("idleExecutors")]
        public JenkinsModelMultiStageTimeSeries IdleExecutors { get; set; }

        [JsonPropertyName("onlineExecutors")]
        public JenkinsModelMultiStageTimeSeries OnlineExecutors { get; set; }

        [JsonPropertyName("queueLength")]
        public JenkinsModelMultiStageTimeSeries QueueLength { get; set; }

        [JsonPropertyName("totalExecutors")]
        public JenkinsModelMultiStageTimeSeries TotalExecutors { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
