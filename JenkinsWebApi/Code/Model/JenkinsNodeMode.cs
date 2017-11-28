using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Node-Mode
    public enum JenkinsNodeMode
    {
        [XmlEnum("NORMAL")]
        Normal,

        [XmlEnum("EXCLUSIVE")]
        Exclusive,

    }
}
