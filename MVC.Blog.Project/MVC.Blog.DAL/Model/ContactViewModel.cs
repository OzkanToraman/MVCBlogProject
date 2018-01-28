using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Blog.DAL.Model
{
    public class ContactViewModel
    {
        public Message Mesaj{ get; set; }
        public Contact İletisim{ get; set; }
    }
}
