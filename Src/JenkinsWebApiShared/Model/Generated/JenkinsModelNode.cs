using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Node")]
    public partial class JenkinsModelNode
    {
        [JsonPropertyName("assignedLabel")]
        public JenkinsModelLabelsLabelAtom[] AssignedLabels { get; set; }

        [JsonPropertyName("mode")]
        public JenkinsModelNodeMode Mode { get; set; }

        [JsonPropertyName("nodeDescription")]
        public string NodeDescription { get; set; }

        [JsonPropertyName("nodeName")]
        public string NodeName { get; set; }

        [JsonPropertyName("numExecutors")]
        public int NumExecutors { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
