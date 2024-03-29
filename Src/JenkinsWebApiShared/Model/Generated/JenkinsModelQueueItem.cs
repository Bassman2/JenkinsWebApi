using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Queue-Item")]
    public partial class JenkinsModelQueueItem : JenkinsModelActionable
    {
        [XmlElement("blocked")]
        public bool IsBlocked { get; set; }

        [XmlElement("buildable")]
        public bool IsBuildable { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("inQueueSince")]
        public long InQueueSince { get; set; }

        [XmlElement("params")]
        public string Params { get; set; }

        [XmlElement("stuck")]
        public bool IsStuck { get; set; }

        [XmlElement("task")]
        public object Task { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("why")]
        public string Why { get; set; }

    }
}
