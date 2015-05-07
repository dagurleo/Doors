using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Repositories
{
    public class GroupRepository
    {
        //TODO: implement more functions we might need and connect to db;
        public List<Group> getGroupsByID(int groupID)
        {
            //TODO: Fill listOfGroup with groups that ID, not sure what the variable should be named
            List<Group> listOfGroup = new List<Group>();
            return listOfGroup;
        }

        public void addNewGroup(Group newGroup)
        {
            //TODO: Create a new group and save it
            return;
        }

        public void editGroup(Group thisGroup)
        {
            //TODO: Edit thisGroup and save.
            return;
        }
    }
}