using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.scm.ChangeLogSet")]
    public partial class JenkinsScmChangeLogSet
    {
        [XmlElement("item")]
        public object[] Items { get; set; }

        [XmlElement("kind")]
        public string Kind { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
