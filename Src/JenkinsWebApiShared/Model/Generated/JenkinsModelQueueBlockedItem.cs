using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Queue-BlockedItem")]
    public partial class JenkinsModelQueueBlockedItem : JenkinsModelQueueNotWaitingItem
    {
        // empty
    }
}
