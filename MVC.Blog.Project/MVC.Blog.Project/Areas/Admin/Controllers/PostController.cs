using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Data;
using MVC.Blog.BLL.Validations.PostValidations;
using System.IO;
using MVC.Blog.DAL.Model;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        bool IsSuccess;

        public PostController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: Admin/Post
        public ActionResult Listele()
        {
            IEnumerable<Post> model = 
                _uow
                .GetRepo<Post>()
                .Where(x => x.IsDeleted==false);

            return View(model);
        }

        public ActionResult Ekle()
        {
            IEnumerable<Category> model = _uow.GetRepo<Category>()
                .Where(x => x.IsDeleted == false);

            #region CategoryCombobox
            List<SelectListItem> cList = new List<SelectListItem>();
            foreach (var item in model)
            {
                cList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            ViewBag.Kategoriler = cList;
            #endregion

            return View();
        }

        [HttpPost][ValidateAntiForgeryToken][ValidateInput(false)]
        public ActionResult Ekle(PostViewModel model)
        {

            #region UploadPhotoSaveToDatabase
            string uniqueFileName = Guid.NewGuid().ToString();
            string extention      = Path.GetExtension(model.PostedPic.FileName);
            string fullFileName   = HttpContext.Server.MapPath("~/Media/Images/" + uniqueFileName + extention);
            model.PostedPic.SaveAs(fullFileName);
            MediaUpload upload = new MediaUpload();
            upload.Name = uniqueFileName + extention;
            upload.Path = "~/Media/Images/" + uniqueFileName + extention;
            _uow.GetRepo<MediaUpload>()
                .Add(upload);
            _uow.Commit();
            #endregion

            char[] separators = { ',', '.', '!', '?', ';', ':', ' ' };
            string[] tags = model.Post.Tags.Split(separators);

            bool IsSuccess = false;
            var validator = new PostAddValidator().Validate(model.Post);
            if (validator.IsValid)
            {
                
                _uow.GetRepo<Post>().Add(model.Post);
                if (_uow.Commit()>0)
                {
                    IsSuccess = true;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Yazı başarıyla eklendi.";
                }
                else
                {
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Yazı kaydedilirken bir hata oluştu!";
                }
            }
            else
            {
                validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
            }
            
            return View();
        }

        public ActionResult Guncelle(int id)
        {
            Post model = _uow
                .GetRepo<Post>()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return View(model);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult Guncelle(Post model)
        {
            _uow.GetRepo<Post>()
                .Update(model);
            if (_uow.Commit()>0)
            {
                return RedirectToAction("Listele", "Post");
            }
            return View();
        }


    }
}