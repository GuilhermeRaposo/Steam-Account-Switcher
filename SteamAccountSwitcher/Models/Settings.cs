using Avalonia.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountSwitcher.Models {
    public class Settings {
        [JsonProperty("Path", Order = 0)] public string Path { get; set; }

        public JObject toJSON() {
            JObject output = (JObject)JToken.FromObject(this);
            return output;
        }

        public void updateSettingsFile() {
            File.WriteAllText("/SteamAccountSwitcherSettings.json", toJSON().ToString());
        }
    }
}
