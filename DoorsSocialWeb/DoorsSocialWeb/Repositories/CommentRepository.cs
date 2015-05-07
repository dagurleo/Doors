using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Repositories
{
    public class CommentRepository
    {
        //TODO: implement more functions we might need and connect to db;
        public List<Comment> getCommentsByPostID(int postID)
        {
            //TODO: return a List<Comment> with comments from that ID
            List<Comment> listOfComments = new List<Comment>();
            return listOfComments;
        }

        public void addNewComment(Comment c)
        {
            //TODO: create a new comment and save it
            return;
        }

        public List<Like> getLikesByComment(int commentID)
        {
            //TODO: return List<Like> filled with likes on that comment.
            List<Like> listOfLikes = new List<Like>();
            return listOfLikes;
        }
    }
}