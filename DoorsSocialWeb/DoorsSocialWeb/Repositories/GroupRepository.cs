using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DoorsSocialWeb.Repositories
{
    public class GroupRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();        

        /*
         * fills a list of group, with all accessible groups for the currently logged in user
         */
        public IEnumerable<Group> getAccessibleGroups()
        {
            //TODO: Fill listOfGroup with groups that ID, not sure what the variable should be named
            ApplicationDbContext db = new ApplicationDbContext();
            string currentUserID = HttpContext.Current.User.Identity.GetUserId();

            var accessibleGroups = from g in db.relGroups
                                   where g.memberID == currentUserID
                                   join gr in db.Groups
                                   on g.groupID equals gr.ID
                                   orderby gr.groupName ascending
                                   select gr;

            return accessibleGroups;
        }

        public Group getCurrentGroup(int id)
        {
            var currGroup = (from g in db.Groups
                            where g.ID == id
                            select g).Single();
            return currGroup;
        }

        public void addNewGroup(Group newGroup)
        {
            db.Groups.Add(newGroup);
            db.SaveChanges();
            
        }
        public void addUserToGroup(string userId, int groupId)
        {
            relGroup relationship = new relGroup { memberID = userId, groupID = groupId };
            db.relGroups.Add(relationship);
            db.SaveChanges();
        }

        public void editGroup(Group thisGroup)
        {
            //TODO: Edit thisGroup and save.
            return;
        }
    }
}