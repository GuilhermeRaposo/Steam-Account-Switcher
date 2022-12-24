import os
from pick import pick
import psutil
from users import *
from registry import edit_registry
from steam import get_steam_path

# Load users and usernames
users = load_users()
user_names = get_usernames(users)

# Show CLI options
title = "Please choose the account you want to login with: "
options = user_names
selected_username, index = pick(options, title, indicator='=>', default_index=0)

selected_userid = get_userid_from_username(users, selected_username)
current_userid = get_current_logged_userid(users)

# Edit the vdf file containing user info
set_current_logged_user(users, current_userid, selected_userid)
write_users(users)

edit_registry(selected_username)

# Kill steam process
for proc in psutil.process_iter():
    PROCNAME = "steam.exe"
    if proc.name() == PROCNAME:
        proc.kill()

# Restart steam process
steam_path = get_steam_path()
os.startfile(steam_path + "/steam.exe")