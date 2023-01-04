using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Interactivity;
using SteamAccountSwitcher.Models;
using SteamAccountSwitcher.Services;

namespace SteamAccountSwitcher.ViewModels {
    public class AccountsViewModel : ViewModelBase {
        private Steam SteamInstance { get; }

        public ObservableCollection<Account> Accounts { get; set; }

        public Account SelectedAccount { get; set; }

        public AccountsViewModel(Steam steam) {
            SteamInstance = steam;
            SteamInstance.GetSteamAccounts();
            Accounts = new ObservableCollection<Account>(SteamInstance.Accounts);
        }

        public void Login() {
            SteamInstance.UpdateLoginUsers(SelectedAccount.SteamID);
            SteamInstance.UpdateRegistry(SelectedAccount.AccountName);
            SteamInstance.KillSteam();
            SteamInstance.RestartSteam();
        }

        public void ReloadAccounts() {
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