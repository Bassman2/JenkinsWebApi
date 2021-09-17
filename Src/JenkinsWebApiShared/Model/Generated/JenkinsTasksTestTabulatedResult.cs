using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.tasks.test.TabulatedResult")]
    public partial class JenkinsTasksTestTabulatedResult : JenkinsTasksTestTestResult
    {
        // empty
    }
}
