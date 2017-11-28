using System.Xml.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.model.UpdateSite-Plugin
    public partial class JenkinsUpdateSitePlugin : JenkinsUpdateSiteEntry
    {
        [XmlElement("category")]
        public string[] Categorys { get; set; }

        [XmlElement("compatibleSinceVersion")]
        public string CompatibleSinceVersion { get; set; }

        [XmlElement("compatibleWithInstalledVersion")]
        public bool IsCompatibleWithInstalledVersion { get; set; }

        [XmlElement("dependencies")]
        public object Dependencies { get; set; }

        [XmlElement("excerpt")]
        public string Excerpt { get; set; }

        [XmlElement("installed")]
        public JenkinsPluginWrapper Installed { get; set; }

        [XmlElement("neededDependency")]
        public JenkinsUpdateSitePlugin[] NeededDependencys { get; set; }

        [XmlElement("optionalDependencies")]
        public object OptionalDependencies { get; set; }

        [XmlElement("requiredCore")]
        public string RequiredCore { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("wiki")]
        public string Wiki { get; set; }

    }
}
