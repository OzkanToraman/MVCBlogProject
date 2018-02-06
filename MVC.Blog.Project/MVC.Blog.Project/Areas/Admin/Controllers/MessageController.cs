using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Model;
using MVC.Blog.Project.Models;

namespace MVC.Blog.Project.Areas.Admin.Controllers
{
    [UserAuthorize]
    public class MessageController : BaseController
    {
        public MessageController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: Admin/Message
        public ActionResult Inbox()
        {
            IEnumerable<Message> model = _uow.GetRepo<Message>()
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.MessageDate);

            return View(model);
        }
        [HttpPost]
        public JsonResult Inbox(string Secilenler)
        {          
            string[] model = Secilenler.Split(' ');
            foreach (string item in model)
            {
                _uow.GetRepo<Message>()
                    .GetById(Convert.ToInt32(item))
                    .IsRead=true;              
            }
            //_uow.Commit();
            return Json(model);
        }

        public ActionResult Trash()
        {
            IEnumerable<Message> model = _uow
                .GetRepo<Message>()
                .Where(x => x.IsDeleted == true);
            return View(model);
        }


        public JsonResult DeleteInbox(string Secilenler)
        {

            string[] model = Secilenler.Split(' ');
            Message msg = new Message();

            foreach (var item in model)
            {
                msg = _uow.GetRepo<Message>()
                    .GetById(Convert.ToInt32(item));
                msg.IsDeleted = true;
                msg.IsRead = true;
            }

            _uow.Commit();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteTrash(string Secilenler)
        {
            string[] model = Secilenler.Split(' ');
            foreach (string item in model)
            {
                _uow.GetRepo<Message>()
                    .Delete(Convert.ToInt32(item));
            }
            _uow.Commit();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }
}