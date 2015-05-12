﻿using System;
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


        public bool userIsPendingMember(string userID)
        {
            foreach(var r in pendingRequests(currentGroup.ID))
            {
                if(r.userRequestId == userID)
                {
                    return true;
                }
            }
            return false;
        }

        public bool userIsApprovedMember(string userID)
        {
            foreach(var r in pendingRequests(currentGroup.ID))
            {
                if (r.userRequestId == userID)
                {
                    if(r.userIsApproved == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public IEnumerable<groupRequest> pendingRequests(int groupID)
        {
            return groupService.getGroupRequests(groupID);
        }

        public ApplicationUser getUserByID(string userID)
        {
            return userService.getUserById(userID);
        }
        
    }
}