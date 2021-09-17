using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.Node-Mode
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum JenkinsModelNodeMode
    {
        [EnumMember(Value = "NORMAL")]
        Normal,

        [EnumMember(Value = "EXCLUSIVE")]
        Exclusive,

    }
}
