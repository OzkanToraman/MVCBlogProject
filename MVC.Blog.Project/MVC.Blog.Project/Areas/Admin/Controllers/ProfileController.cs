using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Model;
using MVC.Blog.Project.Models;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    [UserAuthorize]
    public class ProfileController : BaseController
    {
        public ProfileController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: Admin/Profile
        public ActionResult Profile()
        {
            return View();
        }

    }
}