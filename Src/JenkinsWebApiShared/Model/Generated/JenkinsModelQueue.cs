using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

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
