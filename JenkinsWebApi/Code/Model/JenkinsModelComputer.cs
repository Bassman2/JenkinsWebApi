using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Computer
    public partial class JenkinsModelComputer : JenkinsModelActionable
    {
        [XmlElement("assignedLabel")]
        public JenkinsModelLabelsLabelAtom[] AssignedLabels { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("displayName")]
        public string DisplayName { get; set; }

        [XmlElement("executor")]
        public JenkinsModelExecutor[] Executors { get; set; }

        [XmlElement("icon")]
        public string Icon { get; set; }

        [XmlElement("iconClassName")]
        public string IconClassName { get; set; }

        [XmlElement("idle")]
        public bool IsIdle { get; set; }

        [XmlElement("jnlpAgent")]
        public bool IsJnlpAgent { get; set; }

        [XmlElement("launchSupported")]
        public bool IsLaunchSupported { get; set; }

        [XmlElement("loadStatistics")]
        public JenkinsModelLoadStatistics LoadStatistics { get; set; }

        [XmlElement("manualLaunchAllowed")]
        public bool IsManualLaunchAllowed { get; set; }

        [XmlElement("numExecutors")]
        public int NumExecutors { get; set; }

        [XmlElement("offline")]
        public bool IsOffline { get; set; }

        [XmlElement("offlineCause")]
        public JenkinsSlavesOfflineCause OfflineCause { get; set; }

        [XmlElement("offlineCauseReason")]
        public string OfflineCauseReason { get; set; }

        [XmlElement("oneOffExecutor")]
        public JenkinsModelOneOffExecutor[] OneOffExecutors { get; set; }

        [XmlElement("temporarilyOffline")]
        public bool IsTemporarilyOffline { get; set; }

    }
}
