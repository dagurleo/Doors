using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Repositories;
using DoorsSocialWeb.Models;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Services
{
    public class GroupService
    {
        private GroupRepository groupRepo = new GroupRepository();
        //TODO: Implement functions to deliver groups AND/OR their topics to the group/newsfeed views
        public IEnumerable<Group> getAccessibleGroups()
        {
            return groupRepo.getAccessibleGroups();
        }

        public void addNewGroup(Group group)
        {
            groupRepo.addNewGroup(group);
        }
        
        public Group getCurrentGroup(int id)
        {
            return groupRepo.getCurrentGroup(id);
        }

        public Group getGroupById(int id)
        {
            return groupRepo.getGroupById(id);
        }

        public void addUserToGroup(string userId, int groupId)
        {
            groupRepo.addUserToGroup(userId, groupId);
        }

        public void userApprovesGroupRequest(int requestId)
        {
            groupRepo.userApprovesGroupRequest(requestId);
        }

        public void userDeclinesGroupRequest(int requestId)
        {
            groupRepo.userDeclinesGroupRequest(requestId);
        }

        public Group getNewestGroup()
        {
            return groupRepo.getNewestGroup();
        }

        public void editGroup(Group thisGroup)
        {
            //TODO: Edit thisGroup and save.
            return;
        }

        public IEnumerable<Group> searchGroupsByName(string searchTerm)
        {
            return groupRepo.searchGroupsByName(searchTerm);
        }       


        public IEnumerable<groupRequest> getGroupRequests(int groupID)
        {
            return groupRepo.getGroupRequests(groupID);
        }

        public void sendGroupRequest(string requestUserId, int groupId, string groupOwner)
        {
            groupRepo.sendGroupRequest(requestUserId, groupId, groupOwner);
        }

     
        public IEnumerable<Topic> getTopicsForGroup(int groupId)
        {
            return groupRepo.getTopicsForGroup(groupId);
        }

        public IEnumerable<groupRequest> getGroupRequestsYouAreOwnerOf(string userId)
        {
            return groupRepo.getGroupRequestsYouAreOwnerOf(userId);
        }
    }
}