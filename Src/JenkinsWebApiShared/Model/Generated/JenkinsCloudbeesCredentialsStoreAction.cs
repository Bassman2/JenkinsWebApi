using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.cloudbees.plugins.credentials.CredentialsStoreAction")]
    public partial class JenkinsCloudbeesCredentialsStoreAction
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
