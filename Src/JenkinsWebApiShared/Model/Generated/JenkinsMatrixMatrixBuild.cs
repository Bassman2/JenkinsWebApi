using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.matrix.MatrixBuild")]
    public partial class JenkinsMatrixMatrixBuild : JenkinsModelAbstractBuild
    {
        [JsonPropertyName("run")]
        public JenkinsMatrixMatrixRun[] Runs { get; set; }

    }
}
