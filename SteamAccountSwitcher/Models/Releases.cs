using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SteamAccountSwitcher.Models {
    public class Releases {
        public static async Task<String> GetLastVersionNumber() {
            using (HttpClient client = new HttpClient()) {
                string url = @"https://api.github.com/";
                string apiEndPoint = @"repos/guilhermeraposo/steam-account-switcher/releases";
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                HttpResponseMessage response = client.GetAsync(apiEndPoint).Result;

                if (response != null) {
                    var jsonstring = await response.Content.ReadAsStringAsync();
                    List<Release> releasesList = JsonConvert.DeserializeObject<List<Release>>(jsonstring);
                    return releasesList[0].tag_name;
                }
                return null;
            }
        }
    }

    public class Release {
        [JsonProperty("tag_name")] public string tag_name { get; set; }
    }
}
