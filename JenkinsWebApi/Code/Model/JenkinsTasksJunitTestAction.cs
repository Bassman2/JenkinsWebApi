using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.tasks.junit.TestAction
    public partial class JenkinsTasksJunitTestAction
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
