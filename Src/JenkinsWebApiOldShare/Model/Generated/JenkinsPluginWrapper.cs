using JenkinsWebApi.Internal;
using System.Xml.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.PluginWrapper")]
    public partial class JenkinsPluginWrapper
    {
        [XmlElement("active")]
        public bool IsActive { get; set; }

        [XmlElement("backupVersion")]
        public string BackupVersion { get; set; }

        [XmlElement("bundled")]
        public bool IsBundled { get; set; }

        [XmlElement("deleted")]
        public bool IsDeleted { get; set; }

        [XmlElement("dependency")]
        public JenkinsPluginWrapperDependency[] Dependencys { get; set; }

        [XmlElement("detached")]
        public bool IsDetached { get; set; }

        [XmlElement("downgradable")]
        public bool IsDowngradable { get; set; }

        [XmlElement("enabled")]
        public bool IsEnabled { get; set; }

        [XmlElement("hasUpdate")]
        public bool IsHasUpdate { get; set; }

        [XmlElement("longName")]
        public string LongName { get; set; }

        [XmlElement("minimumJavaVersion")]
        public string MinimumJavaVersion { get; set; }

        [XmlElement("pinned")]
        public bool IsPinned { get; set; }

        [XmlElement("requiredCoreVersion")]
        public string RequiredCoreVersion { get; set; }

        [XmlElement("shortName")]
        public string ShortName { get; set; }

        [XmlElement("supportsDynamicLoad")]
        public JenkinsYesNoMaybe SupportsDynamicLoad { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [XmlAttribute("_class")]
        public string Class { get; set; }
    }
}
