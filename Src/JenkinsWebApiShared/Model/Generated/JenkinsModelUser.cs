using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.User
    public partial class JenkinsModelUser
    {
        [JsonPropertyName("absoluteUrl")]
        public string AbsoluteUrl { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("property")]
        public JenkinsModelUserProperty[] Propertys { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
