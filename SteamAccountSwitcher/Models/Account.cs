using Newtonsoft.Json;

namespace SteamAccountSwitcher.Models {
    public class Account {
        [JsonIgnore] public string SteamID { get; set; }
        [JsonProperty("AccountName", Order = 0)] public string AccountName { get; set; }
        [JsonProperty("PersonaName", Order = 1)] public string PersonaName { get; set; }
        [JsonProperty("RememberPassword", Order = 1)] public string RememberPassword = "1";
        [JsonProperty("WantsOfflineMode", Order = 3)] public string WantsOfflineMode { get; set; }
        //[JsonProperty("SkipOfflineModeWarning", Order = 4)] public string SkipOfflineModeWarning { get; set; }
        //[JsonProperty("AllowAutoLogin", Order = 5)] public string AllowAutoLogin = "1";
        [JsonProperty("MostRecent", Order = 6)] public string MostRecent { get; set; }
        [JsonProperty("Timestamp", Order = 7)] public string Timestamp { get; set; }
        [JsonIgnore] public bool IsLoggedin { get; set; }
    }
}
