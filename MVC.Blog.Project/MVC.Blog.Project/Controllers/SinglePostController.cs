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
    public class SinglePostController : BaseController
    {
        public SinglePostController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: Post
        public ActionResult SinglePost(int id)
        {
            SiteHomeViewModel model = new SiteHomeViewModel();

            model.Gonderi =
                _uow.GetRepo<Post>()
                .Where(x => x.Id == id)
                .FirstOrDefault();
            model.Gonderiler = _uow
                .GetRepo<Post>()
                .GetList()
                .OrderByDescending(x => x.PostDate)
                .Take(3);
            model.Kategoriler =
                _uow.GetRepo<Category>()
                .Where(x => x.IsDeleted == false);
            model.Kullanici =
                _uow.GetRepo<Kullanici>()
                .Where(x => x.RoleId == 1)
                .FirstOrDefault();
            model.Yorumlar =
                _uow.GetRepo<Comments>()
                .Where(x => x.IsDeleted == false && x.PostId==id)
                .OrderByDescending(x => x.CommentDate);
            model.LogUser = HttpContext.User.Identity.Name;
            return View(model);
        }

        public JsonResult PostComment(string Yorum, int GonderiId)
        {
            var user = _uow.GetRepo<Kullanici>()
                .Where(x => x.Email == HttpContext.User.Identity.Name)
                .FirstOrDefault();

            var yorumlar = new CommentViewModel
            {
                CommentBody = Yorum,
                CommentDate = DateTime.Now.ToString(),
                PostId = GonderiId,
                Name = user.Name,
                LastName = user.LastName,
                ProfilPic = user.ProfilPic
            };

            Comments commentModel = new Comments()
            {
                CommentBody = yorumlar.CommentBody,
                CommentDate = Convert.ToDateTime(yorumlar.CommentDate),
                PostId = yorumlar.PostId,
                UserId = user.Id,
            };

            _uow.GetRepo<Comments>()
                .Add(commentModel);
            _uow.Commit();


            return Json(yorumlar, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Like(int likeCount)
        {
            likeCount++;
            return Json(likeCount);
        }
    }
}