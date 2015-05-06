using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorsSocialWeb.Model_Classes
{
    public class User
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
        //public List<Group> groups { get; set; }
        public List<Post> posts { get; set; }
    }
}