using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC.Blog.DAL.Model
{
    public class UserUpdateModel
    {
        public Kullanici kullanici{ get; set; }
        public HttpPostedFileBase ProfilePic { get; set; }
    }
}
