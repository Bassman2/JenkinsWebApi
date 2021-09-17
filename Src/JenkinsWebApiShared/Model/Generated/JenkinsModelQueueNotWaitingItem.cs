using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Queue-NotWaitingItem")]
    public partial class JenkinsModelQueueNotWaitingItem : JenkinsModelQueueItem
    {
        [XmlElement("buildableStartMilliseconds")]
        public long BuildableStartMilliseconds { get; set; }

    }
}
