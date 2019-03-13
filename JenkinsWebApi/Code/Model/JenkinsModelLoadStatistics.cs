using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.LoadStatistics
    public partial class JenkinsModelLoadStatistics
    {
        [XmlElement("availableExecutors")]
        public JenkinsModelMultiStageTimeSeries AvailableExecutors { get; set; }

        [XmlElement("busyExecutors")]
        public JenkinsModelMultiStageTimeSeries BusyExecutors { get; set; }

        [XmlElement("connectingExecutors")]
        public JenkinsModelMultiStageTimeSeries ConnectingExecutors { get; set; }

        [XmlElement("definedExecutors")]
        public JenkinsModelMultiStageTimeSeries DefinedExecutors { get; set; }

        [XmlElement("idleExecutors")]
        public JenkinsModelMultiStageTimeSeries IdleExecutors { get; set; }

        [XmlElement("onlineExecutors")]
        public JenkinsModelMultiStageTimeSeries OnlineExecutors { get; set; }

        [XmlElement("queueLength")]
        public JenkinsModelMultiStageTimeSeries QueueLength { get; set; }

        [XmlElement("totalExecutors")]
        public JenkinsModelMultiStageTimeSeries TotalExecutors { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
