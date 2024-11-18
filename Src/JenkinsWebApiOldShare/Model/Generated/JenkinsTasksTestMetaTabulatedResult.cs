using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.tasks.test.MetaTabulatedResult")]
    public partial class JenkinsTasksTestMetaTabulatedResult : JenkinsTasksTestTabulatedResult
    {
        // empty
    }
}
