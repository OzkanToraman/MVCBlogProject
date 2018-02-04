using MVC.Blog.DAL.Data;
using MVC.Blog.Repository.UOW.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Blog.Project.Models
{
    public class UserAuthorize : AuthorizeAttribute
    {

        public string UserRole { get; set; }       

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.Name== "ozkn.toraman@gmail.com")
            {
                return true;
            }
            else
            {
                httpContext.Response.Redirect("/Home/Index");
                return false;
            }
        }
    }
}