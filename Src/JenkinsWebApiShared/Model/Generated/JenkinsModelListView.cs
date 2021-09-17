using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.ListView")]
    [XmlRoot("listView")]
    public partial class JenkinsModelListView : JenkinsModelView
    {
        // empty
    }
}
