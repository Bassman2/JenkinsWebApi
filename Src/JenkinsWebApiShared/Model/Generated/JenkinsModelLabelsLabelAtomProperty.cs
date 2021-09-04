using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.labels.LabelAtomProperty
    public partial class JenkinsModelLabelsLabelAtomProperty
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
