using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using SteamAccountSwitcher.Models;
using System.Threading.Tasks;

namespace SteamAccountSwitcher.ViewModels {
    public class OptionsViewModel : ViewModelBase {
        public Settings SettingsInstance { get; set; } = new Settings();
        public async Task ChooseSteamFolder() {
            OpenFolderDialog dialog = new OpenFolderDialog();
            var settingsWindow = Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.Windows[1] : null;
            var result = await dialog.ShowAsync((settingsWindow));
            updateSteamFolder(result);
        }

        public void updateSteamFolder(string path) {
             
        }
    }
}
