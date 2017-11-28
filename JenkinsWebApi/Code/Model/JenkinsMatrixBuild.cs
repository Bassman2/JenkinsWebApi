using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.matrix.MatrixBuild
    [XmlRoot("matrixBuild")]
    public partial class JenkinsMatrixBuild : JenkinsAbstractBuild
    {
        [XmlElement("run")]
        public JenkinsMatrixRun[] Runs { get; set; }

    }
}
