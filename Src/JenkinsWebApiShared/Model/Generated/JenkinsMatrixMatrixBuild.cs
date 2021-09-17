using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.matrix.MatrixBuild")]
    [XmlRoot("matrixBuild")]
    public partial class JenkinsMatrixMatrixBuild : JenkinsModelAbstractBuild
    {
        [XmlElement("run")]
        public JenkinsMatrixMatrixRun[] Runs { get; set; }

    }
}
