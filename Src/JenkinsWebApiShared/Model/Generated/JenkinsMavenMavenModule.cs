using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.MavenModule")]
    public partial class JenkinsMavenMavenModule : JenkinsMavenAbstractMavenProject
    {
    }
}
