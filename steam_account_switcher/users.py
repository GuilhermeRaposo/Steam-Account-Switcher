import vdf

def load_users():
    return vdf.load(open("C:\Program Files (x86)\Steam\config\loginusers.vdf"))["users"]

def get_usernames(users):
    usernames = []
    for key, value in users.items():
        usernames.append(value["AccountName"])
    return usernames
    
def get_current_logged_userid(users):
    for userid, user in users.items():
        if user["mostrecent"] == "1":
            return userid

# Sets the "mostrecent" field to false on the account that is about to get logged off
# and to true on the account that is about to get logged in
# also makes sure "RememberPassword" is set to true 
def set_current_logged_user(users, old_userid, new_userid): 
    print(old_userid)
    print(new_userid)
    for userid, user in users.items():
        if userid == old_userid:
            user["mostrecent"] = "0"
            user["RememberPassword"] = "1"
        if userid == new_userid:
            user["mostrecent"] = "1"
            user["RememberPassword"] = "1"

def get_userid_from_username(users, username): 
    for userid, user in users.items():
        if user["AccountName"] == username:
            return userid