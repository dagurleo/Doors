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
using DoorsSocialWeb.Repositories;
using DoorsSocialWeb.Services;
using DoorsSocialWeb.Models.EntityModels;
using DoorsSocialWeb.Models.ViewModels;
using DoorsSocialWeb.Services;
namespace DoorsSocialWeb.Controllers
{
    [Authorize]
    public class LoggedInController : Controller
    {
        public UserService userService = new UserService();
        public GroupService groupService = new GroupService();
        public LikesService likeService = new LikesService();
        public PostService postService = new PostService();
        public CommentService commentService = new CommentService();
        //
        // GET: /LoggedIn/
        public ActionResult Index()
        {

            var shared = new IndexViewModel();


            shared.groups = groupService.getAccessibleGroups();
            shared.currentUser = userService.getCurrentUser();
            shared.friends = userService.getFriendsOfCurrentUser();
            shared.posts = postService.getPostsByFriends();
            return View(shared);
        }

        public ActionResult GroupView(int id)
        {
            var shared = new GroupViewModel();
            shared.groups = groupService.getAccessibleGroups();
            shared.currentUser = userService.getCurrentUser();
            shared.friends = userService.getFriendsOfCurrentUser();
            shared.currentGroup = groupService.getCurrentGroup(id);
            return View(shared);
        }


        public ActionResult CreateGroup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGroup(FormCollection collection)
        {
            string groupOwnerId = collection["ownerid"];
            string groupName = collection["groupname"];
            string groupDescription = collection["groupdescription"];

            Group group = new Group { groupOwnerID = groupOwnerId, groupName = groupName, groupDescription = groupDescription };

            groupService.addNewGroup(group);
            Group theGroup = groupService.getNewestGroup();
            groupService.addUserToGroup(groupOwnerId, theGroup.ID);
            return RedirectToAction("GroupView", "LoggedIn", new { id = theGroup.ID });
        }

        public ActionResult EditProfile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditProfile(FormCollection collection)
        {
            string displayName = collection["displayname"];
            string displayAbout = collection["about"];
            string displayEmail = collection["displayemail"];
            string displayPhone = collection["displayphone"];

            ApplicationUser userInfo = new ApplicationUser { displayAbout = displayAbout, displayName = displayName, displayPhoneNumber = displayPhone, displayEmail = displayEmail };
            userService.editProfile(userInfo);

            return RedirectToAction("Profile", "LoggedIn", new { id = userService.getCurrentUser().Id });
        }

        public ActionResult Profile(string id)
        {

            var shared = new ProfileViewModel();
            shared.groups = groupService.getAccessibleGroups();
            shared.currentUser = userService.getCurrentUser();
            shared.friends = userService.getFriendsOfCurrentUser();
            shared.friend = userService.getUserById(id);


            var postRepo = new PostRepository();
            shared.posts = postRepo.getAllPostByID(id);
            //shared.allUserTextPosts = postRepo.getAllTextPostsByID(id);
            //shared.allUserImagePosts = postRepo.getAllImagePostsByID(id);

            return View(shared);
        }

        public ActionResult Post()
        {
            return RedirectToAction("Index", "LoggedIn");
        }

        [HttpPost]
        public ActionResult Post(FormCollection collection)
        {
            string userid = collection["userid"];
            string subject = collection["subject"];
            string datetime = collection["datetime"];

            if (subject != "")
            {
                Post post = new Post { authorID = userid, subject = subject, dateCreated = DateTime.Now };

                postService.addNewPost(post);
            }
            return RedirectToAction("Index", "LoggedIn");

        }

