using Avalonia.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountSwitcher.Models {
    public class Settings {
        public readonly static string DEFAULT_STEAM_PATH = @"C:\Program Files (x86)\Steam\";
        [JsonProperty("SteamPath", Order = 0)] public string SteamPath { get; set; } = DEFAULT_STEAM_PATH;
        [JsonProperty("Version", Order = 1)] public string Version { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        [JsonProperty("CheckUpdates", Order = 2)] public bool CheckUpdates { get; set; } = true;

        public Settings() {
            ReadSettingsFile();
        }

        public void ReadSettingsFile() {
            try {
                string text = File.ReadAllText("SteamAccountSwitcher.json");
                JObject settings = JObject.Parse(text);
                SteamPath = settings[nameof(SteamPath)].ToString();
                CheckUpdates = bool.Parse(settings[nameof(CheckUpdates)].ToString());
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is NullReferenceException || ex is Newtonsoft.Json.JsonReaderException) {
                UpdateSettingsFile();
            }
        }
        public void UpdateSettingsFile() {
            File.WriteAllText("SteamAccountSwitcher.json", ToJSON().ToString());
        }

        public JObject ToJSON() {
            JObject output = (JObject)JToken.FromObject(this);
            return output;
        }
    }
}
