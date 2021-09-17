using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.tasks.test.TestResult")]
    public partial class JenkinsTasksTestTestResult : JenkinsTasksTestTestObject
    {
        // empty
    }
}
