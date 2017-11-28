using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.matrix.MatrixProject
    [XmlRoot("matrixProject")]
    public partial class JenkinsMatrixProject : JenkinsAbstractProject
    {
        [XmlElement("activeConfiguration")]
        public JenkinsMatrixConfiguration[] ActiveConfigurations { get; set; }

    }
}
