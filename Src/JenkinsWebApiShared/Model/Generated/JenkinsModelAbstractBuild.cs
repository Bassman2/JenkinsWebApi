using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.AbstractBuild")]
    public partial class JenkinsModelAbstractBuild : JenkinsModelRun
    {
        [JsonPropertyName("builtOn")]
        public string BuiltOn { get; set; }

        [JsonPropertyName("changeSet")]
        public JenkinsScmChangeLogSet ChangeSet { get; set; }

        [JsonPropertyName("culprit")]
        public JenkinsModelUser[] Culprits { get; set; }

    }
}
