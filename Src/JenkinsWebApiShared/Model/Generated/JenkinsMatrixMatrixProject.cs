using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.matrix.MatrixProject")]
    public partial class JenkinsMatrixMatrixProject : JenkinsModelAbstractProject
    {
        [JsonPropertyName("activeConfiguration")]
        public JenkinsMatrixMatrixConfiguration[] ActiveConfigurations { get; set; }

    }
}
