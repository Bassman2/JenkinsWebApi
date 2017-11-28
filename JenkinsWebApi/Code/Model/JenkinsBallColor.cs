using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.BallColor
    public enum JenkinsBallColor
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
