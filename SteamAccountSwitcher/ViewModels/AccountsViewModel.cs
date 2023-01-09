using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using DynamicData.Kernel;
using SteamAccountSwitcher.Models;

namespace SteamAccountSwitcher.ViewModels
{
    public class AccountsViewModel : ViewModelBase {
        private Settings settings { get; set; }
        private Steam SteamInstance { get; }

        public ObservableCollection<Account> Accounts { get; set; }

        public Account SelectedAccount { get; set; }

        public ICommand OpenSettings { get; }

        public string Error { get; set; }

        public AccountsViewModel(Steam steam, ICommand openSettingsCommand) {
            SteamInstance = steam;
            SteamInstance.GetSteamAccounts();
            settings = new Settings();
            Accounts = new ObservableCollection<Account>(SteamInstance.Accounts);
            OpenSettings = openSettingsCommand;
            CheckVersion();
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
        }

        public void ReloadAccounts() {
            settings.ReadSettingsFile();
            SteamInstance.Path = settings.SteamPath;
            SteamInstance.GetSteamAccounts();
            List<Account> accounts = SteamInstance.Accounts;
            foreach (Account account1 in accounts) {
                if (!Accounts.Any(account2 => account2.SteamID == account1.SteamID)) {
                    Accounts.Add(account1);
                }
            }
        }

        public void AddNewAccount() {
            SteamInstance.UpdateRegistry("");
            SteamInstance.KillSteam();
            SteamInstance.RestartSteam();
        }
    }
}