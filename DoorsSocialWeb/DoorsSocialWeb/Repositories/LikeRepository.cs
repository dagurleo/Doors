using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Models;

namespace DoorsSocialWeb.Repositories
{
    public class LikeRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Like> getLikesOnPost(int postId)
        {
            var likes = from l in db.Likes
                        where l.postID == postId
                        select l;

            return likes;
        }

        public IEnumerable<Like> getLikesOnComment(int commentId)
        {
            var likes = from l in db.Likes
                        where l.commentID == commentId
                        select l;

            return likes;
        }

        public void addLikeToPost(string userId, int postId)
        {
            Like newLike = new Like { authorID = userId, postID = postId, commentID = 0 };
            db.Likes.Add(newLike);
            db.SaveChanges();
        }

        public IEnumerable<ApplicationUser> getUsersWhoLiked(IEnumerable<Like> likes)
        {
            var users = from l in likes
                        join u in db.Users
                        on l.authorID equals u.Id
                        select u;

            return users;
        }
    }
}