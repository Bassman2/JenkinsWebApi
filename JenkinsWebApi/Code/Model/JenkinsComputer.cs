using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.Computer
    public partial class JenkinsComputer : JenkinsActionable
    {
        [XmlElement("assignedLabel")]
        public JenkinsLabelAtom[] AssignedLabels { get; set; }

        [XmlElement("description")]
        public string[] Descriptions { get; set; }

        [XmlElement("displayName")]
        public string[] DisplayNames { get; set; }

        [XmlElement("executor")]
        public JenkinsExecutor[] Executors { get; set; }

        [XmlElement("icon")]
        public string[] Icons { get; set; }

        [XmlElement("iconClassName")]
        public string[] IconClassNames { get; set; }

        [XmlElement("idle")]
        public bool IsIdle { get; set; }

        [XmlElement("jnlpAgent")]
        public bool IsJnlpAgent { get; set; }

        [XmlElement("launchSupported")]
        public bool IsLaunchSupported { get; set; }

        [XmlElement("loadStatistics")]
        public JenkinsLoadStatistics[] LoadStatisticss { get; set; }

        [XmlElement("manualLaunchAllowed")]
        public bool IsManualLaunchAllowed { get; set; }

        [XmlElement("numExecutors")]
        public int NumExecutors { get; set; }

        [XmlElement("offline")]
        public bool IsOffline { get; set; }

        [XmlElement("offlineCause")]
        public JenkinsOfflineCause[] OfflineCauses { get; set; }

        [XmlElement("offlineCauseReason")]
        public string[] OfflineCauseReasons { get; set; }

        [XmlElement("oneOffExecutor")]
        public JenkinsOneOffExecutor[] OneOffExecutors { get; set; }

        [XmlElement("temporarilyOffline")]
        public bool IsTemporarilyOffline { get; set; }

    }
}
