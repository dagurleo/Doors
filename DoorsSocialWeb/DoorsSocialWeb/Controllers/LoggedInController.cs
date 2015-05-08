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
            var shared = new LoggedInSharedLayoutViewModel();
            var userRepo = new UserRepository();
            shared.groups = groupRepo.getAccessibleGroups();
            shared.currentUser = userRepo.getCurrentUser();
            shared.friends = userRepo.getFriendsOfCurrentUser();

            return View(shared);
        }
        public ActionResult GroupView()
        {
            var groupRepo = new GroupRepository();
            var shared = new LoggedInSharedLayoutViewModel();
            var userRepo = new UserRepository();
            shared.groups = groupRepo.getAccessibleGroups();
            shared.currentUser = userRepo.getCurrentUser();

            return View(shared);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
	}
}