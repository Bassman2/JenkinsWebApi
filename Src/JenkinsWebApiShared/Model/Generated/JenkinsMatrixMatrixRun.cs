using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.matrix.MatrixRun")]
    public partial class JenkinsMatrixMatrixRun : JenkinsModelBuild
    {
        // empty
    }
}
