using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using SteamAccountSwitcher.Models;

namespace SteamAccountSwitcher.ViewModels
{
    public class AccountsViewModel : ViewModelBase {
        private Settings Settings { get; set; }
        private Steam SteamInstance { get; }

        public ObservableCollection<Account> Accounts { get; set; }

        public Account SelectedAccount { get; set; }

        public ICommand OpenSettings { get; }

        public string Error { get; set; }

        public AccountsViewModel(Steam steam, ICommand openSettingsCommand) {
            SteamInstance = steam;
            SteamInstance.GetSteamAccounts();
            Settings = new Settings();
            Accounts = new ObservableCollection<Account>(SteamInstance.Accounts);
            OpenSettings = openSettingsCommand;
            if (Settings.CheckUpdates) {
                CheckVersion();
            }
        }

        public void CheckVersion() {
            try {
                Version Latestversion = new Version(Releases.GetLastVersionNumber().Result);
                Version AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
                if (Latestversion.CompareTo(AssemblyVersion) == 1) {
                    Error = "*New version available*";
                }
            } catch (Exception e) {
                Trace.WriteLine(e);
            }
        }

        public void Login() {
            SteamInstance.UpdateLoginUsers(SelectedAccount.SteamID);
            SteamInstance.UpdateRegistry(SelectedAccount.AccountName);
            SteamInstance.KillSteam();
            SteamInstance.RestartSteam();
            this.ReloadAccounts();
        }

        public void ReloadAccounts() {
            Settings.ReadSettingsFile();
            SteamInstance.Path = Settings.SteamPath;
            SteamInstance.GetSteamAccounts();
            List<Account> accounts = SteamInstance.Accounts;
            Accounts.Clear();
            foreach (Account account1 in accounts) {
                Accounts.Add(account1);
            }
        }

        public void AddNewAccount() {
            SteamInstance.UpdateRegistry("");
            SteamInstance.KillSteam();
            SteamInstance.RestartSteam();
        }
    }
}