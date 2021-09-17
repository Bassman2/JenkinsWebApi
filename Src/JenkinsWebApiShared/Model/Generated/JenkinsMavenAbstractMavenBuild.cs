using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.maven.AbstractMavenBuild")]
    public partial class JenkinsMavenAbstractMavenBuild : JenkinsModelAbstractBuild
    {
        // empty
    }
}
