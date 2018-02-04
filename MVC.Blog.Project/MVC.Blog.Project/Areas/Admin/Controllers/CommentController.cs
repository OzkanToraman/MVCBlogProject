using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Data;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    public class CommentController : BaseController
    {
        public CommentController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: Admin/Comment
        public ActionResult Listele()
        {
            IEnumerable<Comments> model = _uow.GetRepo<Comments>()
             .Where(x => x.IsDeleted == false);
            return View(model);
        }

        public ActionResult Guncelle(int id)
        {

            return View();
        }

        public ActionResult Sil(int id)
        {
            _uow.GetRepo<Comments>()
                .Delete(id);
            _uow.Commit();
            return Redirect("Listele");
        }
    }
}