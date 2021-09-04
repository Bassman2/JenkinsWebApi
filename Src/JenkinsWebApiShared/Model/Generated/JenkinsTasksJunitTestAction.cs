using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.TestAction
    public partial class JenkinsTasksJunitTestAction
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
