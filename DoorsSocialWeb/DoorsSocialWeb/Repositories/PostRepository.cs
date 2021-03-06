﻿using System;
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
        public Post getPostByID(int postID)
        {
            var queryAllPostsMadeByPostID = (from post in db.Posts
                                             where post.ID == postID
                                             select post).Single();
            return queryAllPostsMadeByPostID;
        }

        /*
         * Returns all posts, for newsfeed view.
         */
        public IEnumerable<Post> getAllPosts()
        {
            var queryAllPosts = (from post in db.Posts
                                 orderby post.dateCreated descending
                                 select post);
            return queryAllPosts;
        }

        /*
         * Returns all image posts, for newsfeed filtering.
         */
        public IEnumerable<Post> getAllImagePosts()
        {
            var queryAllImagePosts = (from post in db.Posts
                                      where post.postIsImage == true
                                      select post);
            return queryAllImagePosts;
        }


        /*
         * Returns all text posts, for newsfeed filtering
         * THIS IS WRONG
         */
        public IEnumerable<Post> getAllTextPosts()
        {
            var queryAllTextPosts = (from post in db.Posts 
                                     where post.postIsImage == false
                                     select post);
            return queryAllTextPosts;
        }
        
        /*
         * Returns all posts with that userID
         */
        public IEnumerable<Post> getAllPostByID(string userID)
        {
            var queryAllPostsMadeByUser = (from post in db.Posts
                                           where post.authorID == userID &&
                                           post.groupId == 0
                                           orderby post.dateCreated descending
                                           select post);
            return queryAllPostsMadeByUser;
        }

        /*
         * Return all image post with that userID
         */
        public IEnumerable<Post> getAllImagePostsByID(string userID)
        {
            var queryAllImagePostsMadeByUser = (from post in db.Posts
                                                where ((post.authorID == userID &&
                                                post.postIsImage == true) &&
                                                post.groupId == 0)
                                                select post);
            return queryAllImagePostsMadeByUser;
        }

        /*
         * Return all text posts with that userID
         * SAME AS ABOVE
         */
        public IEnumerable<Post> getAllTextPostsByID(string userID)
        {
            var queryAllTextPostsMadeByUser = (from post in db.Posts
                                                where ((post.authorID == userID &&
                                                post.postIsImage == false) &&
                                                post.groupId == 0)
                                                orderby post.dateCreated descending
                                                select post);
            return queryAllTextPostsMadeByUser;
        }

        public ApplicationUser getUserOfPost(int postID)
        {
            var user = (from post in db.Posts
                        where post.ID == postID
                        join u in db.Users
                        on post.authorID equals u.Id
                        select u).Single();
            return user;
        }

        public IEnumerable<Post> getAllGroupPostsByGroupID(int groupID)
        {
            var allGroupPosts = (from post in db.Posts
                                 where post.groupId != null &&
                                 post.groupId == groupID
                                 orderby post.dateCreated descending
                                 select post);
            return allGroupPosts;
        }

        public IEnumerable<Post> getGroupTopicPostsByGroupID(int groupID, int topicID)
        {
            var groupTopicPosts = (from post in db.Posts
                                   where post.groupId != null &&
                                   post.groupId == groupID &&
                                   post.groupTopicID == topicID
                                   orderby post.dateCreated descending
                                   select post
                                   );
            return groupTopicPosts;
        }

        public Group getGroupByPostID(int postID)
        {
            var group = (from post in db.Posts
                         where post.groupId != null &&
                         post.ID == postID
                         join gr in db.Groups
                         on post.groupId equals gr.ID
                         select gr
                         ).Single();
            return group;
        }

        public IEnumerable<Post> getPostsFromFriends()
        {   
            var userRepo = new UserRepository();
            IEnumerable<ApplicationUser> friends = userRepo.getFriendsOfCurrentUser();            
            var friendsPosts = from f in friends
                               join p in db.Posts on f.Id equals p.authorID                                                             
                               select p;

            var myPosts = getAllPostByID(userRepo.getCurrentUser().Id);

            friendsPosts = friendsPosts.Concat(myPosts);
            var allPosts = from f in friendsPosts
                           where f.groupId == 0 &&
                           f.recipientId == null
                           orderby f.dateCreated descending
                           select f;

            return allPosts;
                             
        }

        public IEnumerable<Post> getPostsWithinTopicByTopicId(int topicID)
        {
            var posts = (from p in db.Posts
                         where p.groupTopicID == topicID
                         orderby p.dateCreated descending
                         select p);
            return posts;
        }


        public void addNewPost(Post post)
        {
            db.Posts.Add(post);
            db.SaveChanges();
        }
        public Post getSinglePostById(int postId)
        {
            Post post = (from p in db.Posts 
                         where p.ID == postId
                         select p).Single();
            return post;
        }
        public void removePost(int postId)
        {
            var post = (from p in db.Posts
                        where p.ID == postId
                        select p).Single();

            var comments = from c in db.Comments
                           where c.postID == postId
                           select c;

            var likes = from l in db.Likes
                        where l.postID == postId
                        select l;

            foreach(var co in comments)
            {
                db.Comments.Remove(co);
            }

            foreach(var l in likes)
            {
                db.Likes.Remove(l);
            }
            db.Posts.Remove(post);
            db.SaveChanges();
        }

        public void addImagePost(int postID, string URL)
        {
            Post OldPost = (from p in db.Posts
                            where p.ID == postID
                            select p).SingleOrDefault();
            OldPost.imageLink = URL;
            db.SaveChanges();
        }
    }
}