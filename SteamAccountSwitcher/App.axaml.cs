using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SteamAccountSwitcher.Models;
using SteamAccountSwitcher.ViewModels;
using SteamAccountSwitcher.Views;

namespace SteamAccountSwitcher
{
    public partial class App : Application {
        public override void Initialize() {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted() {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
                Settings settings = new Settings();
                settings.ReadSettingsFile();
                Steam steam = new Steam(settings.SteamPath);
                desktop.MainWindow = new MainWindow {
                    DataContext = new MainWindowViewModel(steam),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
