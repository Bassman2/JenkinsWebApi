using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.matrix.MatrixBuild
    [XmlRoot("matrixBuild")]
    public partial class JenkinsMatrixMatrixBuild : JenkinsModelAbstractBuild
    {
        [XmlElement("run")]
        public JenkinsMatrixMatrixRun[] Runs { get; set; }

    }
}
