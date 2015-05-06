﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoorsSocialWeb.Controllers
{   
    [Authorize]
    public class LoggedInController : Controller
    {
        //
        // GET: /LoggedIn/
        public ActionResult Index()
        {
            return View();
        }
	}
}