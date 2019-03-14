using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
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
        NotBuilt,
        [XmlEnum("notbuilt_anime")]
        NotBuildAnime
    }
}
