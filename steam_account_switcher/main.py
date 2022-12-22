from pick import pick
from users import *

users = load_users()
user_names = get_usernames(users)

title = "Please choose the account you want to login with: "
options = user_names

selected_username, index = pick(options, title, indicator='=>', default_index=0)

selected_userid = get_userid_from_username(users, selected_username)

current_userid = get_current_logged_userid(users)
set_current_logged_user(users, current_userid, selected_userid)
print(users)