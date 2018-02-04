using MVC.Blog.Project.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Data;
using MVC.Blog.DAL.Model;
using MVC.Blog.BLL.Services.Abstract;
using System.Web.Security;
using PagedList;

namespace MVC.Blog.Project.Controllers
{
    public class HomeController : BaseController
    {
        int sayfaNo;
        IEncryptor _encrypt;
        public HomeController(IUnitOfWork uow,IEncryptor encrypt) : base(uow)
        {
            _encrypt = encrypt;
        }

        public ActionResult Index(int? Page)
        {
            int count = _uow.GetRepo<Post>().Where(x => x.IsDeleted == false).Count();
            double totalPage = Math.Ceiling(Convert.ToDouble(count/4));
            ViewBag.TotalPage = totalPage;
            sayfaNo = Page ?? 1;
                
            SiteHomeViewModel model = new SiteHomeViewModel();
            model.Gonderiler = _uow
               .GetRepo<Post>()
               .GetList()
               .OrderByDescending(x => x.PostDate)
               .Skip((sayfaNo-1)*4)
               .Take(4);
            model.SayfaNo = sayfaNo;
            model.Kategoriler = _uow
                .GetRepo<Category>()
                .Where(x => x.IsDeleted == false);
            model.Kullanici = _uow
                .GetRepo<Kullanici>()
                .Where(x => x.RoleId == 1)
                .FirstOrDefault();
            model.LogUser = HttpContext.User.Identity.Name;

            return View(model);
        }

        //[HttpPost]
        //public ActionResult Index(int Page)
        //{
        //    SiteHomeViewModel model = new SiteHomeViewModel();
        //    model.Gonderiler = _uow.GetRepo<Post>()
        //        .GetList()
        //        .Skip((Page - 1) * 2)
        //        .Take(2);
        //    model.SayfaNo = sayfaNo;
        //    model.Kategoriler = _uow
        //        .GetRepo<Category>()
        //        .Where(x => x.IsDeleted == false);
        //    model.Kullanici = _uow
        //        .GetRepo<Kullanici>()
        //        .Where(x => x.RoleId == 1)
        //        .FirstOrDefault();
        //    model.LogUser = HttpContext.User.Identity.Name;

        //    return PartialView("_Icerik",model);
        //}
    }
}