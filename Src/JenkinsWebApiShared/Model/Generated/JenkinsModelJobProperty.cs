using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.JobProperty
    public partial class JenkinsModelJobProperty
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
