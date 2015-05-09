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

namespace DoorsSocialWeb.Controllers
{   
    [Authorize]
    public class LoggedInController : Controller
    {
        //
        // GET: /LoggedIn/
        public ActionResult Index()
        {
            //var userRepo = new UserRepository();
            //var currentUser = userRepo.getCurrentUser();
            //return View(currentUser);

            var groupRepo = new GroupRepository();
            var shared = new IndexViewModel();
            var userRepo = new UserRepository();
            shared.groups = groupRepo.getAccessibleGroups();
            shared.currentUser = userRepo.getCurrentUser();
            shared.friends = userRepo.getFriendsOfCurrentUser();
            PostRepository postRepo = new PostRepository();            
            var posts = new PostRepository();
            shared.posts = posts.getAllPosts();
            return View(shared);
            
        }

        public ActionResult GroupView(int id)
        {
            var groupRepo = new GroupRepository();
            var shared = new GroupViewModel();
            var userRepo = new UserRepository();
            shared.groups = groupRepo.getAccessibleGroups();
            shared.currentUser = userRepo.getCurrentUser();
            shared.friends = userRepo.getFriendsOfCurrentUser();
            shared.currentGroup = groupRepo.getCurrentGroup(id);

            return View(shared);
        }


        public ActionResult CreateGroupView()
        {
            var groupRepo = new GroupRepository();
            var shared = new LoggedInSharedLayoutViewModel();
            var userRepo = new UserRepository();
            shared.groups = groupRepo.getAccessibleGroups();
            shared.currentUser = userRepo.getCurrentUser();
            shared.friends = userRepo.getFriendsOfCurrentUser();

            return View(shared);
        }

        public ActionResult Profile(string id)
        {
            var groupRepo = new GroupRepository();
            var shared = new ProfileViewModel();
            var userRepo = new UserRepository();
            shared.groups = groupRepo.getAccessibleGroups();
            shared.currentUser = userRepo.getCurrentUser();
            shared.friends = userRepo.getFriendsOfCurrentUser();
            shared.friend = userRepo.getUserByID(id);

            
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

            Post post = new Post { authorID = userid, subject = subject, dateCreated = DateTime.Now };
            PostRepository postrepo = new PostRepository();
            postrepo.addNewPost(post);
            return RedirectToAction("Index", "LoggedIn");
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
	}
}