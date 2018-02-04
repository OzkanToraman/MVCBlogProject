using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Blog.Project.Models
{
    public class UserAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.Cookies["ozkn.toraman@gmail.com"] != null)
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