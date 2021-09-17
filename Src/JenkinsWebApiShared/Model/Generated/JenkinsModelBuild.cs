using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Build")]
    public partial class JenkinsModelBuild : JenkinsModelAbstractBuild
    {
        // empty
    }
}
