using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // org.jenkinsci.plugins.categorizedview.CategorizedJobsView
    [XmlRoot("categorizedJobsView")]
    public partial class JenkinsCategorizedJobsView : JenkinsListView
    {
        // empty
    }
}
