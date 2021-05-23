using Newtonsoft.Json;

namespace JobPlanner.GitJson
{
    public class PermissionsGit
    {
        [JsonProperty("admin")]
        public bool admin { get; set; }

        [JsonProperty("push")]
        public bool Push { get; set; }

        [JsonProperty("pull")]
        public bool Pull { get; set; }
    }
}
