using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DoorsSocialWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string registerEmail { get; set; }
        public string displayName { get; set; }
        public string displayImageUrl { get; set; }
        public string displayAbout { get; set; }
        public string displayEmail { get; set; }
        public string displayPhoneNumber { get; set; }
        public bool userIsModerator { get; set; }
        public bool userIsAdministrator { get; set; }
    }
}