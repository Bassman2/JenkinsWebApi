using System.Runtime.Serialization;
using System.Text.Json.Serialization;

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
