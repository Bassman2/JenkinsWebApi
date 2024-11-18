using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Queue-BlockedItem")]
    [XmlRoot("blockedItem")]
    public partial class JenkinsModelQueueBlockedItem : JenkinsModelQueueNotWaitingItem
    {
        // empty
    }
}
