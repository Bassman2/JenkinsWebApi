using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // jenkins.YesNoMaybe
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum JenkinsYesNoMaybe
    {
        [EnumMember(Value = "YES")]
        Yes,

        [EnumMember(Value = "NO")]
        No,

        [EnumMember(Value = "MAYBE")]
        Maybe,

    }
}
