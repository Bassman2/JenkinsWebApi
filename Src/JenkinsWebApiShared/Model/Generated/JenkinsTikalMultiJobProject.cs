using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.tikal.jenkins.plugins.multijob.MultiJobProject")]
    [XmlRoot("multiJobProject")]
    public partial class JenkinsTikalMultiJobProject : JenkinsModelProject
    {
        // empty
    }
}
