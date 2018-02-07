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
        public Kullanici Kullanici{ get; set; }
        public HttpPostedFileBase ProfilPic { get; set; }
    }
}
