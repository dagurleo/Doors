﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
   
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ImagePost> Images { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Post> Posts { get; set; }  
        public DbSet<Topic> Topics {get; set; }
        public DbSet<relGroup> relGroups { get; set; }
        public DbSet<relUsers> relUsers { get; set; }
        public DbSet<friendRequest> friendRequests { get; set; }
        public DbSet<groupRequest> groupRequests { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        public static ApplicationDbContext Create() 
        {
            return new ApplicationDbContext();
        }
    }
}