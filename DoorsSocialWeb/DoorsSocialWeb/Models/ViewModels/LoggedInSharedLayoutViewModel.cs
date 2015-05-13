using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Services;


namespace DoorsSocialWeb.Models.ViewModels
{
    public class LoggedInSharedLayoutViewModel
    {
        private UserService userService = new UserService();
        public ApplicationUser currentUser { get; set; }
        public IEnumerable<Group> groups {get; set; }
        public IEnumerable<ApplicationUser> friends { get; set; }
        public DateTime datetime { get; set; }

        public bool userIsFriend(string friendId)
        {
            IEnumerable<ApplicationUser> friends = userService.getFriendsOfCurrentUser();
            if (friends.Contains(userService.getUserById(friendId)))
            {
                return true;
            }
            return false;
        }

        public IEnumerable<friendRequest> getFriendRequests(string userID)
        {
            return userService.getFriendRequests(userID);
        }

        public bool userIsPendingFriend(string friendID, string currentUserId)
        {
            //Sækja öll friend request sem VINUR minn á, tékka hvort ég sé á honum.
            IEnumerable<friendRequest> fRequests = getFriendRequests(friendID);
            if (fRequests != null)
            {
                foreach (var f in fRequests)
                {
                    if (f.userRequestID == currentUserId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool isUserPendingRequest(string friendID)
        {
            return userService.isUserPendingRequest(friendID);
        }
    }
}