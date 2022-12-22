import winreg

def edit_registry(user_name):
    with winreg.ConnectRegistry(None, winreg.HKEY_CURRENT_USER) as hkey:
        with winreg.OpenKey(hkey, "Software", 0, winreg.KEY_ALL_ACCESS) as software:
            with winreg.OpenKey(software, "Valve", 0, winreg.KEY_ALL_ACCESS) as valve:
                with winreg.OpenKey(valve, "Steam", 0, winreg.KEY_ALL_ACCESS) as steam:
                    winreg.SetValueEx(steam, "RememberPassword", 0, winreg.REG_DWORD, 1)
                    winreg.SetValueEx(steam, "AutoLoginUser", 0, winreg.REG_SZ, user_name)