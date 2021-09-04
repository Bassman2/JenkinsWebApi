using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.labels.LabelAtom
    public partial class JenkinsModelLabelsLabelAtom : JenkinsModelLabel
    {
        [JsonPropertyName("propertiesList")]
        public JenkinsModelLabelsLabelAtomProperty[] PropertiesLists { get; set; }

    }
}
