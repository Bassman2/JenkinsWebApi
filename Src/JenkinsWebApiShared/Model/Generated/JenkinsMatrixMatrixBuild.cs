using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.matrix.MatrixBuild
    public partial class JenkinsMatrixMatrixBuild : JenkinsModelAbstractBuild
    {
        [JsonPropertyName("run")]
        public JenkinsMatrixMatrixRun[] Runs { get; set; }

    }
}
