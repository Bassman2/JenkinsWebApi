using System.Text.Json.Serialization;

namespace JenkinsWebApi.Model
{
    // com.cloudbees.hudson.plugins.folder.AbstractFolder
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
