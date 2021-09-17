using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.View")]
    public partial class JenkinsModelView
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("job")]
        public JenkinsModelJob[] Jobs { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("property")]
        public object[] Propertys { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
