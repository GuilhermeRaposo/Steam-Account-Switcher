using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia.Interactivity;
using SteamAccountSwitcher.Models;

namespace SteamAccountSwitcher.ViewModels
{
    public class AccountsViewModel : ViewModelBase {
        private Settings settings { get; set; }
        private Steam SteamInstance { get; }

        public ObservableCollection<Account> Accounts { get; set; }

        public Account SelectedAccount { get; set; }

        public ICommand OpenSettings { get; }

        public AccountsViewModel(Steam steam, ICommand openSettingsCommand) {
            SteamInstance = steam;
            SteamInstance.GetSteamAccounts();
            settings = new Settings();
            Accounts = new ObservableCollection<Account>(SteamInstance.Accounts);
            OpenSettings = openSettingsCommand;
        }

        public void Login() {
            SteamInstance.UpdateLoginUsers(SelectedAccount.SteamID);
            SteamInstance.UpdateRegistry(SelectedAccount.AccountName);
            SteamInstance.KillSteam();
            SteamInstance.RestartSteam();
        }

        public void ReloadAccounts() {
            settings.readSettingsFile();
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