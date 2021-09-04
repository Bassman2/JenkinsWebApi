using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // hudson.scm.RepositoryBrowser
    public partial class JenkinsScmRepositoryBrowser
    {
        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
