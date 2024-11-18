using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.MavenModule")]
    public partial class JenkinsMavenMavenModule : JenkinsMavenAbstractMavenProject
    {
    }
}
