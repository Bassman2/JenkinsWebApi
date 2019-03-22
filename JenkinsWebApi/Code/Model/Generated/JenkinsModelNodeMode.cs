using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.Node-Mode
    public enum JenkinsModelNodeMode
    {
        [XmlEnum("NORMAL")]
        Normal,

        [XmlEnum("EXCLUSIVE")]
        Exclusive,

    }
}
