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

            if(groupName != "")
            {
                Group group = new Group { groupOwnerID = groupOwnerId, groupName = groupName, groupDescription = groupDescription };

                groupService.addNewGroup(group);
                Group theGroup = groupService.getNewestGroup();
                groupService.addUserToGroup(groupOwnerId, theGroup.ID);
                return RedirectToAction("GroupView", "Group", new { id = theGroup.ID });
            }
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
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

        public ActionResult PostImage()
        {
            return RedirectToAction("Index", "LoggedIn");
        }

        [HttpPost]
        public ActionResult PostImage(HttpPostedFileBase file, FormCollection collection)
        {
            if(file == null)
            {
                return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }
            string userid = collection["userid"];
            string subject = collection["subject"];
            if(file.ContentLength > 0)
            {
                Post post = new Post { authorID = userid, postIsImage = true, subject = subject, dateCreated = DateTime.Now};
                postService.addNewPost(post);

                string imageID = post.ID.ToString();
                UploadToFtp(file, imageID);

                string URL = "http://www.ads.menn.is/doors/images/" + imageID;

                postService.addImagePost(post.ID, URL);
            }
            return RedirectToAction("Index", "LoggedIn");
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
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
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
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult removeCommentFromPost(int commentId)
        {
            commentService.removeComment(commentId);
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

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}