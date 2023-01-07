﻿using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace SteamAccountSwitcher.ViewModels {
    public class SettingsViewModel : ViewModelBase {
        OptionsViewModel Options { get; }
        public SettingsViewModel() {
            Options = new OptionsViewModel();
        }
    }
}
