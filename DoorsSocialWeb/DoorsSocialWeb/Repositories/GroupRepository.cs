using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DoorsSocialWeb.Repositories
{
    public class GroupRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /*
         * fills a list of group, with all accessible groups for the currently logged in user
         */
        public IEnumerable<Group> getAccessibleGroups()
        {
            //TODO: Fill listOfGroup with groups that ID, not sure what the variable should be named
            ApplicationDbContext db = new ApplicationDbContext();
            string currentUserID = HttpContext.Current.User.Identity.GetUserId();

            var accessibleGroups = from g in db.relGroups
                                   where g.memberID == currentUserID
                                   join gr in db.Groups
                                   on g.groupID equals gr.ID
                                   orderby gr.groupName ascending
                                   select gr;

            return accessibleGroups;
        }

        public Group getCurrentGroup(int id)
        {
            var currGroup = (from g in db.Groups
                             where g.ID == id
                             select g).SingleOrDefault();
            return currGroup;
        }

        public void addNewGroup(Group newGroup)
        {
            TopicRepository topicRepo = new TopicRepository();
            db.Groups.Add(newGroup);
            db.SaveChanges();
            topicRepo.addNewTopic(newGroup.ID, "General discussion");
        }

        public void addUserToGroup(string userId, int groupId)
        {
            relGroup relationship = new relGroup { memberID = userId, groupID = groupId };
            db.relGroups.Add(relationship);
            db.SaveChanges();
        }

        public Group getGroupById(int groupId)
        {
            var group = (from g in db.Groups
                         where g.ID == groupId
                         select g).Single();
            return group;
        }

        public Group getNewestGroup()
        {
            var group = (from g in db.Groups
                         orderby g.ID descending
                         select g).First();
            return group;
        }

        public void editGroup(Group thisGroup)
        {
            //TODO: Edit thisGroup and save.
            return;
        }

        public void addTopic(Topic topic)
        {
            db.Topics.Add(topic);
            db.SaveChanges();
        }

        public Topic getTopicById(int topicId)
        {
            Topic topic = (from t in db.Topics
                           where t.ID == topicId
                           select t).Single();
            return topic;
        }

        public void deleteTopic(int topicId)
        {
            var posts = from p in db.Posts
                        where p.groupTopicID == topicId
                        select p;
            var postRepo = new PostRepository();
            foreach(var i in posts)
            {
                postRepo.removePost(i.ID);
            }
            db.Topics.Remove(getTopicById(topicId));
            db.SaveChanges();
        }

        public void getUserRequests()
        {

        }

        public IEnumerable<Group> searchGroupsByName(string searchTerm)
        {
            var groups = from g in db.Groups
                         where g.groupName.Contains(searchTerm)
                         select g;
            return groups;
        }

        public IEnumerable<groupRequest> getGroupRequests(int groupID)
        {
            var requests = from g in db.Groups
                           where g.ID == groupID
                           join r in db.groupRequests
                           on g.ID equals r.groupID
                           select r;

            return requests;
        }

        public void sendGroupRequest(string requestUserId, int groupId, string groupOwner)
        {
            groupRequest groupReq = new groupRequest { groupID = groupId, groupOwnerId = groupOwner, userIsApproved = false, userRequestId = requestUserId };
            db.groupRequests.Add(groupReq);
            db.SaveChanges();
        }
        
        public groupRequest getGroupRequestById(int requestId)
        {
            var request = (from r in db.groupRequests
                           where r.id == requestId
                           select r).Single();
            return request;
        }

        public void userApprovesGroupRequest(int requestId)
        {
            var request = getGroupRequestById(requestId);
            addUserToGroup(request.userRequestId, request.groupID);
            db.groupRequests.Remove(request);
            db.SaveChanges();
        }

        public void userDeclinesGroupRequest(int requestId)
        {
            var request = getGroupRequestById(requestId);
            db.groupRequests.Remove(request);
            db.SaveChanges();
        }
        public IEnumerable<groupRequest> getGroupRequestsYouAreOwnerOf(string userId)
        {
            var requests = from r in db.groupRequests
                           where r.groupOwnerId == userId
                           select r;

            return requests;
        }

        public void leaveGroup(int groupId)
        {
            string currentUserId = HttpContext.Current.User.Identity.GetUserId();
            var relation = (from r in db.relGroups
                            where r.groupID == groupId && r.memberID == currentUserId
                            select r).Single();

            db.relGroups.Remove(relation);
            db.SaveChanges();
        }

        public int getGroupIdByTopicId(int topicId)
        {
            var groupId = (from t in db.Topics
                           where t.ID == topicId
                           select t.groupID).Single();

            return groupId;
        }

    }
}