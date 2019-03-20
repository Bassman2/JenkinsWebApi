using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    // hudson.tasks.test.TestObject
    public partial class JenkinsTasksTestTestObject : JenkinsTasksJunitTestObject
    {
        [XmlElement("testAction")]
        public JenkinsTasksJunitTestAction[] TestActions { get; set; }

    }
}
