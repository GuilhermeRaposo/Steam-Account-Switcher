using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using SteamAccountSwitcher.ViewModels;
using SteamAccountSwitcher.Views;
using System.Threading.Tasks;

namespace SteamAccountSwitcher.Views {
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel> {
        public MainWindow() {
            InitializeComponent();
            this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }
        public async Task DoShowDialogAsync(InteractionContext<SettingsViewModel, OptionsViewModel> interaction) {
            SettingsWindow dialog = new SettingsWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<OptionsViewModel>(this);
            interaction.SetOutput(result);
        }
    }
}
