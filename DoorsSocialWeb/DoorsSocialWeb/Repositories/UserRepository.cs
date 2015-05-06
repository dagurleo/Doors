using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Model_Classes;

namespace DoorsSocialWeb.Repositories
{
    public class UserRepository
    {
        //TODO: implement more functions we might need and connect to db;
        public List<User> getUsersByID(int UserID)
        {
            //TODO: return a list<User> or a User by that ID? NOT SURE
            List<User> listOfUser = new List<User>();
            return listOfUser;
        }

        public void addNewUser(User newUser)
        {
            //TODO: add a new user
        }

        public void editUserProfile(User thisUser)
        {
            //TODO: edit thisUser profile
        }
    }
}