using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Interactivity;
using SteamAccountSwitcher.Models;
using SteamAccountSwitcher.Services;

namespace SteamAccountSwitcher.ViewModels {
    public class AccountsViewModel : ViewModelBase {
        public AccountsViewModel(Steam steam) {
            steam.GetSteamAccounts();
            SteamInstance = steam;
            Items = new ObservableCollection<Account>(steam.Accounts);
        }

        private Steam SteamInstance { get; }

        public ObservableCollection<Account> Items { get; }

        public Account SelectedAccount { get; set; }

        public void OnClickCommand() {
            SteamInstance.UpdateLoginUsers(SelectedAccount.SteamID);
            SteamInstance.UpdateRegistry(SelectedAccount.AccountName);
            SteamInstance.KillSteam();
            SteamInstance.RestartSteam();
        }
    }
}