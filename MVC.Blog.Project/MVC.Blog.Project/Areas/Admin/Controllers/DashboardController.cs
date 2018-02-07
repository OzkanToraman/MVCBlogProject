using MVC.Blog.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Data;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    [UserAuthorize]
    public class DashboardController : BaseController
    {
        public static int userId;

        public DashboardController(IUnitOfWork uow) : base(uow)
        {
        }

        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            ViewBag.UserName = HttpContext.User.Identity.Name;
            userId =_uow
                .GetRepo<Kullanici>()
                .Where(x => x.Email == HttpContext.User.Identity.Name)
                .FirstOrDefault()
                .Id;
            IEnumerable<Message> messages  =  _uow
                                                .GetRepo<Message>()
                                                .Where(x => x.IsRead == false);
            return View(messages);
        }

    }
}