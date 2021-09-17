using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Job")]
    public partial class JenkinsModelJob : JenkinsModelAbstractItem
    {
        [JsonPropertyName("allBuild")]
        public JenkinsModelRun[] AllBuilds { get; set; }

        [JsonPropertyName("buildable")]
        public bool IsBuildable { get; set; }

        [JsonPropertyName("build")]
        public JenkinsModelRun[] Builds { get; set; }

        [JsonPropertyName("color")]
        public JenkinsModelBallColor Color { get; set; }

        [JsonPropertyName("firstBuild")]
        public JenkinsModelRun FirstBuild { get; set; }

        [JsonPropertyName("healthReport")]
        public JenkinsModelHealthReport[] HealthReports { get; set; }

        [JsonPropertyName("inQueue")]
        public bool IsInQueue { get; set; }

        [JsonPropertyName("keepDependencies")]
        public bool IsKeepDependencies { get; set; }

        [JsonPropertyName("lastBuild")]
        public JenkinsModelRun LastBuild { get; set; }

        [JsonPropertyName("lastCompletedBuild")]
        public JenkinsModelRun LastCompletedBuild { get; set; }

        [JsonPropertyName("lastFailedBuild")]
        public JenkinsModelRun LastFailedBuild { get; set; }

        [JsonPropertyName("lastStableBuild")]
        public JenkinsModelRun LastStableBuild { get; set; }

        [JsonPropertyName("lastSuccessfulBuild")]
        public JenkinsModelRun LastSuccessfulBuild { get; set; }

        [JsonPropertyName("lastUnstableBuild")]
        public JenkinsModelRun LastUnstableBuild { get; set; }

        [JsonPropertyName("lastUnsuccessfulBuild")]
        public JenkinsModelRun LastUnsuccessfulBuild { get; set; }

        [JsonPropertyName("nextBuildNumber")]
        public int NextBuildNumber { get; set; }

        [JsonPropertyName("property")]
        public JenkinsModelJobProperty[] Propertys { get; set; }

        [JsonPropertyName("queueItem")]
        public JenkinsModelQueueItem QueueItem { get; set; }

    }
}
