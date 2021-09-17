using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.labels.LabelAtom")]
    [XmlRoot("labelAtom")]
    public partial class JenkinsModelLabelsLabelAtom : JenkinsModelLabel
    {
        [XmlElement("propertiesList")]
        public JenkinsModelLabelsLabelAtomProperty[] PropertiesLists { get; set; }

    }
}
