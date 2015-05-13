using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using DoorsSocialWeb.Models;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Repositories;



namespace DoorsSocialWeb.Repositories
{
    public class UserRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //TODO: implement more functions we might need and connect to db;

        public IEnumerable<ApplicationUser> getAllUsers()
        {
            var allUsers = (from user in db.Users
                            select user);
            return allUsers;
        }


        public ApplicationUser getUserByID(string userID)
        {
            ApplicationUser friend = (from user in db.Users
                                      where user.Id == userID
                                      select user).SingleOrDefault();
            return friend;
        }

        public ApplicationUser getCurrentUser()
        {

            string currentUserId = HttpContext.Current.User.Identity.GetUserId();

            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            return currentUser;
        }

        public IEnumerable<ApplicationUser> getMembersOfCurrentGroup(int groupID)
        {
            var userMembers = from uM in db.relGroups
                              where uM.groupID == groupID
                              join u in db.Users
                              on uM.memberID equals u.Id
                              orderby u.displayName ascending
                              select u;

            return userMembers;
        }

        public IEnumerable<ApplicationUser> getFriendsOfCurrentUser()
        {
            string currentUser = HttpContext.Current.User.Identity.GetUserId();

            var leftFriends = from f in db.relUsers
                              where f.friend2Id == currentUser
                              select f.friend1Id;
            
            var rightFriends = from f in db.relUsers
                               where f.friend1Id == currentUser
                               select f.friend2Id;

            var combined = rightFriends.Concat(leftFriends);            

            var allFriends = from u in db.Users
                             join f in combined
                             on u.Id equals f
                             orderby u.displayName ascending
                             select u;

            return allFriends;
        }

        public void requestFriend(string currentUserId, string friendUserId)
        {
            friendRequest friendReq = new friendRequest { userID = friendUserId, userRequestID = currentUserId, userIsApproved = false };
            db.friendRequests.Add(friendReq);
            db.SaveChanges();
        }

        public IEnumerable<friendRequest> getFriendRequests(string userID)
        {
            var requests = from fr in db.friendRequests
                           where fr.userID == userID
                           select fr;

            return requests;
        }

        public void approveUser(friendRequest frReq)
        {
            //TODO - búa til nýtt req með þessum strings og approved = true
            //TODO - finna gamla, færa þetta yfir í það og savea changes
            friendRequest oldFriendReq =  (from fr in db.friendRequests
                                           where fr.userID == frReq.userID &&
                                           fr.userRequestID == frReq.userRequestID
                                           select fr).SingleOrDefault();

            oldFriendReq.userID = frReq.userID;
            oldFriendReq.userRequestID = frReq.userID;
            oldFriendReq.userIsApproved = frReq.userIsApproved;
            db.SaveChanges();
        }

        public void addRelations(string id1, string id2)
        {
            relUsers relationship = new relUsers { friend1Id = id1, friend2Id = id2 };
            db.relUsers.Add(relationship);
            db.SaveChanges();
        }

        public void removeRelations(string id1, string id2)
        {
            var relationship = (from r in db.relUsers
                               where r.friend1Id == id1 && r.friend2Id == id2
                               select r).SingleOrDefault();

            
            db.relUsers.Remove(relationship);
            db.SaveChanges();
        }


        public void editUserProfile(ApplicationUser thisUser)
        {
            var user = getCurrentUser();

            user.displayName = thisUser.displayName;
            user.displayAbout = thisUser.displayAbout;
            user.displayEmail = thisUser.displayEmail;
            user.displayPhoneNumber = thisUser.displayPhoneNumber;
            db.SaveChanges();
        }

        public IEnumerable<ApplicationUser> searchUsersByName(string searchTerm)
        {
            var users = from u in db.Users
                        where u.displayName.Contains(searchTerm)
                        select u;
            return users;
        }
    }
}