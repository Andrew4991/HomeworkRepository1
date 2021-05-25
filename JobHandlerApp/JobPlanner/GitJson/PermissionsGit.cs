using System.Text.Json.Serialization;

namespace JobPlanner.GitJson
{
    public class PermissionsGit
    {
        [JsonPropertyName("admin")]
        public bool admin { get; set; }

        [JsonPropertyName("push")]
        public bool Push { get; set; }

        [JsonPropertyName("pull")]
        public bool Pull { get; set; }
    }
}
