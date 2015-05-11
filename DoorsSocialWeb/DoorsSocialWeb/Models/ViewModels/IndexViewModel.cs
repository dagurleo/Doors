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
    public class IndexViewModel : LoggedInSharedLayoutViewModel
    {
        public UserService userService = new UserService();
        public LikesService likeService = new LikesService();
        public IEnumerable<Post> posts { get; set; }
        public PostViewModel post { get; set; }
        public IEnumerable<Like> getLikesForPost(int postId)
        {                       
            return likeService.getLikesOnPost(postId);
        }

        public IEnumerable<ApplicationUser> getUsersWhoLikesPost(int postId)
        {
            return likeService.getUsersWhoLikedPost(postId);
        }

        public bool hasCurrentUserLikedPost(int postId)
        {
            var users = getUsersWhoLikesPost(postId);
            foreach (var i in users)
            {
                if (i.Id == currentUser.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public ApplicationUser getAuthor(string id)
        {
            return userService.getUserById(id);
        }
    }
}