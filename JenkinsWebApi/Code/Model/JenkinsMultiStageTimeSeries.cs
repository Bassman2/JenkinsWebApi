using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.MultiStageTimeSeries
    public partial class JenkinsMultiStageTimeSeries
    {
        [XmlElement("hour")]
        public JenkinsTimeSeries[] Hours { get; set; }

        [XmlElement("min")]
        public JenkinsTimeSeries[] Mins { get; set; }

        [XmlElement("sec10")]
        public JenkinsTimeSeries[] Sec10s { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
