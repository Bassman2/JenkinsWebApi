using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("org.jenkinsci.plugins.envinject.EnvInjectVarList")]
    public partial class JenkinsJenkinsciEnvInjectVarList
    {
        [JsonPropertyName("envMap")]
        public object EnvMap { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
