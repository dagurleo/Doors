using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Repositories;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Services
{
    public class LikesService
    {
        private LikeRepository likeRepo = new LikeRepository();

        public IEnumerable<Like> getLikesOnPost(int postId)
        {
            return likeRepo.getLikesOnPost(postId);
        }
        
        public IEnumerable<Like> getLikesOnComment(int commentId)
        {
            return likeRepo.getLikesOnComment(commentId);
        }
    }
}