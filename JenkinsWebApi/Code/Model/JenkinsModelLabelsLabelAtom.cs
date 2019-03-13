using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.labels.LabelAtom
    [XmlRoot("labelAtom")]
    public partial class JenkinsModelLabelsLabelAtom : JenkinsModelLabel
    {
        [XmlElement("propertiesList")]
        public JenkinsModelLabelsLabelAtomProperty[] PropertiesLists { get; set; }

    }
}
