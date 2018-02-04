using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Blog.Project.Models
{
    public class RolesAttribute:AuthorizeAttribute
    {
        public RolesAttribute()
        {

        }
    }
}