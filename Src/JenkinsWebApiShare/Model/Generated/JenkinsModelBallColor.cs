using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.BallColor
    public enum JenkinsModelBallColor
    {
        [XmlEnum("red")]
        Red,

        [XmlEnum("red_anime")]
        RedAnime,

        [XmlEnum("yellow")]
        Yellow,

        [XmlEnum("yellow_anime")]
        YellowAnime,

        [XmlEnum("blue")]
        Blue,

        [XmlEnum("blue_anime")]
        BlueAnime,

        [XmlEnum("grey")]
        Grey,

        [XmlEnum("grey_anime")]
        GreyAnime,

        [XmlEnum("disabled")]
        Disabled,

        [XmlEnum("disabled_anime")]
        DisabledAnime,

        [XmlEnum("aborted")]
        Aborted,

        [XmlEnum("aborted_anime")]
        AbortedAnime,

        [XmlEnum("notbuilt")]
        Notbuilt,

        [XmlEnum("notbuilt_anime")]
        NotbuiltAnime,

    }
}
