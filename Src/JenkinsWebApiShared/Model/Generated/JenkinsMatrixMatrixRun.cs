using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.matrix.MatrixRun")]
    public partial class JenkinsMatrixMatrixRun : JenkinsModelBuild
    {
        // empty
    }
}
