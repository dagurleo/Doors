using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Repositories;
using DoorsSocialWeb.Models;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Services
{
    public class UserService
    {
        //TODO: Implement functions to deliver users/profiles to views...
        /*
         * Returns the current user by calling getCurrentUser from userrepo
         */
        private UserRepository userRepo = new UserRepository();

        public ApplicationUser getCurrentUser()
        {
            return userRepo.getCurrentUser();
        }

        public IEnumerable<ApplicationUser> getFriendsOfCurrentUser()
        {
            return userRepo.getFriendsOfCurrentUser();
        }

        public IEnumerable<ApplicationUser> getMembersOfCurrentGroup(int groupID)
        {
            return userRepo.getMembersOfCurrentGroup(groupID);
        }

        public ApplicationUser getUserById(string id)
        {
            return userRepo.getUserByID(id);
        }

        public void addRelations(string id1, string id2)
        {
            userRepo.addRelations(id1, id2);
        }

        public void editProfile(ApplicationUser userUpdate)
        {
            userRepo.editUserProfile(userUpdate);
        }

  
    }
}