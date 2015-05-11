using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Repositories;
using DoorsSocialWeb.Services;

namespace DoorsSocialWeb.Models.ViewModels
{
    public class ProfileViewModel : LoggedInSharedLayoutViewModel
    {
        UserRepository userRepo = new UserRepository();
        public LikesService likeService = new LikesService();
        public IEnumerable<Post> posts { get; set; }
        public ApplicationUser friend { get; set; }
        public ApplicationUser getAuthor(string id)
        {
            return userRepo.getUserByID(id);
        }

        public IEnumerable<Like> getLikesForPost(int postId)
        {
            var likes = likeService.getLikesOnPost(postId);
            return likes;
        }
    }
}