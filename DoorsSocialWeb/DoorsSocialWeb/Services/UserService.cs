using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Repositories;
using DoorsSocialWeb.Models;

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

    }
}