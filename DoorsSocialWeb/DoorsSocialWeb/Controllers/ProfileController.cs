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
    public class ProfileController : Controller
    {
        public UserService userService = new UserService();
        public GroupService groupService = new GroupService();
        public LikesService likeService = new LikesService();
        public PostService postService = new PostService();
        public CommentService commentService = new CommentService();
        public MessageService messageService = new MessageService();
        //
        // GET: /Profile/
        public ActionResult Index()
        {
            return View();
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

            return RedirectToAction("Profile", "Profile", new { id = userService.getCurrentUser().Id });
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
        public ActionResult Profile(HttpPostedFileBase file)
        {
            if(file == null)
            {
                return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }
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
            var password = "her kemur password";
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



        public ActionResult SendNewMessage(FormCollection collection)
        {
            string senderId = collection["senderId"];
            string recieverId = collection["recieverId"];
            string subject = collection["subject"];
            if(subject != "")
            {
                Message message = new Message { senderID = senderId, recieverID = recieverId, subject = subject, dateCreated = DateTime.Now, };
                messageService.addNewMessage(message);
                IEnumerable<Message> messages = messageService.getConversation(senderId, recieverId);
            }
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult wallPostFromFriend()
        {
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        [HttpPost]
        public ActionResult wallPostFromFriend(HttpPostedFileBase file, FormCollection collection)
        {
            string recipientUserId = collection["recipientUserId"];
            string userID = collection["userId"];
            string subject = collection["subject"];
            if(file == null)
            {
                if (subject != "")
                {
                    Post newPost = new Post { authorID = userID, dateCreated = DateTime.Now, recipientId = recipientUserId, subject = subject };
                    postService.addNewPost(newPost);
                }
                return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }
            else
            {
                if (file.ContentLength > 0)
                {
                    Post post = new Post { authorID = userID, recipientId = recipientUserId, postIsImage = true, subject = subject, dateCreated = DateTime.Now };
                    postService.addNewPost(post);

                    string imageID = post.ID.ToString();
                    UploadToFtp(file, imageID);

                    string URL = "http://www.ads.menn.is/doors/images/" + imageID;

                    postService.addImagePost(post.ID, URL);
                }
                return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }
        }
	}
}
