using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.matrix.MatrixProject
    public partial class JenkinsMatrixMatrixProject : JenkinsModelAbstractProject
    {
        [JsonPropertyName("activeConfiguration")]
        public JenkinsMatrixMatrixConfiguration[] ActiveConfigurations { get; set; }

    }
}
