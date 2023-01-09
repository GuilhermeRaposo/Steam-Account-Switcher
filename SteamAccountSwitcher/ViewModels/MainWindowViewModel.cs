using System;
using ReactiveUI;
using SteamAccountSwitcher.Models;
using System.Reactive.Linq;
using System.Windows.Input;
using System.Reflection;
using MessageBox.Avalonia;
using System.Reactive;

namespace SteamAccountSwitcher.ViewModels
{
    public class MainWindowViewModel : ViewModelBase {
        public AccountsViewModel Steam { get; }
        public ICommand OpenSettings { get; }
        public Interaction<SettingsViewModel, OptionsViewModel> ShowDialog { get; }

        public MainWindowViewModel(Steam steam) {
            ShowDialog = new Interaction<SettingsViewModel, OptionsViewModel>();
            OpenSettings = ReactiveCommand.CreateFromTask(async () => {
                SettingsViewModel settings = new SettingsViewModel();
                var result = await ShowDialog.Handle(settings);
            });
            Steam = new AccountsViewModel(steam, OpenSettings);
        }
    }
}
