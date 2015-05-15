using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Services;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Models.ViewModels
{
    public class TopicViewModel : LoggedInSharedLayoutViewModel
    {        
        public Group currentGroup { get; set; }
        public Topic currentTopic { get; set; }
        public UserService userService = new UserService();
        public PostService postService = new PostService();
        public CommentService commentService = new CommentService();
        public LikesService likeService = new LikesService();
        public IEnumerable<Topic> topics { get; set; }
        public IEnumerable<Post> posts { get; set; }
        

        public IEnumerable<ApplicationUser> getGroupMembersByGroupId(int groupID)
        {
            return userService.getMembersOfCurrentGroup(groupID);
        }

        public IEnumerable<Post> getPostsWithinTopicID(int topicID)
        {
            return postService.getPostsWithinTopicByTopicId(topicID);
        }

        public IEnumerable<Comment> getCommentsForPost(int postID)
        {
            return commentService.getCommentsByPostID(postID);
        }

        public IEnumerable<ApplicationUser> getUsersWhoLikesPost(int postID)
        {
            return likeService.getUsersWhoLikedPost(postID);
        }

        public bool hasCurrentUserLikedPost(int postID)
        {
            if(getUsersWhoLikesPost(postID).Contains(getAuthor(currentUser.Id)))
            {
                return true;
            }
            return false;
        }

    }
}