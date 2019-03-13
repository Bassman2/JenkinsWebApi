using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.LoadStatistics
    public partial class JenkinsLoadStatistics
    {
        [XmlElement("availableExecutors")]
        public JenkinsMultiStageTimeSeries[] AvailableExecutorss { get; set; }

        [XmlElement("busyExecutors")]
        public JenkinsMultiStageTimeSeries[] BusyExecutorss { get; set; }

        [XmlElement("connectingExecutors")]
        public JenkinsMultiStageTimeSeries[] ConnectingExecutorss { get; set; }

        [XmlElement("definedExecutors")]
        public JenkinsMultiStageTimeSeries[] DefinedExecutorss { get; set; }

        [XmlElement("idleExecutors")]
        public JenkinsMultiStageTimeSeries[] IdleExecutorss { get; set; }

        [XmlElement("onlineExecutors")]
        public JenkinsMultiStageTimeSeries[] OnlineExecutorss { get; set; }

        [XmlElement("queueLength")]
        public JenkinsMultiStageTimeSeries[] QueueLengths { get; set; }

        [XmlElement("totalExecutors")]
        public JenkinsMultiStageTimeSeries[] TotalExecutorss { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
