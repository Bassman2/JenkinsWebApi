using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.ProxyView")]
    [XmlRoot("proxyView")]
    public partial class JenkinsModelProxyView : JenkinsModelView
    {
        // empty
    }
}
