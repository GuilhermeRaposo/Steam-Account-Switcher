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
        public readonly static string DEFAULT_STEAM_PATH = @"C:\Program Files (x86)\Steam\";
        [JsonProperty("SteamPath", Order = 0)] public string SteamPath { get; set; }
        [JsonProperty("Version", Order = 1)] public string Version { get; set; }

        public Settings() {
            ReadSettingsFile();
        }

        public void ReadSettingsFile() {
            try {
                string text = File.ReadAllText("SteamAccountSwitcher.json");
                JObject settings = JObject.Parse(text);
                SteamPath = settings[nameof(SteamPath)].ToString();
            } catch (FileNotFoundException) {
                SteamPath = DEFAULT_STEAM_PATH;
                UpdateSettingsFile();
            }
        }
        public void UpdateSettingsFile() {
            File.WriteAllText("SteamAccountSwitcher.json", toJSON().ToString());
        }

        public JObject toJSON() {
            JObject output = (JObject)JToken.FromObject(this);
            return output;
        }
    }
}
