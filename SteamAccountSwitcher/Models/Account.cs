using Newtonsoft.Json;

namespace SteamAccountSwitcher.Models {
    public class Account {
        [JsonIgnore] public string SteamID { get; set; }
        public string AccountName { get; set; }
        [JsonProperty("PersonaName", Order = 0)] public string PersonaName { get; set; }
        [JsonProperty("RememberPassword", Order = 1)] public string RememberPassword = "1";
        [JsonProperty("WantsOfflineMode", Order = 2)] public string WantsOfflineMode { get; set; }
        [JsonProperty("SkipOfflineModeWarning", Order = 3)] public string SkipOfflineModeWarning { get; set; }
        [JsonProperty("AllowAutoLogin", Order = 4)] public string AllowAutoLogin = "1";
        [JsonProperty("MostRecent", Order = 5)] public string MostRecent { get; set; }
        [JsonProperty("Timestamp", Order = 6)] public string Timestamp { get; set; }

        public override string ToString() {
            return $"{SteamID}";
        }
    }
}
