using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // jenkins.YesNoMaybe
    public enum JenkinsYesNoMaybe
    {
        [XmlEnum("YES")]
        Yes,

        [XmlEnum("NO")]
        No,

        [XmlEnum("MAYBE")]
        Maybe,

    }
}
