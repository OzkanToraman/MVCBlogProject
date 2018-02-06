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

        [HttpPost][ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ContactPage(Contact model)
        {
            _uow
                .GetRepo<Contact>()
                .Update(model);
            if (_uow.Commit() > 0)
                TempData["Msg"] = "Güncelleme başarılı";
            return View();
        }

        public ActionResult AboutPage()
        {
            About model = _uow
                .GetRepo<About>()
                .GetList()
                .FirstOrDefault();
            return View(model);
        }

        [HttpPost][ValidateInput(false)][ValidateAntiForgeryToken]
        public ActionResult AboutPage(About model)
        {
            _uow
                .GetRepo<About>()
                .Update(model);
            if (_uow.Commit() > 0)
                TempData["Msg"] = "Güncelleme başarılı";
            return View();
        }
    }
}