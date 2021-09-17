using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.labels.LabelAtom")]
    public partial class JenkinsModelLabelsLabelAtom : JenkinsModelLabel
    {
        [JsonPropertyName("propertiesList")]
        public JenkinsModelLabelsLabelAtomProperty[] PropertiesLists { get; set; }

    }
}
