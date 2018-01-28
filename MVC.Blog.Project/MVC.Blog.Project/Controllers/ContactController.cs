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

        [HttpPost]
        public ActionResult Contact(Message model)
        {
            if (model!=null)
            {
                var validator = new MessageValidator().Validate(model);
                if (validator.IsValid)
                {
                    Kullanici k = _uow.GetRepo<Kullanici>()
                        .Where(x => x.Email == "otoraman@outlook.com")
                        .FirstOrDefault();
                    if (k==null)
                    {
                        model.MessageFrom = "Misafir";
                    }
                    else
                    {
                        model.UserId = k.Id;
                        model.MessageFrom = k.Name; 
                    }
                    _uow
                        .GetRepo<Message>()
                        .Add(model);
                    if (_uow.Commit()>0)
                    {
                        ViewBag.Msg = "Mesajınız başarıyla gönderildi";
                    }
                }
            }
            return View();
        }

    }
}