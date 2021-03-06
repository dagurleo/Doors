﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Models;

namespace DoorsSocialWeb.Repositories
{
    public class CommentRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /*
         * Returns all comments on the post with the id postID
         */
        public IEnumerable<Comment> getCommentsByPostID(int postID)
        {
            var listOfComments = (from comment in db.Comments
                                  where comment.postID == postID
                                  select comment);
            return listOfComments;
        }

        /*
         * Returns the like list on the comment with commentID
         */
        public IEnumerable<Like> getLikesByComment(int commentID)
        {
            var listOfLikes = (from like in db.Likes 
                               where like.commentID == commentID
                               select like);
            return listOfLikes;
        }

        /* 
         * Creates a new comment, and saves it on the database.
         */
        public void addNewComment(string userId, int postId, string subject)
        {
            Comment comment = new Comment { authorID = userId, postID = postId, subject = subject, dateCreated = DateTime.Now };
            db.Comments.Add(comment);
            db.SaveChanges();
        }

        /*
         * Removes the comment with the commentId from the database
         */

        public void removeComment(int commentId)
        {
            var comment = (from c in db.Comments
                          where c.ID == commentId
                          select c).Single();

            db.Comments.Remove(comment);
            db.SaveChanges();
        }
    }
}