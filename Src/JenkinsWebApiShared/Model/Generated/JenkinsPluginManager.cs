using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.PluginManager")]
    public partial class JenkinsPluginManager
    {
        [JsonPropertyName("plugin")]
        public JenkinsPluginWrapper[] Plugins { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
