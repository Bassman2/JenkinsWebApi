using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.scm.ChangeLogSet")]
    public partial class JenkinsScmChangeLogSet
    {
        [JsonPropertyName("item")]
        public object[] Items { get; set; }

        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
