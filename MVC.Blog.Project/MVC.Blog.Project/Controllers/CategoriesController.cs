using MVC.Blog.DAL.Model;
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
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: Categories
        public ActionResult Index(int id)
        {
            SiteHomeViewModel model = new SiteHomeViewModel();
            model.Gonderiler = _uow
               .GetRepo<Post>()
               .Where(x=>x.CategoryId==id)
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