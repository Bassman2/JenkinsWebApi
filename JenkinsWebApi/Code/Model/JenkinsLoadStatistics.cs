using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.LoadStatistics
    public partial class JenkinsLoadStatistics
    {
        [XmlElement("availableExecutors")]
        public JenkinsMultiStageTimeSeries AvailableExecutors { get; set; }

        [XmlElement("busyExecutors")]
        public JenkinsMultiStageTimeSeries BusyExecutors { get; set; }

        [XmlElement("connectingExecutors")]
        public JenkinsMultiStageTimeSeries ConnectingExecutors { get; set; }

        [XmlElement("definedExecutors")]
        public JenkinsMultiStageTimeSeries DefinedExecutors { get; set; }

        [XmlElement("idleExecutors")]
        public JenkinsMultiStageTimeSeries IdleExecutors { get; set; }

        [XmlElement("onlineExecutors")]
        public JenkinsMultiStageTimeSeries OnlineExecutors { get; set; }

        [XmlElement("queueLength")]
        public JenkinsMultiStageTimeSeries QueueLength { get; set; }

        [XmlElement("totalExecutors")]
        public JenkinsMultiStageTimeSeries TotalExecutors { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
