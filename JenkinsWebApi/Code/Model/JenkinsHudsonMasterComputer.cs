using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Hudson-MasterComputer
    [XmlRoot("masterComputer")]
    public partial class JenkinsHudsonMasterComputer : JenkinsJenkinsMasterComputer
    {
        // empty
    }
}
