using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue-BuildableItem
    [XmlRoot("buildableItem")]
    public partial class JenkinsModelQueueBuildableItem : JenkinsModelQueueNotWaitingItem
    {
        [XmlElement("pending")]
        public bool IsPending { get; set; }

    }
}
