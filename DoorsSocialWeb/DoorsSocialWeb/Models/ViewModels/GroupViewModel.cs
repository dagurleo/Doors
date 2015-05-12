using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Services;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Models.ViewModels
{
    public class GroupViewModel : LoggedInSharedLayoutViewModel
    {
        public Group currentGroup { get; set; }
        public GroupService groupService = new GroupService();
        public UserService userService = new UserService();
        public IEnumerable<Post> groupPosts { get; set; }
        public IEnumerable<Topic> currentGroupTopics { get; set; }

        public bool userIsMember(string userID)
        {
            if(userService.getMembersOfCurrentGroup(currentGroup.ID).Contains(userService.getUserById(userID)))
            {
                return true;
            }
            return false;
        }

        /*
        public bool userIsPendingMember(string userID)
        {
            if(groupService.getGroupRequests(currentGroup.ID).Contains(userService.getUserById(userID)))
            {
                return true;
            }
            return false;
        }
        */
    }
}