using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.OverallLoadStatistics
    public partial class JenkinsModelOverallLoadStatistics : JenkinsModelLoadStatistics
    {
        [XmlElement("totalQueueLength")]
        public JenkinsModelMultiStageTimeSeries TotalQueueLength { get; set; }

    }
}
