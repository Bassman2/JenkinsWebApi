using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.labels.LabelAtom")]
    public partial class JenkinsModelLabelsLabelAtom : JenkinsModelLabel
    {
        [JsonPropertyName("propertiesList")]
        public JenkinsModelLabelsLabelAtomProperty[] PropertiesLists { get; set; }

    }
}
