using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.OverallLoadStatistics
    public partial class JenkinsOverallLoadStatistics : JenkinsLoadStatistics
    {
        [XmlElement("totalQueueLength")]
        public JenkinsMultiStageTimeSeries TotalQueueLength { get; set; }

    }
}
