using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.AbstractMavenProject")]
    public partial class JenkinsMavenAbstractMavenProject : JenkinsModelAbstractProject
    {
        // empty
    }
}
