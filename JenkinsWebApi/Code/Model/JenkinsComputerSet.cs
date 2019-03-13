using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.ComputerSet
    [XmlRoot("computerSet")]
    public partial class JenkinsComputerSet
    {
        [XmlElement("busyExecutors")]
        public int BusyExecutors { get; set; }

        [XmlElement("computer")]
        public JenkinsComputer[] Computers { get; set; }

        [XmlElement("displayName")]
        public string[] DisplayNames { get; set; }

        [XmlElement("totalExecutors")]
        public int TotalExecutors { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
