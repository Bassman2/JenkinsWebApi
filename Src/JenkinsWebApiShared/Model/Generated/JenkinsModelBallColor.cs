using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.BallColor
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum JenkinsModelBallColor
    {
        [EnumMember(Value = "red")]
        Red,

        [EnumMember(Value = "red_anime")]
        RedAnime,

        [EnumMember(Value = "yellow")]
        Yellow,

        [EnumMember(Value = "yellow_anime")]
        YellowAnime,

        [EnumMember(Value = "blue")]
        Blue,

        [EnumMember(Value = "blue_anime")]
        BlueAnime,

        [EnumMember(Value = "grey")]
        Grey,

        [EnumMember(Value = "grey_anime")]
        GreyAnime,

        [EnumMember(Value = "disabled")]
        Disabled,

        [EnumMember(Value = "disabled_anime")]
        DisabledAnime,

        [EnumMember(Value = "aborted")]
        Aborted,

        [EnumMember(Value = "aborted_anime")]
        AbortedAnime,

        [EnumMember(Value = "notbuilt")]
        Notbuilt,

        [EnumMember(Value = "notbuilt_anime")]
        NotbuiltAnime,

    }
}
