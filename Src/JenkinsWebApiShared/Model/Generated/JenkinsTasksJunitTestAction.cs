using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.tasks.junit.TestAction")]
    public partial class JenkinsTasksJunitTestAction
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
