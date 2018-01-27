using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC.Blog.DAL.Model
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public HttpPostedFileBase PostedPic{ get; set; }
    }
}
