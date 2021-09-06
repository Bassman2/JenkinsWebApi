using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.model.Hudson-MasterComputer
    [XmlRoot("masterComputer")]
    public partial class JenkinsModelHudsonMasterComputer : JenkinsModelJenkinsMasterComputer
    {
        // empty
    }
}
