using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Job
    public partial class JenkinsJob : JenkinsAbstractItem
    {
        [XmlElement("allBuild")]
        public JenkinsRun[] AllBuilds { get; set; }

        [XmlElement("buildable")]
        public bool IsBuildable { get; set; }

        [XmlElement("build")]
        public JenkinsRun[] Builds { get; set; }

        [XmlElement("color")]
        public JenkinsBallColor Color { get; set; }

        [XmlElement("firstBuild")]
        public JenkinsRun FirstBuild { get; set; }

        [XmlElement("healthReport")]
        public JenkinsHealthReport[] HealthReports { get; set; }

        [XmlElement("inQueue")]
        public bool IsInQueue { get; set; }

        [XmlElement("keepDependencies")]
        public bool IsKeepDependencies { get; set; }

        [XmlElement("lastBuild")]
        public JenkinsRun LastBuild { get; set; }

        [XmlElement("lastCompletedBuild")]
        public JenkinsRun LastCompletedBuild { get; set; }

        [XmlElement("lastFailedBuild")]
        public JenkinsRun LastFailedBuild { get; set; }

        [XmlElement("lastStableBuild")]
        public JenkinsRun LastStableBuild { get; set; }

        [XmlElement("lastSuccessfulBuild")]
        public JenkinsRun LastSuccessfulBuild { get; set; }

        [XmlElement("lastUnstableBuild")]
        public JenkinsRun LastUnstableBuild { get; set; }

        [XmlElement("lastUnsuccessfulBuild")]
        public JenkinsRun LastUnsuccessfulBuild { get; set; }

        [XmlElement("nextBuildNumber")]
        public int NextBuildNumber { get; set; }

        [XmlElement("property")]
        public JenkinsJobProperty[] Propertys { get; set; }

        [XmlElement("queueItem")]
        public JenkinsQueueItem QueueItem { get; set; }

    }
}
