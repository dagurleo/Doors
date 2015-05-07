using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using DoorsSocialWeb.Models;
using DoorsSocialWeb.Repositories;



namespace DoorsSocialWeb.Repositories
{
    public class UserRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //TODO: implement more functions we might need and connect to db;

        /*
         * Return a List<ApplicationUser> who are friends with that userID
         */
        public List<ApplicationUser> getUsersByID(int UserID)
        {
            //TODO: return a list<User> who are friends with that userID
            List<ApplicationUser> listOfUser = new List<ApplicationUser>();
            return listOfUser;
        }

        public ApplicationUser getCurrentUser()
        {

            string currentUserId = HttpContext.Current.User.Identity.GetUserId();

            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            return currentUser;
        }

        public void editUserProfile(ApplicationUser thisUser)
        {
            //TODO: edit thisUser profile
        }
    }
}