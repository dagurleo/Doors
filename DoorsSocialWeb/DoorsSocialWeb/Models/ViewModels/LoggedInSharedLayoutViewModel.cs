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
        private MessageService messageService = new MessageService();
        private GroupService groupService = new GroupService();
        public ApplicationUser currentUser { get; set; }
        public IEnumerable<Group> groups {get; set; }
        public IEnumerable<ApplicationUser> friends { get; set; }
        public IEnumerable<Message> messages { get; set; }
        public DateTime datetime { get; set; }
        public ApplicationUser getAuthor(string id)
        {
            return userService.getUserById(id);
        }

        public Group getGroupById(int id)
        {
            return groupService.getGroupById(id);
        }

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

        public IEnumerable<groupRequest> getGroupRequestsYouAreOwnerOf()
        {

            return groupService.getGroupRequestsYouAreOwnerOf(currentUser.Id);
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

        public IEnumerable<Message> getFirstMessages(string userID)
        {
            messages = messageService.getFirstMessages(userID);
            return messages;
        }

        public IEnumerable<Topic> getTopics(int groupID)
        {
            return groupService.getTopicsForGroup(groupID);
        }
    }
}