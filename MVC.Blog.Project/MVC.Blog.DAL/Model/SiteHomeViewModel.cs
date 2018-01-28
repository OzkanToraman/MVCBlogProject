using MVC.Blog.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Blog.DAL.Model
{
    public class SiteHomeViewModel
    {
        public IEnumerable<Post> Gonderiler { get; set; }
        public Post Gonderi { get; set; }
        public Kullanici Kullanici { get; set; }
        public IEnumerable<Category> Kategoriler { get; set; }
        public IEnumerable<Comments> Yorumlar { get; set; }
        public Comments Yorum { get; set; }
    }
}