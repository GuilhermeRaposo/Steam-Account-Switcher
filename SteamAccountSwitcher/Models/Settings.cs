using Avalonia.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace SteamAccountSwitcher.Models {
    public class Settings {
        public readonly static string DEFAULT_STEAM_PATH = @"C:\Program Files (x86)\Steam\";
        private readonly string SETTINGS_PATH = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Steam Account Switcher",
            "settings.json"
        );
        [JsonProperty("SteamPath", Order = 0)] public string SteamPath { get; set; } = DEFAULT_STEAM_PATH;
        [JsonProperty("Version", Order = 1)] public string Version { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        [JsonProperty("CheckUpdates", Order = 2)] public bool CheckUpdates { get; set; } = true;

        public Settings() {
            ReadSettingsFile();
            Console.WriteLine(SETTINGS_PATH);
        }

        public void ReadSettingsFile() {
            try {
                string text = File.ReadAllText(SETTINGS_PATH);
                JObject settings = JObject.Parse(text);
                SteamPath = settings[nameof(SteamPath)].ToString();
                CheckUpdates = bool.Parse(settings[nameof(CheckUpdates)].ToString());
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is NullReferenceException || ex is Newtonsoft.Json.JsonReaderException) {
                UpdateSettingsFile();
            }
            catch(Exception ex) when (ex is DirectoryNotFoundException || ex is UnauthorizedAccessException) {
                string path = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Steam Account Switcher"
                );
                System.IO.Directory.CreateDirectory(path);
            }
        }
        public void UpdateSettingsFile() {
            File.WriteAllText(SETTINGS_PATH, ToJSON().ToString());
        }

        public JObject ToJSON() {
            JObject output = (JObject)JToken.FromObject(this);
            return output;
        }
    }
}
