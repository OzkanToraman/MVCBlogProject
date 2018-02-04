using MVC.Blog.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    [UserAuthorize]
    public class PageController : Controller
    {
        // GET: Admin/Page
        public ActionResult Index()
        {
            return View();
        }
    }
}