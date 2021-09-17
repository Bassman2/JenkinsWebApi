using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.ListView")]
    public partial class JenkinsModelListView : JenkinsModelView
    {
        // empty
    }
}
