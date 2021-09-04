using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Actionable
    public partial class JenkinsModelActionable
    {
        [JsonPropertyName("action")]
        public object[] Actions { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
