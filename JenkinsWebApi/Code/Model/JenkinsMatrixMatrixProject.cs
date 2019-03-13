using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.matrix.MatrixProject
    [XmlRoot("matrixProject")]
    public partial class JenkinsMatrixMatrixProject : JenkinsModelAbstractProject
    {
        [XmlElement("activeConfiguration")]
        public JenkinsMatrixMatrixConfiguration[] ActiveConfigurations { get; set; }

    }
}
