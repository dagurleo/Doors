using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Models;

namespace DoorsSocialWeb.Repositories
{
    public class PostRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        /*
         * Return a post with that ID
         */
        public IEnumerable<Post> getPostByID(int postID)
        {
            var queryAllPostsMadeByPostID = (from post in db.Posts where post.ID == postID select post);
            return queryAllPostsMadeByPostID;
        }

        /*
         * Returns all posts, for newsfeed view.
         */
        public IEnumerable<Post> getAllPosts()
        {
            var queryAllPosts = (from post in db.Posts select post);
            return queryAllPosts;
        }

        /*
         * Returns all image posts, for newsfeed filtering.
         * Need to add to table postIsImage bool variable, to be able to filter.
         *
        public IEnumerable<Post> getAllImagePosts()
        {
            var queryAllImagePosts = (from post in db.Posts where post.)
        }
         */

        /*
         * Returns all text posts, for newsfeed filtering
         * same as above, or postisimage bool == false; problem if we want users to be able to add f.x. videos in the future.... what then ?
         *
        public IEnumerable<Post> getAllTextPosts()
        {
            //var queryAllTextPosts = (from post in db.Posts where post.postistext == true);
            IEnumerable<Post> bla;
            return bla;
        }
        
        *
         * Returns all posts with that userID
         *
        public IEnumerable<Post> getAllPostByID(string userID)
        {
            var queryAllPostsMadeByUser = (from post in db.Posts where post.authorID == userID select post);
            return queryAllPostsMadeByUser;
        }

        *
         * Return all image post with that userID
         * SAME AS ABOVE
         *
        public IEnumerable<Post> getAllImagePostsByID(string userID)
        {
            
        }

        *
         * Return all text posts with that userID
         * SAME AS ABOVE
         *
        public IEnumerable<Post> getAllTextPostsByID(string userID)
        {

        }

        /*
         * Create a new post
         */
        public void addNewPost()
        {
            
        }
    }
}