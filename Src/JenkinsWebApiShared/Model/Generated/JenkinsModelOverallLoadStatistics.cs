using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.OverallLoadStatistics")]
    public partial class JenkinsModelOverallLoadStatistics : JenkinsModelLoadStatistics
    {
        [XmlElement("totalQueueLength")]
        public JenkinsModelMultiStageTimeSeries TotalQueueLength { get; set; }

    }
}
