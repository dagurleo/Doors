using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models;

namespace DoorsSocialWeb.Model_Classes
{
    public class Group
    {
        public int groupID { get; set; }
        public int groupOwnerID { get; set; }
        public string groupName { get; set; }
        public string groupDescription { get; set; }
        public List<ApplicationUser> members { get; set; }
        public List<Topic> topics { get; set; }
    }
}