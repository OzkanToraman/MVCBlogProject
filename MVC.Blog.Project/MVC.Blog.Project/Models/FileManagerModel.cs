using MVC.Blog.DAL.Data;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Blog.Project.Models
{
    public class FileManagerModel
    {
        public int? SayfaNo { get; set; }
        public IPagedList<MediaUpload> Yuklenenler { get; set; }
    }
}