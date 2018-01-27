using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    public class ProfileController : BaseController
    {
        public ProfileController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: Admin/Profile
        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult ProfilePicUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProfilePicUpload(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (HttpPostedFileBase item in files)
            {
                string uniqueFileName = Guid.NewGuid().ToString();
                string extention = Path.GetExtension(item.FileName);
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

            return RedirectToAction("Profile");
        }
    }
}