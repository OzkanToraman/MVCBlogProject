using MVC.Blog.DAL.Data;
using MVC.Blog.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    [UserAuthorize]
    public class PageController : BaseController
    {
        public PageController(IUnitOfWork uow) : base(uow)
        {
        }

        public ActionResult ContactPage()
        {
            Contact model = _uow
                .GetRepo<Contact>()
                .GetList()
                .FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult ContactPage(Contact model)
        {
            _uow
                .GetRepo<Contact>()
                .Update(model);
            _uow
                .Commit();
            return View();
        }
    }
}