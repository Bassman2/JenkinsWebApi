using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Node
    public partial class JenkinsModelNode
    {
        [XmlElement("assignedLabel")]
        public JenkinsModelLabelsLabelAtom[] AssignedLabels { get; set; }

        [XmlElement("mode")]
        public JenkinsModelNodeMode Mode { get; set; }

        [XmlElement("nodeDescription")]
        public string NodeDescription { get; set; }

        [XmlElement("nodeName")]
        public string NodeName { get; set; }

        [XmlElement("numExecutors")]
        public int NumExecutors { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
