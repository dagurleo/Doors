﻿using System;
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
        private UserRepository userRepo = new UserRepository();

        /*
         * Returns all likes from the post with postID = postId
         */
        public IEnumerable<Like> getLikesOnPost(int postId)
        {
            var likes = from l in db.Likes
                        where l.postID == postId
                        select l;

            return likes;
        }


        /*
         * Returns all likes on comment with commentID = commentId
         */
        public IEnumerable<Like> getLikesOnComment(int commentId)
        {
            var likes = from l in db.Likes
                        where l.commentID == commentId
                        select l;

            return likes;
        }


        /*
         * Creates a new like on post with postID = postId
         */
        public void addLikeToPost(int postId)
        {            
            Like newLike = new Like { authorID = userRepo.getCurrentUser().Id, postID = postId, commentID = 0 };
            db.Likes.Add(newLike);
            db.SaveChanges();
        }

        /*
         * Returns all users who have liked the likes from IEnumerable<Like> likes
         */
        public IEnumerable<ApplicationUser> getUsersWhoLiked(IEnumerable<Like> likes)
        {
            var users = from l in likes
                        join u in db.Users
                        on l.authorID equals u.Id
                        select u;

            return users;
        }

        /*
         * Removes like from post with postID = postId
         */
        public void removeLikeOnPost(int postId)
        {
            var currentUserId = userRepo.getCurrentUser().Id;
            Like likeToRemove = (from l in db.Likes
                                 where l.postID == postId &&
                                 l.authorID == currentUserId
                                 select l).SingleOrDefault();
            if (likeToRemove != null)
            {
                db.Likes.Remove(likeToRemove);
                db.SaveChanges();
            }
        }

       
    }
}