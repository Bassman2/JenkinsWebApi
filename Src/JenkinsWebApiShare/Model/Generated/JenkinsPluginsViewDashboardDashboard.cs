using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.plugins.view.dashboard.Dashboard")]
    [XmlRoot("dashboard")]
    public partial class JenkinsPluginsViewDashboardDashboard : JenkinsModelListView
    {
        // empty
    }
}
