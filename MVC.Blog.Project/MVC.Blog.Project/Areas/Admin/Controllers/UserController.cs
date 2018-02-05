using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Model;
using System.IO;
using MVC.Blog.Project.Models;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    [UserAuthorize]
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
            RolFill();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(Kullanici model)
        {
            if (model != null)
            {
                model.ProfilPic = "/Media/Images/c3c5c1df-0964-4c38-820e-7412187c7117.jpg";
                _uow.GetRepo<Kullanici>()
                    .Add(model);
                _uow.Commit();
                return RedirectToAction("Listele", "User");
            }
            ViewBag.Msg = "Zorunlu alanları doldurunuz!";
            return View();
        }

        public ActionResult Guncelle(int id)
        {
            Kullanici model = _uow
                .GetRepo<Kullanici>()
                .Where(x => x.Id == id)
                .FirstOrDefault();
            RolFill();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guncelle(int id,UserUpdateModel model)
        {
            if (model.kullanici != null)
            {
                if (model.ProfilePic != null)
                {
                    #region UploadPhotoSaveToDatabase
                    MediaUpload m = new MediaUpload();
                    m = UploadSaveToDatabase(model.ProfilePic);
                    _uow.GetRepo<MediaUpload>()
                        .Add(m);
                    _uow.Commit();
                    #endregion
                    model.kullanici.ProfilPic = m.Path.ToString();
                }

                _uow.GetRepo<Kullanici>()
                    .Update(model.kullanici);
                if (_uow.Commit() > 0)
                {
                    return RedirectToAction("Listele", "User");
                }
            }
            return View();
        }

        public ActionResult Sil(int id)
        {
            _uow.GetRepo<Kullanici>()
                .Where(x => x.Id == id)
                .FirstOrDefault()
                .IsDeleted = true;
            _uow.Commit();
            return Redirect("Listele");
        }

        void RolFill()
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
            #endregion

        }
        MediaUpload UploadSaveToDatabase(HttpPostedFileBase img)
        {
            string uniqueFileName = Guid.NewGuid().ToString();
            string extention = Path.GetExtension(img.FileName);
            string fullFileName = HttpContext.Server.MapPath("/Media/Images/" + uniqueFileName + extention);
            img.SaveAs(fullFileName);
            MediaUpload upload = new MediaUpload();
            upload.Name = uniqueFileName + extention;
            upload.Path = "/Media/Images/" + uniqueFileName + extention;

            return upload;
        }
    }
}