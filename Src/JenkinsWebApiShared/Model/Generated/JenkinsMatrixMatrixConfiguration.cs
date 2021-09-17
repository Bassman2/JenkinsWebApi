using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.matrix.MatrixConfiguration")]
    public partial class JenkinsMatrixMatrixConfiguration : JenkinsModelProject
    {
        // empty
    }
}
