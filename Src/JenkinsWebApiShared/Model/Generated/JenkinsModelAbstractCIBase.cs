using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.AbstractCIBase")]
    public partial class JenkinsModelAbstractCIBase : JenkinsModelNode
    {
        // empty
    }
}
