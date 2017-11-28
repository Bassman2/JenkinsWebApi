using System.Xml.Serialization;

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
