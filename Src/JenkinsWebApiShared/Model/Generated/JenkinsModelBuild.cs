using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Build")]
    public partial class JenkinsModelBuild : JenkinsModelAbstractBuild
    {
        // empty
    }
}
