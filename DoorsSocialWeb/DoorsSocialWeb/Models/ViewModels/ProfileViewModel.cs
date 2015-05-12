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

        public IEnumerable<ApplicationUser> getUsersWhoLikesPost(int postId)
        {
            return likeService.getUsersWhoLikedPost(postId);
        }

        public bool userIsFriend(string friendId)
        {
            IEnumerable<ApplicationUser> friends = userRepo.getFriendsOfCurrentUser();
            if(friends.Contains(userRepo.getUserByID(friendId)))
            {
                return true;
            }
            return false;
        }

        public IEnumerable<friendRequest> getFriendRequests(string userID)
        {
            return userRepo.getFriendRequests(userID);
        }

        public bool userIsPendingFriend(string friendID, string currentUserId)
        {
            //Sækja öll friend request sem VINUR minn á, tékka hvort ég sé á honum.
            IEnumerable<friendRequest> fRequests = userRepo.getFriendRequests(friendID);
            if(fRequests != null)
            {
                foreach(var f in fRequests)
                {
                    if(f.userRequestID == currentUserId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}