using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.TimeSeries")]
    public partial class JenkinsModelTimeSeries
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
