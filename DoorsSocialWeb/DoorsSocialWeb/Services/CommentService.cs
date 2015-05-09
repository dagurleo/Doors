using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Repositories;

namespace DoorsSocialWeb.Services
{
    public class CommentService
    {
        private CommentRepository commentRepo = new CommentRepository();

        public IEnumerable<Comment> getCommentsByPostID(int postID)
        {
            return commentRepo.getCommentsByPostID(postID);
        }


        public IEnumerable<Like> getLikesByComment(int commentID)
        {
            return commentRepo.getLikesByComment(commentID);
        }

        /* 
         * Need info on what variables i have to parse
         */
        public void addNewComment()
        {
            commentRepo.addNewComment();
        }
    }
}