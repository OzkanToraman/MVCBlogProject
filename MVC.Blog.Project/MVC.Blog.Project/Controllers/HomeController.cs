using MVC.Blog.Project.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Data;
using MVC.Blog.DAL.Model;

namespace MVC.Blog.Project.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork uow) : base(uow)
        {
        }

        public ActionResult Index()
        {
            SiteHomeViewModel model = new SiteHomeViewModel();
            model.Gonderiler = _uow
               .GetRepo<Post>()
               .GetList()
               .OrderByDescending(x => x.PostDate)
               .Take(3);
            model.Kategoriler = _uow
                .GetRepo<Category>()
                .Where(x => x.IsDeleted == false);
            model.Kullanici = _uow
                .GetRepo<Kullanici>()
                .Where(x => x.RoleId == 1)
                .FirstOrDefault();
            return View(model);
        }
    }
}