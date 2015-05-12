using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Models.ViewModels
{
    public class SearchViewModel : LoggedInSharedLayoutViewModel
    {   
        public IEnumerable<ApplicationUser> usersSearched { get; set; }
        public IEnumerable<Group> groupsSearched { get; set; }
    }
}