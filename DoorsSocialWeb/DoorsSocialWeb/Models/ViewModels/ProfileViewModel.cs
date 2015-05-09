using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;
namespace DoorsSocialWeb.Models.ViewModels
{
    public class ProfileViewModel : LoggedInSharedLayoutViewModel
    {
        public IEnumerable<Post> posts { get; set; }
        public ApplicationUser friend { get; set; }

    }
}