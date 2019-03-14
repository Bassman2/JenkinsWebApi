using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    public enum JenkinsModelNodeMode
    {
        [XmlEnum("NORMAL")]
        Normal,
        [XmlEnum("EXCLUSIVE")]
        Exclusive
    }
}
