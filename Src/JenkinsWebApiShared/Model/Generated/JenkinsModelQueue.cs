using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Queue")]
    public partial class JenkinsModelQueue
    {
        [JsonPropertyName("discoverableItem")]
        public JenkinsModelQueueStubItem[] DiscoverableItems { get; set; }

        [JsonPropertyName("item")]
        public JenkinsModelQueueItem[] Items { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
