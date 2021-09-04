using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.AbstractItem
    public partial class JenkinsModelAbstractItem : JenkinsModelActionable
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("displayNameOrNull")]
        public string DisplayNameOrNull { get; set; }

        [JsonPropertyName("fullDisplayName")]
        public string FullDisplayName { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

    }
}
