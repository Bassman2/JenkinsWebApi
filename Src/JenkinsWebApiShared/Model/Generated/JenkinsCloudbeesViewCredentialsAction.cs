using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.cloudbees.plugins.credentials.ViewCredentialsAction")]
    public partial class JenkinsCloudbeesViewCredentialsAction
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
