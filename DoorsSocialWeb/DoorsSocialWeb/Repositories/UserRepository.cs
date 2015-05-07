using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models;

namespace DoorsSocialWeb.Repositories
{
    public class UserRepository
    {
        //TODO: implement more functions we might need and connect to db;
        public List<ApplicationUser> getUsersByID(int UserID)
        {
            //TODO: return a list<User> or a User by that ID? NOT SURE
            List<ApplicationUser> listOfUser = new List<ApplicationUser>();
            return listOfUser;
        }

        public void addNewUser(ApplicationUser newUser)
        {
            //TODO: add a new user
        }

        public void editUserProfile(ApplicationUser thisUser)
        {
            //TODO: edit thisUser profile
        }
    }
}