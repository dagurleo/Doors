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
using System.Net;
using System.IO;
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
        public MessageService messageService = new MessageService();
        
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
            shared.currentGroupTopics = groupService.getTopicsForGroup(shared.currentGroup.ID);
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


     
        
        public ActionResult addLikeToPost(int postId)
        {                       
            likeService.addLikeOnPost(postId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult RemoveLikeFromPost(int postId)
        {
            likeService.removeLikeOnPost(postId);
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

        
        public ActionResult userApprovesFriendRequest(int requestId)
        {
            
            userService.approveUser(requestId);            
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri + "#notificationCenter");
        }

        public ActionResult userDeclinesFriendRequest(int requestId)
        {
            userService.declineUser(requestId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri + "#notificationCenter");
        }

        public ActionResult userAcceptsGroupRequest(int requestId)
        {
            groupService.userApprovesGroupRequest(requestId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri + "#notificationCenter");
        }

        public ActionResult userDeclinesGroupRequest(int requestId)
        {
            groupService.userDeclinesGroupRequest(requestId);  
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri + "#notificationCenter");
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

        [HttpPost]
        public ActionResult Profile(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                string imageID = userService.getCurrentUser().Id;
                UploadToFtp(file, imageID);

                string URL = "http://www.ads.menn.is/doors/images/" + imageID;
                userService.editProfilePicture(URL);
            }

            

            return RedirectToAction("Profile");
        }

        void UploadToFtp(HttpPostedFileBase uploadfile, string imageID)
        {
            var uploadurl = "ftp://ads.menn.is/public_html/doors/images";
            var uploadfilename = uploadfile.FileName;
            var username = "ads.menn.is";
            var password = "5zXvxNtyTRDvp3uE";
            Stream streamObj = uploadfile.InputStream;
            byte[] buffer = new byte[uploadfile.ContentLength];
            streamObj.Read(buffer, 0, buffer.Length);
            streamObj.Close();
            streamObj = null;
            string ftpurl = String.Format("{0}/{1}", uploadurl, imageID);
            var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
            requestObj.Method = WebRequestMethods.Ftp.UploadFile;
            requestObj.Credentials = new NetworkCredential(username, password);
            Stream requestStream = requestObj.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Flush();
            requestStream.Close();
            requestObj = null;
        }

        public ActionResult Conversation(string messageRecieverId)
        {
            var shared = new MessageViewModel();
            shared.groups = groupService.getAccessibleGroups();
            shared.currentUser = userService.getCurrentUser();
            shared.friends = userService.getFriendsOfCurrentUser();
            shared.messageReciever = userService.getUserById(messageRecieverId);
            shared.messagesWithUser = messageService.getConversation(userService.getCurrentUser().Id, messageRecieverId);
            return View(shared);
        }

        public ActionResult ownerOfGroupCreateNewTopic()
        {
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        [HttpPost]
        public ActionResult ownerOfGroupCreateNewTopic(FormCollection collection)
        {
            string groupIDstring = collection["groupid"];
            string ownerID = collection["ownerid"];
            string topicName = collection["topicName"];
            int groupID = Int32.Parse(groupIDstring);
            groupService.addNewTopic(groupID, topicName);

            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult SendNewMessage(FormCollection collection)
        {
            string senderId = collection["senderId"];
            string recieverId = collection["recieverId"];
            string subject = collection["subject"];
            Message message = new Message { senderID = senderId, recieverID = recieverId, subject = subject, dateCreated = DateTime.Now, };
            messageService.addNewMessage(message);
            IEnumerable<Message> messages = messageService.getConversation(senderId, recieverId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }
        /*
        public ActionResult approvedUsersAddNewPostInGroup()
        {
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        [HttpPost]
        public ActionResult approvedUsersAddNewPostInGroup(FormCollection collection)
        {
            string groupIDstring = collection["groupid"];
            string topicID = collection["t"]
        }
        */

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}