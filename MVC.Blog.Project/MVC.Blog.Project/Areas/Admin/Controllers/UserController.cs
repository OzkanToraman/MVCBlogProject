using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Model;
using System.IO;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: Admin/User
        public ActionResult Listele()
        {
            IEnumerable<Kullanici> model =
                _uow
                .GetRepo<Kullanici>()
                .Where(x => x.IsDeleted == false);
                
            return View(model);
        }

        public ActionResult Ekle()
        {
            #region ComboboxRolDoldur
            IEnumerable<Role> model = _uow
                    .GetRepo<Role>()
                    .GetList();

            List<SelectListItem> rolList = new List<SelectListItem>();
            foreach (var item in model)
            {
                rolList.Add(new SelectListItem
                {
                    Text = item.RoleName,
                    Value = item.Id.ToString()
                });
            }
            ViewBag.Roller = rolList;
            TempData["roller"] = rolList;
            #endregion

            return View();
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult Ekle(Kullanici model)
        {

            _uow.GetRepo<Kullanici>()
                .Add(model);
            _uow.Commit();
            return RedirectToAction("Listele","User");
        }

        public ActionResult Guncelle(int id)
        {
            Kullanici model = _uow
                .GetRepo<Kullanici>()
                .Where(x => x.Id == id)
                .FirstOrDefault();
            ViewBag.Roller2 = TempData["roller"];
            return View(model);
        }

        [HttpPost]
        public ActionResult Guncelle(UserUpdateModel model)
        {
            #region UploadPhotoSaveToDatabase
            string uniqueFileName = Guid.NewGuid().ToString();
            string extention = Path.GetExtension(model.ProfilePic.FileName);
            string fullFileName = HttpContext.Server.MapPath("/Media/Images/" + uniqueFileName + extention);
            model.ProfilePic.SaveAs(fullFileName);
            MediaUpload upload = new MediaUpload();
            upload.Name = uniqueFileName + extention;
            upload.Path = "/Media/Images/" + uniqueFileName + extention;
            _uow.GetRepo<MediaUpload>()
                .Add(upload);
            _uow.Commit();
            #endregion

            model.kullanici.ProfilPic = "/Media/Images/" + uniqueFileName + extention;

            _uow.GetRepo<Kullanici>()
                .Update(model.kullanici);
            if (_uow.Commit()>0)
            {
                return Redirect("Listele");
            }
            return View();
        }
    }
}