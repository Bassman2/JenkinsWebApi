using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.ListView")]
    public partial class JenkinsModelListView : JenkinsModelView
    {
        // empty
    }
}
