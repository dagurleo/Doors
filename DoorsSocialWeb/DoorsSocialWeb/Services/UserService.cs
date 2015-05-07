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
        public ApplicationUser getCurrentUser()
        {
            UserRepository userRepo = new UserRepository();
            return userRepo.getCurrentUser();
        }

    }
}