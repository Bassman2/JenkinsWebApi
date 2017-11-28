using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.labels.LabelAtom
    public partial class JenkinsLabelAtom : JenkinsLabel
    {
        [XmlElement("propertiesList")]
        public JenkinsLabelAtomProperty[] PropertiesLists { get; set; }

    }
}
