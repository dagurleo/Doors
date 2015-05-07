﻿using Microsoft.AspNet.Identity.EntityFramework;
using DoorsSocialWeb.Model_Classes;

namespace DoorsSocialWeb.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string registerEmail { get; set; }
        public string displayName { get; set; }
        public string displayImageUrl { get; set; }
        public string displayAbout { get; set; }
        public string displayEmail { get; set; }
        public string displayPhoneNumber { get; set; }
        public bool userIsModerator { get; set; }
        public bool userIsAdministrator { get; set; }
        public List<Message> messages { get; set; }
        public List<Notification> notifications { get; set; }
        public List<Group> groups { get; set; }
        public List<Post> posts { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}