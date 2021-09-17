using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    [SerializableClass("hudson.model.View-AsynchPeople-People")]
    public partial class JenkinsModelViewAsynchPeoplePeople
    {
        [JsonPropertyName("user")]
        public JenkinsModelViewUserInfo[] Users { get; set; }

        /// <summary>
        /// Jenkins Java class name.
        /// </summary>
        [JsonPropertyName("_class")]
        public string Class { get; set; }
    }
}
