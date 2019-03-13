using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.test.TestObject
    public partial class JenkinsTasksTestTestObject : JenkinsTasksJunitTestObject
    {
        [XmlElement("testAction")]
        public JenkinsTasksJunitTestAction[] TestActions { get; set; }

    }
}
