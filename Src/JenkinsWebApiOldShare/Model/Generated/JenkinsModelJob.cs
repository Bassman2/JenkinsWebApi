using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.Job")]
    public partial class JenkinsModelJob : JenkinsModelAbstractItem
    {
        [XmlElement("allBuild")]
        public JenkinsModelRun[]? AllBuilds { get; set; }

        [XmlElement("buildable")]
        public bool IsBuildable { get; set; }

        [XmlElement("build")]
        public JenkinsModelRun[]? Builds { get; set; }

        [XmlElement("color")]
        public JenkinsModelBallColor Color { get; set; }

        [XmlElement("firstBuild")]
        public JenkinsModelRun? FirstBuild { get; set; }

        [XmlElement("healthReport")]
        public JenkinsModelHealthReport[]? HealthReports { get; set; }

        [XmlElement("inQueue")]
        public bool IsInQueue { get; set; }

        [XmlElement("keepDependencies")]
        public bool IsKeepDependencies { get; set; }

        [XmlElement("lastBuild")]
        public JenkinsModelRun? LastBuild { get; set; }

        [XmlElement("lastCompletedBuild")]
        public JenkinsModelRun? LastCompletedBuild { get; set; }

        [XmlElement("lastFailedBuild")]
        public JenkinsModelRun? LastFailedBuild { get; set; }

        [XmlElement("lastStableBuild")]
        public JenkinsModelRun? LastStableBuild { get; set; }

        [XmlElement("lastSuccessfulBuild")]
        public JenkinsModelRun? LastSuccessfulBuild { get; set; }

        [XmlElement("lastUnstableBuild")]
        public JenkinsModelRun? LastUnstableBuild { get; set; }

        [XmlElement("lastUnsuccessfulBuild")]
        public JenkinsModelRun? LastUnsuccessfulBuild { get; set; }

        [XmlElement("nextBuildNumber")]
        public int NextBuildNumber { get; set; }

        [XmlElement("property")]
        public JenkinsModelJobProperty[]? Propertys { get; set; }

        [XmlElement("queueItem")]
        public JenkinsModelQueueItem? QueueItem { get; set; }

    }
}
