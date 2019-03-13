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
        public JenkinsBallColor[] Colors { get; set; }

        [XmlElement("firstBuild")]
        public JenkinsRun[] FirstBuilds { get; set; }

        [XmlElement("healthReport")]
        public JenkinsHealthReport[] HealthReports { get; set; }

        [XmlElement("inQueue")]
        public bool IsInQueue { get; set; }

        [XmlElement("keepDependencies")]
        public bool IsKeepDependencies { get; set; }

        [XmlElement("lastBuild")]
        public JenkinsRun[] LastBuilds { get; set; }

        [XmlElement("lastCompletedBuild")]
        public JenkinsRun[] LastCompletedBuilds { get; set; }

        [XmlElement("lastFailedBuild")]
        public JenkinsRun[] LastFailedBuilds { get; set; }

        [XmlElement("lastStableBuild")]
        public JenkinsRun[] LastStableBuilds { get; set; }

        [XmlElement("lastSuccessfulBuild")]
        public JenkinsRun[] LastSuccessfulBuilds { get; set; }

        [XmlElement("lastUnstableBuild")]
        public JenkinsRun[] LastUnstableBuilds { get; set; }

        [XmlElement("lastUnsuccessfulBuild")]
        public JenkinsRun[] LastUnsuccessfulBuilds { get; set; }

        [XmlElement("nextBuildNumber")]
        public int NextBuildNumber { get; set; }

        [XmlElement("property")]
        public JenkinsJobProperty[] Propertys { get; set; }

        [XmlElement("queueItem")]
        public JenkinsQueueItem[] QueueItems { get; set; }

    }
}
