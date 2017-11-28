using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.TimeSeries
    public partial class JenkinsTimeSeries
    {
        [XmlElement("history")]
        public object[] Historys { get; set; }

        [XmlElement("latest")]
        public object Latest { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
