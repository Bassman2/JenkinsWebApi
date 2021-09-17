using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.tasks.test.TestObject")]
    public partial class JenkinsTasksTestTestObject : JenkinsTasksJunitTestObject
    {
        [JsonPropertyName("testAction")]
        public JenkinsTasksJunitTestAction[] TestActions { get; set; }

    }
}
