using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.UserProperty
    public partial class JenkinsModelUserProperty
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
