using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Repositories;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Services
{
    public class PostService
    {
        private PostRepository postRepo = new PostRepository();

        /*
         * Return a post with that ID
        */
        public List<Post> getPostByID(int postID)
        {
            return postRepo.getPostByID(postID).ToList();
        }

        /*
         * Returns all posts, for newsfeed view.
         */
        public List<Post> getAllPosts()
        {
            return postRepo.getAllPosts().ToList();
        }
/*
        public List<Post> getAllImagePosts()
        {
            return postRepo.getAllImagePosts().ToList();
        }

        public IEnumerable<Post> getAllTextPosts()
        {
            return postRepo.getAllTextPosts().ToList();
        }
*/
        /*
         * Returns all posts with that userID
         */
        public List<Post> getAllPostByID(string userID)
        {
            return postRepo.getAllPostByID(userID).ToList();
        }

        /*
         * Return all image post with that userID
         * SAME AS ABOVE
         */
        public List<Post> getAllImagePostsByID(string userID)
        {
            return postRepo.getAllImagePostsByID(userID).ToList();
        }

        /*
         * Return all text posts with that userID
         * SAME AS ABOVE
         */
        public List<Post> getAllTextPostsByID(string userID)
        {
            return postRepo.getAllTextPostsByID(userID).ToList();
        }

        /*
         * Create a new post, need info on variables to parse to repo..
         */
        public void addNewPost()
        {
            postRepo.addNewPost();
        }
    }
}