using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.AllView")]
    [XmlRoot("allView")]
    public partial class JenkinsModelAllView : JenkinsModelView
    {
        // empty
    }
}
