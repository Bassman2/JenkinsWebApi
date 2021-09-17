using JenkinsWebApi.Internal;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    [SerializableClass("com.cloudbees.hudson.plugins.folder.AbstractFolder")]
    public partial class JenkinsCloudbeesAbstractFolder : JenkinsModelAbstractItem
    {
        [JsonPropertyName("healthReport")]
        public JenkinsModelHealthReport[] HealthReports { get; set; }

        [JsonPropertyName("job")]
        public JenkinsModelJob[] Jobs { get; set; }

        [JsonPropertyName("primaryView")]
        public JenkinsModelView PrimaryView { get; set; }

        [JsonPropertyName("view")]
        public JenkinsModelView[] Views { get; set; }

    }
}
