using SteamAccountSwitcher.Services;

namespace SteamAccountSwitcher.ViewModels {
    public class MainWindowViewModel : ViewModelBase {
        public MainWindowViewModel(Steam steam) {
            List = new AccountsViewModel(steam);
        }

        public AccountsViewModel List { get; }
    }
}
