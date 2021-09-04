using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Computer
    public partial class JenkinsModelComputer : JenkinsModelActionable
    {
        [JsonPropertyName("assignedLabel")]
        public JenkinsModelLabelsLabelAtom[] AssignedLabels { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("executor")]
        public JenkinsModelExecutor[] Executors { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("iconClassName")]
        public string IconClassName { get; set; }

        [JsonPropertyName("idle")]
        public bool IsIdle { get; set; }

        [JsonPropertyName("jnlpAgent")]
        public bool IsJnlpAgent { get; set; }

        [JsonPropertyName("launchSupported")]
        public bool IsLaunchSupported { get; set; }

        [JsonPropertyName("loadStatistics")]
        public JenkinsModelLoadStatistics LoadStatistics { get; set; }

        [JsonPropertyName("manualLaunchAllowed")]
        public bool IsManualLaunchAllowed { get; set; }

        [JsonPropertyName("numExecutors")]
        public int NumExecutors { get; set; }

        [JsonPropertyName("offline")]
        public bool IsOffline { get; set; }

        [JsonPropertyName("offlineCause")]
        public JenkinsSlavesOfflineCause OfflineCause { get; set; }

        [JsonPropertyName("offlineCauseReason")]
        public string OfflineCauseReason { get; set; }

        [JsonPropertyName("oneOffExecutor")]
        public JenkinsModelOneOffExecutor[] OneOffExecutors { get; set; }

        [JsonPropertyName("temporarilyOffline")]
        public bool IsTemporarilyOffline { get; set; }

    }
}
