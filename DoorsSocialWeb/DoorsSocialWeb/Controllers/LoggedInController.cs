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

namespace DoorsSocialWeb.Controllers
{   
    [Authorize]
    public class LoggedInController : Controller
    {
        //
        // GET: /LoggedIn/
        public ActionResult Index()
        {
<<<<<<< HEAD
            var userService = new UserService();
            var currentUser = userService.getCurrentUser();
            return View(currentUser);
=======
            //var userRepo = new UserRepository();
            //var currentUser = userRepo.getCurrentUser();
            //return View(currentUser);

            var groupRepo = new GroupRepository();
            var groups = groupRepo.getAccessibleGroups();
            return View(groups);
>>>>>>> 91b9132c108b73ebdca615228c9a4d76a385b0ea
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
	}
}