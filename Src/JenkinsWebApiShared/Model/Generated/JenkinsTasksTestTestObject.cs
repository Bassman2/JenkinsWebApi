using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.test.TestObject
    public partial class JenkinsTasksTestTestObject : JenkinsTasksJunitTestObject
    {
        [JsonPropertyName("testAction")]
        public JenkinsTasksJunitTestAction[] TestActions { get; set; }

    }
}
