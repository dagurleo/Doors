using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Repositories;
namespace DoorsSocialWeb.Models.ViewModels
{
    public class ProfileViewModel : LoggedInSharedLayoutViewModel
    {
        UserRepository userRepo = new UserRepository();
        public IEnumerable<Post> posts { get; set; }
        public ApplicationUser friend { get; set; }
        public ApplicationUser getAuthor(string id)
        {
            return userRepo.getUserByID(id);
        }
    }
}