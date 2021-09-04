using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.TestObject
    public partial class JenkinsTasksJunitTestObject
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
