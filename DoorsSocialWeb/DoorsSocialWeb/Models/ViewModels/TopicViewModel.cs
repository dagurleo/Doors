using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Services;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Models.ViewModels
{
    public class TopicViewModel : LoggedInSharedLayoutViewModel
    {        
        public Group currentGroup { get; set; }
        public Topic currentTopic { get; set; }
        public UserService userService = new UserService();
        public IEnumerable<Topic> topics { get; set; }
        public IEnumerable<Post> posts { get; set; }

        public IEnumerable<ApplicationUser> getGroupMembersByGroupId(int groupID)
        {
            return userService.getMembersOfCurrentGroup(groupID);
        }

    }
}