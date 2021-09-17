using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.ProxyView")]
    public partial class JenkinsModelProxyView : JenkinsModelView
    {
        // empty
    }
}
