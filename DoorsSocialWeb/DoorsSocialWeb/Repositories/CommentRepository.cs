using System;
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
         * Need info on what variables i have to parse
         */
        public void addNewComment(string userId, int postId, string subject)
        {
            Comment comment = new Comment { authorID = userId, postID = postId, subject = subject, dateCreated = DateTime.Now };
            db.Comments.Add(comment);
            db.SaveChanges();
        }
    }
}