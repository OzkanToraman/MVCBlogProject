using MVC.Blog.Project.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;

namespace MVC.Blog.Project.Controllers
{
    public class AboutController : BaseController
    {
        public AboutController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: About
        public ActionResult About()
        {
            return View();
        }
    }
}