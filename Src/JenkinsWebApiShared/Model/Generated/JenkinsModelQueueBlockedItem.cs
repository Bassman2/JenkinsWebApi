using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.Queue-BlockedItem
    [XmlRoot("blockedItem")]
    public partial class JenkinsModelQueueBlockedItem : JenkinsModelQueueNotWaitingItem
    {
        // empty
    }
}
