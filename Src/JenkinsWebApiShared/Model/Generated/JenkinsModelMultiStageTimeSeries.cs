using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.MultiStageTimeSeries")]
    public partial class JenkinsModelMultiStageTimeSeries
    {
        [XmlElement("hour")]
        public JenkinsModelTimeSeries Hour { get; set; }

        [XmlElement("min")]
        public JenkinsModelTimeSeries Min { get; set; }

        [XmlElement("sec10")]
        public JenkinsModelTimeSeries Sec10 { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
