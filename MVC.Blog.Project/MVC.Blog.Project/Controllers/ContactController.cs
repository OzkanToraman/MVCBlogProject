using MVC.Blog.Project.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Data;

namespace MVC.Blog.Project.Controllers
{
    public class ContactController : BaseController
    {
        public ContactController(IUnitOfWork uow) : base(uow)
        {
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}