using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SteamAccountSwitcher.Services;
using SteamAccountSwitcher.ViewModels;
using SteamAccountSwitcher.Views;

namespace SteamAccountSwitcher {
    public partial class App : Application {
        public override void Initialize() {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted() {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
                Steam steam = new Steam(@"C:\Program Files (x86)\Steam");
                desktop.MainWindow = new MainWindow {
                    DataContext = new MainWindowViewModel(steam),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
