using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.tikal.jenkins.plugins.multijob.views.MultiJobView")]
    [XmlRoot("multiJobView")]
    public partial class JenkinsTikalMultiJobView : JenkinsModelListView
    {
        // empty
    }
}
