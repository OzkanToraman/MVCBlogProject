using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Data;
using MVC.Blog.BLL.Validations.CategoryValidations;
using MVC.Blog.Project.Models;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{

    public class CategoryController : BaseController
    {

        bool IsSucces;

        public CategoryController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: Admin/Category
        public ActionResult Listele()
        {
            var model =
                _uow
                .GetRepo<Category>()
                .Where(x => x.IsDeleted == false);

            return View(model);
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(Category model)
        {
            bool IsSuccess = false;
            var validator = new CategoryAddValidator(_uow).Validate(model);
            if (validator.IsValid)
            {
                _uow.GetRepo<Category>()
                    .Add(model);
                if (_uow.Commit() > 0)
                {
                    IsSuccess = true;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Kategori başarıyla eklendi.";
                    ModelState.Clear();
                }
                else
                {
                    IsSuccess = false;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Kategori eklerken bir hata oluştu!";
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
            Category model = _uow.GetRepo<Category>().Where(x => x.Id == id)
                .FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guncelle(Category model)
        {

            _uow.GetRepo<Category>()
                .Update(model);
            if (_uow.Commit() > 0)
            {
                return RedirectToAction("Listele", "Category");
            }
            return View();

        }

        public ActionResult Sil(int id)
        {
            _uow.GetRepo<Category>()
               .Delete(id);
            _uow.Commit();

            return RedirectToAction("Listele", "Category");


        }
    }
}