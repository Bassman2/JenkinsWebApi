using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue-Item
    public partial class JenkinsQueueItem : JenkinsActionable
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
        public string[] Paramss { get; set; }

        [XmlElement("stuck")]
        public bool IsStuck { get; set; }

        [XmlElement("task")]
        public object[] Tasks { get; set; }

        [XmlElement("url")]
        public string[] Urls { get; set; }

        [XmlElement("why")]
        public string[] Whys { get; set; }

    }
}
