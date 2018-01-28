﻿using MVC.Blog.Project.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Blog.Repository.UOW.Abstract;
using MVC.Blog.DAL.Data;
using MVC.Blog.DAL.Model;

namespace MVC.Blog.Project.Controllers
{
    public class SinglePostController : BaseController
    {
        public SinglePostController(IUnitOfWork uow) : base(uow)
        {
        }

        // GET: Post
        public ActionResult SinglePost(int id)
        {
            SiteHomeViewModel model = new SiteHomeViewModel();

            Post p = _uow.GetRepo<Post>()
                .Where(x => x.Id == id)
                .FirstOrDefault();
            model.Gonderi = p;
            model.Kategoriler = _uow.GetRepo<Category>()
                .Where(x => x.IsDeleted == false);
            model.Kullanici = _uow.GetRepo<Kullanici>()
                .Where(x => x.RoleId == 1)
                .FirstOrDefault();
            return View(model);
        }
    }
}