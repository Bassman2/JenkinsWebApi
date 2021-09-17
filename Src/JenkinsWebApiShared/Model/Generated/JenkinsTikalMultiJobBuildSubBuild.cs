using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.tikal.jenkins.plugins.multijob.MultiJobBuild-SubBuild")]
    public partial class JenkinsTikalMultiJobBuildSubBuild
    {
        [JsonPropertyName("abort")]
        public bool IsAbort { get; set; }

        [JsonPropertyName("build")]
        public JenkinsModelRun Build { get; set; }

        [JsonPropertyName("buildNumber")]
        public int BuildNumber { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("jobAlias")]
        public string JobAlias { get; set; }

        [JsonPropertyName("jobName")]
        public string JobName { get; set; }

        [JsonPropertyName("multiJobBuild")]
        public bool IsMultiJobBuild { get; set; }

        [JsonPropertyName("parentBuildNumber")]
        public int ParentBuildNumber { get; set; }

        [JsonPropertyName("parentJobName")]
        public string ParentJobName { get; set; }

        [JsonPropertyName("phaseName")]
        public string PhaseName { get; set; }

        [JsonPropertyName("result")]
        public object Result { get; set; }

        [JsonPropertyName("retry")]
        public bool IsRetry { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
