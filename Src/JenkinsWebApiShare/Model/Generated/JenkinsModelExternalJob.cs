using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.ExternalJob")]
    [XmlRoot("externalJob")]
    public partial class JenkinsModelExternalJob : JenkinsModelViewJob
    {
        // empty
    }
}
