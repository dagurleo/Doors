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
        //
        // GET: /LoggedIn/
        public ActionResult Index()
        {            
            
            var shared = new IndexViewModel();
            
            var postService = new PostService();
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


        public ActionResult CreateGroupView()
        {            
            var shared = new LoggedInSharedLayoutViewModel();
            shared.groups = groupService.getAccessibleGroups();
            shared.currentUser = userService.getCurrentUser();
            shared.friends = userService.getFriendsOfCurrentUser();

            return View(shared);
        }
        [HttpPost]
        public ActionResult CreateGroupView(FormCollection collection)
        {
            string groupOwnerId = collection["ownerid"];
            string groupName = collection["groupname"];
            string groupDescription = collection["groupdescription"];

            Group group = new Group { groupOwnerID = groupOwnerId, groupName = groupName, groupDescription = groupDescription };
            
            groupService.addNewGroup(group);
            return RedirectToAction("CreateGroupView", "LoggedIn", group.ID);
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

            if(subject != "")
            {
                Post post = new Post { authorID = userid, subject = subject, dateCreated = DateTime.Now };
                var postService = new PostService();
                postService.addNewPost(post);
            }
            return RedirectToAction("Index", "LoggedIn");

        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
	}
}