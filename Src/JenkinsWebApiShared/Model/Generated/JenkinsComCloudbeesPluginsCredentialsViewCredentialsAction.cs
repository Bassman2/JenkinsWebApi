using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // com.cloudbees.plugins.credentials.ViewCredentialsAction
    public partial class JenkinsComCloudbeesPluginsCredentialsViewCredentialsAction
    {
        [JsonPropertyName("stores")]
        public object Stores { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
