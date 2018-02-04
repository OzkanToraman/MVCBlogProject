using MVC.Blog.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            ViewBag.UserName = HttpContext.User.Identity.Name;
            return View();
        }

    }
}