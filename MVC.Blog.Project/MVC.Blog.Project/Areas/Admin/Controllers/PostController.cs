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
using MVC.Blog.BLL.Services.Abstract;
using System.Threading.Tasks;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        bool IsSuccess;
        IEnumerable<string> tags = new List<string>();

        public PostController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: Admin/Post
        public ActionResult Listele()
        {
            IEnumerable<Post> model =
                _uow
                .GetRepo<Post>()
                .Where(x => x.IsDeleted == false);
            CategoryFill();
            return View(model);
        }

        public ActionResult Ekle()
        {
            IEnumerable<Category> model = _uow.GetRepo<Category>()
                .Where(x => x.IsDeleted == false);

            CategoryFill();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Ekle(PostViewModel model)
        {

            if (model.PostedPic != null)
            {
                #region UploadPhotoSaveToDatabase    
                MediaUpload upload = new MediaUpload();
                upload = UploadSaveToDatabase(model.PostedPic);
                _uow.GetRepo<MediaUpload>()
                    .Add(upload);
                _uow.Commit();
                model.Post.PostPic = upload.Path.ToString();
                #endregion
            }

            if (model.Post.Tags != null)
            {
                char[] separators = { ',', '.', '!', '?', ';', ':', ' ' };
                tags = model.Post.Tags.Split(separators);
            }

            IsSuccess = false;
            var validator = new PostAddValidator().Validate(model.Post);
            if (validator.IsValid)
            {
                if (model.Post.Tags != null)
                {
                    foreach (var item in tags)
                    {
                        model.Post.Tags = item.ToString();
                    }
                }
                model.Post.UserId = 1;              
                model.Post.PostDate = DateTime.Now;
                _uow.GetRepo<Post>()
                    .Add(model.Post);

                #region KategoriGönderiSayısıKontrol
                _uow.GetRepo<Category>()
                            .GetById(model.Post.CategoryId)
                            .PostCount++;
                #endregion

                if (_uow.Commit() > 0)
                {
                    IsSuccess = true;
                    ViewBag.IsSuccess = IsSuccess;
                    ViewBag.Msg = "Yazı başarıyla eklendi.";
                }
                else
                {
                    ViewBag.IsSuccess = IsSuccess;
                    ViewBag.Msg = "Yazı kaydedilirken bir hata oluştu!";
                }
            }
            else
            {
                validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
            }
            CategoryFill();
            return View();
        }

        public ActionResult Guncelle(int id)
        {
            Post model = _uow
                .GetRepo<Post>()
                .Where(x => x.Id == id)
                .FirstOrDefault();
            CategoryFill();
            PostViewModel m = new PostViewModel()
            {
                Post = model,
                 PostedPic=null,
            };
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Guncelle(PostViewModel model)
        {
            if (model.Post != null)
            {
                if (model.PostedPic != null)
                {
                    MediaUpload m = new MediaUpload();
                    m = UploadSaveToDatabase(model.PostedPic);
                    _uow.GetRepo<MediaUpload>()
                            .Add(m);
                    model.Post.PostPic = m.Path.ToString();
                }
                _uow.GetRepo<Post>()
                    .Update(model.Post);
                if (_uow.Commit() > 0)
                {
                    return RedirectToAction("Listele", "Post");
                }
            }
            return RedirectToAction("Listele", "Post");
        }

        public ActionResult Sil(int id)
        {
            var sorgu = _uow.GetRepo<Post>()
                .GetById(id);
            sorgu.IsDeleted = true;

            #region SilinenGönderiyeGöreKategoriSayısıDüzenle
            _uow.GetRepo<Category>()
                    .GetById(sorgu.CategoryId)
                    .PostCount--;
            #endregion

            _uow.Commit();
            return RedirectToAction("Listele", "Post");
        }

        void CategoryFill()
        {
            #region CategoryCombobox
            IEnumerable<Category> catmodel = _uow.GetRepo<Category>()
                .Where(x => x.IsDeleted == false);

            List<SelectListItem> cList = new List<SelectListItem>();
            foreach (var item in catmodel)
            {
                cList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            ViewBag.Kategoriler = cList;
            TempData["cat"] = cList;
            #endregion
        }

        void RoleFill()
        {
            #region StatusCombobox
            IEnumerable<Role> rolemodel = _uow.GetRepo<Role>()
                .GetList();

            List<SelectListItem> roleList = new List<SelectListItem>();
            foreach (var item in rolemodel)
            {
                roleList.Add(new SelectListItem
                {
                    Text = item.RoleName,
                    Value = item.Id.ToString()
                });
            }
            ViewBag.Durum = roleList;
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