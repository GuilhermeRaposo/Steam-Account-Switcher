using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace SteamAccountSwitcher.Views {
    public partial class AccountsView : UserControl {
        private string Selected { get; set; }

        public AccountsView() {
            InitializeComponent();
        }

        public void AccountSelected(object sender, RoutedEventArgs e) {
            Selected = e.Source.ToString();
            Console.WriteLine(123);
        }
    }
}
