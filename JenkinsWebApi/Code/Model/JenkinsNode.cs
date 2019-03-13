using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Node
    public partial class JenkinsNode
    {
        [XmlElement("assignedLabel")]
        public JenkinsLabelAtom[] AssignedLabels { get; set; }

        [XmlElement("mode")]
        public JenkinsNodeMode[] Modes { get; set; }

        [XmlElement("nodeDescription")]
        public string[] NodeDescriptions { get; set; }

        [XmlElement("nodeName")]
        public string[] NodeNames { get; set; }

        [XmlElement("numExecutors")]
        public int NumExecutors { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
