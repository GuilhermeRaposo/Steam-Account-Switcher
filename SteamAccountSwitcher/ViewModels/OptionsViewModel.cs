using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using MessageBox.Avalonia.DTO;
using ReactiveUI;
using SteamAccountSwitcher.Models;
using System.IO;
using System.Threading.Tasks;

namespace SteamAccountSwitcher.ViewModels {
    public class OptionsViewModel : ViewModelBase {
        private string sucess;
        public string Sucess {
            get => sucess;
            set => this.RaiseAndSetIfChanged(ref sucess, value);
        }

        private string error;
        public string Error {
            get => error;
            set => this.RaiseAndSetIfChanged(ref error, value);
        }
        public Settings SettingsInstance { get; set; } = new Settings();
        public async Task ChooseSteamFolder() {
            OpenFolderDialog dialog = new OpenFolderDialog();
            var settingsWindow = Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.Windows[1] : null;
            var result = await dialog.ShowAsync((settingsWindow));
            if (File.Exists(result + "/steam.exe")) {
                SettingsInstance.SteamPath = result;
                SettingsInstance.updateSettingsFile();
                Sucess = "Sucess";
                Error = "";
            }
            else if (result != null) {
                Error = "steam.exe not found";
                Sucess = "";
            }
        }
    }
}
