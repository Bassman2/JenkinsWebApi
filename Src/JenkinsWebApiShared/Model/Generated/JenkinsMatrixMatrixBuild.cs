using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.matrix.MatrixBuild")]
    public partial class JenkinsMatrixMatrixBuild : JenkinsModelAbstractBuild
    {
        [JsonPropertyName("run")]
        public JenkinsMatrixMatrixRun[] Runs { get; set; }

    }
}