        public ActionResult DeletePost(int postId)
        {
            postService.removePost(postId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }


        /*
        public ActionResult addLikeToPost()
        {
            return RedirectToAction("Index", "LoggedIn");
        }
        */
        [HttpPost]
        public ActionResult addLikeToPost(FormCollection collection)
        {
            var shared = new IndexViewModel();
            shared.groups = groupService.getAccessibleGroups();
            shared.currentUser = userService.getCurrentUser();
            shared.friends = userService.getFriendsOfCurrentUser();

            string userId = collection["userid"];
            string postIdString = collection["postid"];
            int postId = Int32.Parse(postIdString);
            likeService.addLikeOnPost(userId, postId);
            var theLike = new Like { authorID = userId, postID = postId, commentID = 0 };
            return Json(theLike, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveLikeFromPost()
        {
            return RedirectToAction("Index", "LoggedIn");
        }

        [HttpPost]
        public ActionResult RemoveLikeFromPost(FormCollection collection)
        {
            string userId = collection["userid"];
            string postIdString = collection["postid"];
            int postId = Int32.Parse(postIdString);
            likeService.removeLikeOnPost(userId, postId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult addCommentToPost()
        {
            return RedirectToAction("Index", "LoggedIn");
        }

        [HttpPost]
        public ActionResult addCommentToPost(FormCollection collection)
        {
            string userID = collection["userid"];
            string postIdString = collection["postid"];
            string subject = collection["subject"];
            int postId = Int32.Parse(postIdString);

            if (subject != "")
            {
                var commentService = new CommentService();
                commentService.addNewComment(userID, postId, subject);
            }
            return RedirectToAction("Index", "LoggedIn");
        }

        public ActionResult removeCommentFromPost(int commentId)
        {
            commentService.removeComment(commentId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult requestFriendship()
        {
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        [HttpPost]
        public ActionResult requestFriendship(FormCollection collection)
        {
            string currentUserId = collection["userid"];
            string friendUserId = collection["friendid"];
            userService.requestFriend(currentUserId, friendUserId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult userApprovesFriendRequests()
        {
            return View();
        }

        [HttpPost]
        public ActionResult userApprovesFriendRequests(FormCollection collection)
        {
            string currentUserId = collection["userid"];
            string friendUserId = collection["friendid"];
            friendRequest frReq = new friendRequest { userID = currentUserId, userRequestID = friendUserId, userIsApproved = true };
            userService.approveUser(frReq);
            userService.addRelations(currentUserId, friendUserId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult removeFriend()
        {
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        [HttpPost]
        public ActionResult removeFriend(FormCollection collection)
        {
            string currentUserId = collection["userid"];
            string friendUserId = collection["friendid"];

            userService.removeRelations(currentUserId, friendUserId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        [HttpPost]
        public ActionResult Search(FormCollection collection)
        {
            var searchTerm = collection["searchTerm"];
            var shared = new SearchViewModel();
            shared.groups = groupService.getAccessibleGroups();
            shared.currentUser = userService.getCurrentUser();
            shared.friends = userService.getFriendsOfCurrentUser();
            shared.usersSearched = userService.searchUsersByName(searchTerm);
            shared.groupsSearched = groupService.searchGroupsByName(searchTerm);
            if (searchTerm == "")
            {
                return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }
            else
            {
                return View(shared);
            }
        }


        public ActionResult requestToJoinGroup()
        {
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        [HttpPost]
        public ActionResult requestToJoinGroup(FormCollection collection)
        {
            string requestUserId = collection["userid"];
            string groupId = collection["groupid"];
            string groupOwner = collection["groupOwner"];
            var groupIdInt = Int32.Parse(groupId);

            groupService.sendGroupRequest(requestUserId, groupIdInt, groupOwner);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult ownerOfGroupAcceptsUsers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ownerOfGroupAcceptsUsers(FormCollection collection)
        {
            string requestUserId = collection["userid"];
            string groupIDstring = collection["groupid"];
            string groupOwner = collection["groupOwner"];
            var groupIdInt = Int32.Parse(groupIDstring);
            groupRequest groupReq = new groupRequest { groupID = groupIdInt, groupOwnerId = groupOwner, userIsApproved = true, userRequestId = requestUserId };
            groupService.approveGroupRequest(groupReq);
            groupService.addUserToGroup(requestUserId, groupIdInt);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}