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
            return postRepo.getPostByID(postID);
        }

        public IEnumerable<Post> getAllPosts()
        {
            return postRepo.getAllPosts();
        }

        public IEnumerable<Post> getAllImagePosts()
        {
            return postRepo.getAllImagePosts();
        }

        public IEnumerable<Post> getAllTextPosts()
        {
            return postRepo.getAllTextPosts();
        }

        public IEnumerable<Post> getAllPostByID(string userID)
        {
            return postRepo.getAllPostByID(userID);
        }

        public IEnumerable<Post> getAllImagePostsByID(string userID)
        {
            return postRepo.getAllImagePostsByID(userID);
        }

        public IEnumerable<Post> getAllTextPostsByID(string userID)
        {
            return postRepo.getAllTextPostsByID(userID);
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
       
        public void addNewPost(Post post)
        {
            postRepo.addNewPost(post);
        }
    }
}