using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Repositories;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Models;

namespace DoorsSocialWeb.Services
{
    public class PostService
    {
        private PostRepository postRepo = new PostRepository();

        
        public IEnumerable<Post> getPostByID(int postID)
        {
            return postRepo.getPostByID(postID).ToList();
        }

        public IEnumerable<Post> getAllPosts()
        {
            return postRepo.getAllPosts().ToList();
        }

        public IEnumerable<Post> getAllImagePosts()
        {
            return postRepo.getAllImagePosts().ToList();
        }

        public IEnumerable<Post> getAllTextPosts()
        {
            return postRepo.getAllTextPosts().ToList();
        }

        public IEnumerable<Post> getAllPostByID(string userID)
        {
            return postRepo.getAllPostByID(userID).ToList();
        }

        public IEnumerable<Post> getAllImagePostsByID(string userID)
        {
            return postRepo.getAllImagePostsByID(userID).ToList();
        }

        public IEnumerable<Post> getAllTextPostsByID(string userID)
        {
            return postRepo.getAllTextPostsByID(userID).ToList();
        }

        public IEnumerable<Post> getAllGroupPostsByGroupID(int groupID)
        {
            return postRepo.getAllGroupPostsByGroupID(groupID);
        }

        public IEnumerable<Post> getGroupTopicPostsByGroupID(int groupID, int topicID)
        {
            return postRepo.getGroupTopicPostsByGroupID(groupID, topicID);
        }

        public ApplicationUser getUserOfPost(int postID)
        {
            return postRepo.getUserOfPost(postID);
        }

        public Group getGroupByPostID(int postID)
        {
            return postRepo.getGroupByPostID(postID);
        }
       






        public void addNewPost()
        {
            postRepo.addNewPost();
        }
    }
}