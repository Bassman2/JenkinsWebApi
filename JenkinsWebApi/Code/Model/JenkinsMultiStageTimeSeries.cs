using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.MultiStageTimeSeries
    public partial class JenkinsMultiStageTimeSeries
    {
        [XmlElement("hour")]
        public JenkinsTimeSeries Hour { get; set; }

        [XmlElement("min")]
        public JenkinsTimeSeries Min { get; set; }

        [XmlElement("sec10")]
        public JenkinsTimeSeries Sec10 { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
