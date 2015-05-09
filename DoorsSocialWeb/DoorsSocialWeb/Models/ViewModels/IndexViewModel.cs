using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Repositories;

namespace DoorsSocialWeb.Models.ViewModels
{
    public class IndexViewModel : LoggedInSharedLayoutViewModel
    {
        public UserRepository userRepo = new UserRepository();
        public IEnumerable<Post> posts { get; set; }
        public PostViewModel post { get; set; }
        public ApplicationUser getAuthor(string id)
        {

            return userRepo.getUserByID(id);
        }

    }
}