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
    public class GroupController : Controller
    {
        public UserService userService = new UserService();
        public GroupService groupService = new GroupService();
        public LikesService likeService = new LikesService();
        public PostService postService = new PostService();
        public CommentService commentService = new CommentService();
        public MessageService messageService = new MessageService();
        //
        // GET: /Group/
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

        public ActionResult Topic(int id)
        {
            var shared = new TopicViewModel();
            shared.groups = groupService.getAccessibleGroups();
            shared.currentUser = userService.getCurrentUser();
            shared.friends = userService.getFriendsOfCurrentUser();
            shared.currentTopic = groupService.getTopicById(id);
            shared.currentGroup = groupService.getCurrentGroup(shared.currentTopic.groupID);
            
            shared.topics = groupService.getTopicsForGroup(shared.currentGroup.ID);
            return View(shared);
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
            if(topicName != "" && !(groupService.doesTopicExistWithinGroup(groupID, topicName)))
            {
                groupService.addNewTopic(groupID, topicName);

            }
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult deleteTopic(int topicId)
        {
            var currentTopic = groupService.getTopicById(topicId);
            groupService.deleteTopic(topicId);
            
            return RedirectToAction("GroupView", "Group", new {id = currentTopic.groupID});
        }

        public ActionResult approvedUsersAddNewPostInGroup()
        {
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        [HttpPost]
        public ActionResult approvedUsersAddNewPostInGroup(FormCollection collection)
        {
            string groupIDstring = collection["groupid"];
            string topicIDstring = collection["topicid"];
            string userid = collection["userid"];
            string subject = collection["subject"];
            int groupID = Int32.Parse(groupIDstring);
            int topicID = Int32.Parse(topicIDstring);
            Post post = new Post { authorID = userid, dateCreated = DateTime.Now, groupId = groupID, groupTopicID = topicID, subject = subject };
            postService.addNewPost(post);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }


        public ActionResult TopicPost()
        {
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        [HttpPost]
        public ActionResult TopicPost(HttpPostedFileBase file, FormCollection collection)
        {
            string groupidString = collection["groupid"];
            string userid = collection["userid"];
            string topicString = collection["topicid"];
            string subject = collection["subject"];
            int groupID = Int32.Parse(groupidString);
            int topicID = Int32.Parse(topicString);
            if (file == null)
            {
                if (subject != "")
                {
                    Post post = new Post { authorID = userid, groupId = groupID, groupTopicID = topicID, subject = subject, dateCreated = DateTime.Now };
                    postService.addNewPost(post);
                }
                return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }
            else
            {
                if (file.ContentLength > 0)
                {
                    Post post = new Post { authorID = userid, groupId = groupID, groupTopicID = topicID, subject = subject, dateCreated = DateTime.Now, postIsImage = true };
                    postService.addNewPost(post);

                    string imageID = post.ID.ToString();
                    UploadToFtp(file, imageID);

                    string URL = "http://www.ads.menn.is/doors/images/" + imageID;

                    postService.addImagePost(post.ID, URL);
                }
                return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }
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
    }
}