import winreg

def get_steam_path():
    with winreg.ConnectRegistry(None, winreg.HKEY_CURRENT_USER) as hkey:
        with winreg.OpenKey(hkey, "Software", 0, winreg.KEY_ALL_ACCESS) as software:
            with winreg.OpenKey(software, "Valve", 0, winreg.KEY_ALL_ACCESS) as valve:
                with winreg.OpenKey(valve, "Steam", 0, winreg.KEY_ALL_ACCESS) as steam:
                    return winreg.QueryValueEx(steam, "SteamPath")[0]