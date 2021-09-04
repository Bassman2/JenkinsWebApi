using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.PluginWrapper
    public partial class JenkinsPluginWrapper
    {
        [JsonPropertyName("active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("backupVersion")]
        public string BackupVersion { get; set; }

        [JsonPropertyName("bundled")]
        public bool IsBundled { get; set; }

        [JsonPropertyName("deleted")]
        public bool IsDeleted { get; set; }

        [JsonPropertyName("dependency")]
        public JenkinsPluginWrapperDependency[] Dependencys { get; set; }

        [JsonPropertyName("detached")]
        public bool IsDetached { get; set; }

        [JsonPropertyName("downgradable")]
        public bool IsDowngradable { get; set; }

        [JsonPropertyName("enabled")]
        public bool IsEnabled { get; set; }

        [JsonPropertyName("hasUpdate")]
        public bool IsHasUpdate { get; set; }

        [JsonPropertyName("longName")]
        public string LongName { get; set; }

        [JsonPropertyName("minimumJavaVersion")]
        public string MinimumJavaVersion { get; set; }

        [JsonPropertyName("pinned")]
        public bool IsPinned { get; set; }

        [JsonPropertyName("requiredCoreVersion")]
        public string RequiredCoreVersion { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("supportsDynamicLoad")]
        public JenkinsYesNoMaybe SupportsDynamicLoad { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
