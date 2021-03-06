﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Services;
using DoorsSocialWeb.Models.EntityModels;

namespace DoorsSocialWeb.Models.ViewModels
{
    public class GroupViewModel : LoggedInSharedLayoutViewModel
    {
        public Group currentGroup { get; set; }
        public GroupService groupService = new GroupService();
        public CommentService commentService = new CommentService();
        public UserService userService = new UserService();
        public PostService postService = new PostService();
        public LikesService likeService = new LikesService();
        public IEnumerable<Post> groupPosts { get; set; }
        public IEnumerable<Topic> currentGroupTopics { get; set; }

        public bool userIsMember(string userID)
        {
            if(userService.getMembersOfCurrentGroup(currentGroup.ID).Contains(userService.getUserById(userID)))
            {
                return true;
            }
            return false;
        }


        public bool userIsPendingMember(string userID)
        {
            foreach(var r in pendingRequests(currentGroup.ID))
            {
                if(r.userRequestId == userID)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<ApplicationUser> getGroupMembersByGroupId(int groupID)
        {
            return userService.getMembersOfCurrentGroup(groupID);
        }

        public bool userIsApprovedMember(string userID, int groupID)
        {
            if (userService.getMembersOfCurrentGroup(groupID).Contains(userService.getUserById(userID)))
            {
                return true;
            }
            return false;
        }

        public IEnumerable<groupRequest> pendingRequests(int groupID)
        {
            return groupService.getGroupRequests(groupID);
        }

        public ApplicationUser getUserByID(string userID)
        {
            return userService.getUserById(userID);
        }

        public IEnumerable<Topic> getTopicsWithinGroup(int groupID)
        {
            return groupService.getTopicsForGroup(groupID);
        }

        public IEnumerable<Post> getPostsWithinGroup(int groupID)
        {
            return postService.getAllGroupPostsByGroupID(groupID);
        }

        public IEnumerable<Comment> getCommentsForPost(int postID)
        {
            var comments = commentService.getCommentsByPostID(postID);
            return comments;
        }

        public IEnumerable<ApplicationUser> getUsersWhoLikesPost(int postID)
        {
            return likeService.getUsersWhoLikedPost(postID);
        }

        public bool hasCurrentUserLikedPost(int postID)
        {
            if (getUsersWhoLikesPost(postID).Contains(getAuthor(currentUser.Id)))
            {
                return true;
            }
            return false;
        }
    }
}