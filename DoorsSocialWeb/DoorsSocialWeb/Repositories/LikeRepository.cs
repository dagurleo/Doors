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
    }
}