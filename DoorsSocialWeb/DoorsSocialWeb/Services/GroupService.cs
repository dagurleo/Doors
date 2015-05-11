﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Repositories;
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
        
        public void addUserToGroup(string userId, int groupId)
        {
            groupRepo.addUserToGroup(userId, groupId);
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
    }
}