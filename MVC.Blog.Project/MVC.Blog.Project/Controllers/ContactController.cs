using MVC.Blog.Project.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Data;
using MVC.Blog.BLL.Validations.MessageValidations;
using MVC.Blog.DAL.Model;

namespace MVC.Blog.Project.Controllers
{
    public class ContactController : BaseController
    {
        public ContactController(IUnitOfWork uow) : base(uow)
        {
        }

        public ActionResult Contact()
        {
            ContactViewModel model = new ContactViewModel();
            model.İletisim = _uow.GetRepo<Contact>()
                .GetList()
                .FirstOrDefault();
            return View(model);
        }

        [HttpPost][ValidateAntiForgeryToken][AllowAnonymous]
        public ActionResult Contact(ContactViewModel model)
        { 
            if (model!=null)
            {
                var validator = new MessageValidator().Validate(model.Mesaj);
                if (validator.IsValid)
                {
                    Kullanici k = _uow.GetRepo<Kullanici>()
                        .Where(x => x.Email == HttpContext.User.Identity.Name)
                        .FirstOrDefault();
                    if (k==null)
                    {
                        model.Mesaj.MessageDate = DateTime.Now;
                        model.Mesaj.MessageFrom = model.Mesaj.Email;
                        model.Mesaj.UserId = 3;
                    }
                    else
                    {
                        model.Mesaj.MessageDate = DateTime.Now;
                        model.Mesaj.UserId = k.Id;
                        model.Mesaj.MessageFrom = k.Name + k.LastName; 
                    }
                    _uow
                        .GetRepo<Message>()
                        .Add(model.Mesaj);
                    if (_uow.Commit()>0)
                    {
                        ModelState.Clear();
                        ViewBag.Msg = "Mesajınız başarıyla gönderildi";
                    }
                }
            }
            model.İletisim = _uow.GetRepo<Contact>()
               .GetList()
               .FirstOrDefault();
            return View(model);
        }

    }
}