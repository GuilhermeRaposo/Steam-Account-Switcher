using System.IO;
using System.Collections.Generic;
using Gameloop.Vdf;
using Gameloop.Vdf.JsonConverter;
using Newtonsoft.Json.Linq;
using System;
using Microsoft.Win32;
using System.Diagnostics;

namespace SteamAccountSwitcher.Models
{
    public class Steam
    {
        public List<Account> Accounts { get; } = new List<Account>();
        private string Path { get; set; }

        public Steam(string path)
        {
            Path = path;
        }

        public void GetSteamAccounts()
        {
            Accounts.Clear();
            var loginUsersVToken = VdfConvert.Deserialize(File.ReadAllText(Path + @"\config\loginusers.vdf"));
            var loginUsers = new JObject() { loginUsersVToken.ToJson() };

            if (loginUsers["users"] != null)
            {
                foreach (var user in loginUsers["users"])
                {
                    string steamId = user.ToObject<JProperty>()?.Name;

                    Accounts.Add(new Account()
                    {
                        SteamID = steamId,
                        AccountName = user.First["AccountName"].ToString(),
                        PersonaName = user.First["PersonaName"].ToString(),
                        WantsOfflineMode = user.First["WantsOfflineMode"].ToString(),
                        SkipOfflineModeWarning = user.First["SkipOfflineModeWarning"].ToString(),
                        MostRecent = user.First["MostRecent"].ToString(),
                        Timestamp = user.First["Timestamp"].ToString()
                    });
                }
            }
        }

        public void UpdateLoginUsers(string selectedSteamId)
        {
            // Update "loginusers.vdf"
            JObject output = new JObject();
            foreach (Account account in Accounts)
            {
                if (account.SteamID == selectedSteamId)
                {
                    account.MostRecent = "1";
                }
                output[account.SteamID] = (JObject)JToken.FromObject(account);
            }

            File.WriteAllText(Path + @"\config\loginusers.vdf", "\"users\"" + Environment.NewLine + output.ToVdf());
        }

        public void UpdateRegistry(string selectedAccountName)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Valve\Steam");
            key.SetValue("RememberPassword", 1);
            key.SetValue("AutoLoginUser", selectedAccountName);
        }

        public void KillSteam()
        {
            Process[] procs = Process.GetProcessesByName("steam");
            foreach (Process proc in procs)
            {
                proc.Kill();
            }
        }

        public void RestartSteam()
        {
            Process.Start(Path + @"\Steam.exe");
        }
    }
}
