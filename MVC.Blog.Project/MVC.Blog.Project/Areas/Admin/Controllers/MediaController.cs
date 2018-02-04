using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Data;
using System.IO;
using PagedList;
using MVC.Blog.Project.Models;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    //[UserAuthorize]
    public class MediaController : BaseController
    {
        public MediaController(IUnitOfWork uow) : base(uow)
        {
        }
        IEnumerable<MediaUpload> model = new List<MediaUpload>();
        // GET: Admin/Media
        public ActionResult FileManager(int? Page)
        {
            int _sayfaNo = Page ?? 1;
            ViewBag.currentPage = _sayfaNo;
            model = _uow.GetRepo<MediaUpload>()
                .GetList()
                .OrderByDescending(x => x.Id)
                .Skip((_sayfaNo - 1) * 5)
                .Take(5);

            //if (Request.IsAjaxRequest())
            //{
            //    model = _uow.GetRepo<MediaUpload>()
            //    .GetList()
            //    .OrderByDescending(x => x.Id)
            //    .Take(3);
            //    return PartialView("_FileManagerTable", model);
            //}
            return View(model);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (HttpPostedFileBase item in files)
            {
                string uniqueFileName = Guid.NewGuid().ToString();
                string extention = Path.GetExtension(item.FileName);
                //fiziksel dosya konumunu belirttik
                //HttpContext.Server.MapPath() bulunduğumuz web dizini gönderir.
                string fullFileName = HttpContext.Server.MapPath("/Media/Images/" + uniqueFileName + extention);

                //bu itemi şu dizine kaydet demek
                item.SaveAs(fullFileName);
                MediaUpload upload = new MediaUpload();
                upload.Name = uniqueFileName + extention;
                upload.Path = "/Media/Images/" + uniqueFileName + extention;
                _uow.GetRepo<MediaUpload>()
                    .Add(upload);
                _uow.Commit();
                //yeni bir dosya açmak için
                //System.IO.File.Create(fullFileName);

                //dosya silme işlemi klasörden silme
                //System.IO.File.Delete(fullFileName);

                // Dosya Var Mı ? bool bir değer döndür.
                //System.IO.File.Exists(fullFileName);

            }

            return RedirectToAction("FileManager");
        }

        public JsonResult DeleteUpload(string Secilenler)
        {
            string[] model = Secilenler.Split(' ');

            foreach (string item in model)
            {
                _uow.GetRepo<MediaUpload>()
                    .Delete(Convert.ToInt32(item));
            }
            _uow.Commit();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult FileManager(int Page)
        {
            var model = _uow.GetRepo<MediaUpload>()
                .GetList()
                .OrderByDescending(x => x.Id)
                .Skip((Page - 1) * 5)
                .Take(5);

            return PartialView("_FileManagerTable",model);
        }
    }
}