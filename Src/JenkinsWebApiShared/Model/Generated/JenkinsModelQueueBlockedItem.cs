using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Queue-BlockedItem")]
    public partial class JenkinsModelQueueBlockedItem : JenkinsModelQueueNotWaitingItem
    {
        // empty
    }
}
