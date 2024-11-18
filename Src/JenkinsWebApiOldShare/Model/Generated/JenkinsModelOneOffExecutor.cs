using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.OneOffExecutor")]
    public partial class JenkinsModelOneOffExecutor : JenkinsModelExecutor
    {
        // empty
    }
}
