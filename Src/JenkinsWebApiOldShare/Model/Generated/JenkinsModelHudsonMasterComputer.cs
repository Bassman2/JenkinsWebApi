using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Hudson-MasterComputer")]
    [XmlRoot("masterComputer")]
    public partial class JenkinsModelHudsonMasterComputer : JenkinsModelJenkinsMasterComputer
    {
        // empty
    }
}
