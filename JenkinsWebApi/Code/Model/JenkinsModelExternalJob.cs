using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.ExternalJob
    [XmlRoot("externalJob")]
    public partial class JenkinsModelExternalJob : JenkinsModelViewJob
    {
        // empty
    }
}
