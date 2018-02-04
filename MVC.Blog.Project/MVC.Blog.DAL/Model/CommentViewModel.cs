using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Blog.DAL.Model
{
    public class CommentViewModel
    {
        public string CommentBody { get; set; }
        public int PostId { get; set; }
        public string CommentDate { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ProfilPic { get; set; }
    }
}
