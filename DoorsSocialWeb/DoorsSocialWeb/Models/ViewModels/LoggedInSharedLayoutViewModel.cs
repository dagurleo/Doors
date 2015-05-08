using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;


namespace DoorsSocialWeb.Models.ViewModels
{
    public class LoggedInSharedLayoutViewModel
    {
        public ApplicationUser currentUser { get; set; }
        public List<Group> groups {get; set; }
        public List<ApplicationUser> friends { get; set; }
    }
}