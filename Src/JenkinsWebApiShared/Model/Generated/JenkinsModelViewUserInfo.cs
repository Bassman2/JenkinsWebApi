using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.View-UserInfo
    public partial class JenkinsModelViewUserInfo
    {
        [JsonPropertyName("lastChange")]
        public long LastChange { get; set; }

        [JsonPropertyName("project")]
        public JenkinsModelJob Project { get; set; }

        [JsonPropertyName("user")]
        public JenkinsModelUser User { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
