﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Blog.Project.Controllers
{
    public class HomeController : Controller
    {
        // GET: Hıme
        public ActionResult Index()
        {
            return View();
        }
    }
}