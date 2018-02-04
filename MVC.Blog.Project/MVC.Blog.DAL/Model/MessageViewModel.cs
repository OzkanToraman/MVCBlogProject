using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Blog.DAL.Model
{
    public class MessageViewModel
    {
        public IEnumerable<Message> Mesaj{ get; set; }
        public Kullanici Kullanici { get; set; }
    }
}
