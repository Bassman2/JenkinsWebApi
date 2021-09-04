using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // com.cloudbees.plugins.credentials.CredentialsStoreAction
    public partial class JenkinsComCloudbeesPluginsCredentialsCredentialsStoreAction
    {
        [JsonPropertyName("domains")]
        public object Domains { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
