using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.tasks.test.MetaTabulatedResult")]
    public partial class JenkinsTasksTestMetaTabulatedResult : JenkinsTasksTestTabulatedResult
    {
        // empty
    }
}
