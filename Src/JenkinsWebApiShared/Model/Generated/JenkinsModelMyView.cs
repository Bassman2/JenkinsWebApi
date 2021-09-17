using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.MyView")]
    [XmlRoot("myView")]
    public partial class JenkinsModelMyView : JenkinsModelView
    {
        // empty
    }
}
