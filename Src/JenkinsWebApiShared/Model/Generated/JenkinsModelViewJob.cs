using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.ViewJob")]
    public partial class JenkinsModelViewJob : JenkinsModelJob
    {
        // empty
    }
}
