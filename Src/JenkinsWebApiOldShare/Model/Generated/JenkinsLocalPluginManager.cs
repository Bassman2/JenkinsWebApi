using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.LocalPluginManager")]
    [XmlRoot("localPluginManager")]
    public partial class JenkinsLocalPluginManager : JenkinsPluginManager
    {
        // empty
    }
}
